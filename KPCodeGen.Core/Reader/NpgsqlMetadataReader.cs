/*
 * Copyright 2011-2015 Numeric Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using  KPCodeGen.Entity.Domain;
using Npgsql;
using System.Data.Common;
using KPCodeGen.Enumerator;

namespace KPCodeGen.Core.Reader
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class NpgsqlMetadataReader : AbstractMetadataReader
    {
        private readonly string _connectionString;

        public NpgsqlMetadataReader(string connectionStr)
        {
            this._connectionString = connectionStr;
            DbConnectionStringBuilder connStr = new DbConnectionStringBuilder();
            connStr.ConnectionString = _connectionString;
            DatabaseName = connStr["Database"] as String;
        }


        #region IMetadataReader Members

        protected override IList<Column> GetTablesDetails(Table table, string owner)
        {
            var columns = new List<Column>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                using (NpgsqlCommand tableDetailsCommand = conn.CreateCommand())
                {


                    tableDetailsCommand.CommandText = String.Format(
@"
SELECT
	c.column_name, c.data_type, c.is_nullable, b.constraint_type as type, a.constraint_name
FROM 
	information_schema.constraint_column_usage a
INNER JOIN information_schema.table_constraints b 
	ON a.constraint_name=b.constraint_name
INNER JOIN information_schema.columns c 
	ON a.column_name=c.column_name AND a.table_name=c.table_name
WHERE
		a.table_schema = '{1}' 
	AND a.table_name='{0}'
	AND b.constraint_type IN ('PRIMARY KEY')

UNION

SELECT
	a.column_name, a.data_type, a.is_nullable, b.constraint_type as type, b.constraint_name
FROM
	information_schema.columns a
INNER JOIN information_schema.table_constraints b 
	ON b.constraint_name ='{0}_'||a.column_name||'_fkey'

UNION

SELECT 
	a.column_name, a.data_type, a.is_nullable, '', ''
FROM
	information_schema.columns a
WHERE 
	    a.table_schema = '{1}' 
	AND a.table_name = '{0}' 
	AND a.column_name 
	NOT IN
	(
		SELECT
			c.column_name
		FROM
			information_schema.constraint_column_usage a
		INNER JOIN information_schema.table_constraints b 
			ON a.constraint_name=b.constraint_name
		INNER JOIN information_schema.columns c 
			ON a.column_name=c.column_name AND a.table_name=c.table_name
		WHERE
				a.table_schema = '{1}'
			AND a.table_name = '{0}' 
			AND b.constraint_type IN ('PRIMARY KEY')
			
		UNION
		
		SELECT
			a.column_name
		FROM 
			information_schema.columns a
		INNER JOIN information_schema.table_constraints b 
			ON b.constraint_name ='{0}_'||a.column_name||'_fkey'
	)", table.Name, owner);


                    using (NpgsqlDataReader sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        while (sqlDataReader.Read())
                        {
                            
                            string columnName = sqlDataReader.GetString(0);
                            string dataType = sqlDataReader.GetString(1);
                            bool isNullable = sqlDataReader.GetString(2).Equals("YES",
                                                                                StringComparison.
                                                                                    CurrentCultureIgnoreCase);
                            bool isPrimaryKey =
                                (!sqlDataReader.IsDBNull(3)
                                     ? sqlDataReader.GetString(3).Equals(
                                         NpgsqlConstraintType.PrimaryKey.ToString(),
                                         StringComparison.CurrentCultureIgnoreCase)
                                     : false);
                            bool isForeignKey =
                                (!sqlDataReader.IsDBNull(3)
                                     ? sqlDataReader.GetString(3).Equals(
                                         NpgsqlConstraintType.ForeignKey.ToString(),
                                         StringComparison.CurrentCultureIgnoreCase)
                                     : false);

                            string constraintName = sqlDataReader.GetString(4);

                            var m = new DataTypeMapper();

                            columns.Add(new Column(table)
                            {
                                Name = columnName,
                                DataType = dataType,
                                IsNullable = isNullable,
                                IsPrimaryKey = isPrimaryKey,
                                IsForeignKey = isForeignKey,
                                ConstraintName = constraintName,
                                MappedDataType =
                                m.MapFromDBType(DatabaseServerType.PostgreSQL, dataType, null, null, null, isPrimaryKey),
                                //DataLength = dataLength
                            });
                        }
                        table.Columns = columns;
                        table.OwnerSchema = owner;
                        table.PrimaryKey = DeterminePrimaryKeys(table);

                        // Need to find the table name associated with the FK
                        foreach (var c in table.Columns)
                        {
                            c.ForeignKeyTableName = GetForeignKeyReferenceTableName(table.Name, c.Name);
                        }
                        table.ForeignKeys = DetermineForeignKeyReferences(table);
                        table.HasManyRelationships = DetermineHasManyRelationships(table);
                    }
                }
            }
            return columns;
        }

        public override IList<string> GetOwners()
        {
            var owners = new List<string>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                var tableCommand = conn.CreateCommand();
                tableCommand.CommandText = @"select distinct table_schema from information_schema.tables
                                                union
                                                select schema_name from information_schema.schemata
                                                ";
                var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    var ownerName = sqlDataReader.GetString(0);
                    owners.Add(ownerName);
                }
            }

            return owners;
        }

        public override List<Table> GetTables(string owner)
        {
            var tables = new List<Table>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                var tableCommand = conn.CreateCommand();
                tableCommand.CommandText = String.Format("select table_name from information_schema.tables where table_type like 'BASE TABLE' and TABLE_SCHEMA = '{0}'", owner);
                var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (sqlDataReader.Read())
                {
                    var tableName = sqlDataReader.GetString(0);
                    tables.Add(new Table
                    {
                        Name = tableName,
                        OwnerSchema = owner,
                        DatabaseName = this.DatabaseName
                    });
                }
            }
            tables.Sort((x, y) => x.Name.CompareTo(y.Name));
            return tables;
        }
        public override List<string> GetSequences(string owner)
        {
            return null;
        }
        public string GetSequences(string tablename, string owner, string column)
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            string tableName = "";
            using (conn)
            {
                NpgsqlCommand seqCommand = conn.CreateCommand();
                seqCommand.CommandText = @"select 
b.sequence_name
from
information_schema.columns a
inner join information_schema.sequences b on a.column_default like 'nextval(\''||b.sequence_name||'%'
where
a.table_schema='" + owner + "' and a.table_name='" + tablename + "' and a.column_name='" + column + "'";
                NpgsqlDataReader seqReader = seqCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (seqReader.Read())
                {
                    tableName = seqReader.GetString(0);

                    // sequences.Add(tableName);
                }
            }
            return tableName;
        }
        public List<string> GetSequences(List<Table> tables)
        {
            var sequences = new List<string>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                NpgsqlCommand seqCommand = conn.CreateCommand();
                seqCommand.CommandText = "select sequence_name from information_schema.sequences";
                NpgsqlDataReader seqReader = seqCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (seqReader.Read())
                {
                    string tableName = seqReader.GetString(0);

                    sequences.Add(tableName);
                }
            }
            return sequences;
        }

        #endregion

        public override PrimaryKey DeterminePrimaryKeys(Table table)
        {
            var primaryKeys = table.Columns.Where(x => x.IsPrimaryKey.Equals(true)).ToList();

            if (primaryKeys.Count() == 1)
            {
                Column c = primaryKeys.First();
                var key = new PrimaryKey
                {
                    Type = PrimaryKeyType.PrimaryKey,
                    Columns = { c }
                };
                return key;
            }

            if (primaryKeys.Count() > 1)
            {
                var key = new PrimaryKey
                {
                    Type = PrimaryKeyType.CompositeKey,
                    Columns = primaryKeys
                };

                return key;
            }

            return null;
        }

        public override IList<ForeignKey> DetermineForeignKeyReferences(Table table)
        {
            var foreignKeys = table.Columns.Where(x => x.IsForeignKey).Distinct()
                                   .Select(c => new ForeignKey
                                   {
                                       Name = c.ConstraintName,
                                       References = GetForeignKeyReferenceTableName(table.Name, c.Name),
                                       Columns = DetermineColumnsForForeignKey(table.Columns, c.ConstraintName)
                                   }).ToList();

            Table.SetUniqueNamesForForeignKeyProperties(foreignKeys);

            return foreignKeys;
        }

        /// <summary>
        /// Search for one or more columns that make up the foreign key.
        /// </summary>
        /// <param name="columns">All columns that could be used for the foreign key</param>
        /// <param name="foreignKeyName">Name of the foreign key constraint</param>
        /// <returns>List of columns associated with the foreign key</returns>
        /// <remarks>Composite foreign key will return multiple columns</remarks>
        private IList<Column> DetermineColumnsForForeignKey(IList<Column> columns, string foreignKeyName)
        {
            return (from c in columns
                    where c.IsForeignKey && c.ConstraintName == foreignKeyName
                    select c).ToList();
        }

        private string GetForeignKeyReferenceTableName(string selectedTableName, string columnName)
        {
            var conn = new Npgsql.NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                NpgsqlCommand tableCommand = conn.CreateCommand();
                tableCommand.CommandText = String.Format(
                    @"
                        select pk.table_name
                        from information_schema.referential_constraints c
                        inner join information_schema.table_constraints fk on c.constraint_name = fk.constraint_name
                        inner join information_schema.table_constraints pk on c.unique_constraint_name = pk.constraint_name
                        inner join information_schema.key_column_usage cu on c.constraint_name = cu.constraint_name
                        inner join (
                        select i1.table_name, i2.column_name
                        from information_schema.table_constraints i1
                        inner join information_schema.key_column_usage i2 on i1.constraint_name = i2.constraint_name
                        where i1.constraint_type = 'PRIMARY KEY'
                        ) pt on pt.table_name = pk.table_name
                        where fk.table_name = '{0}' and cu.column_name = '{1}'",
                    selectedTableName, columnName);
                object referencedTableName = tableCommand.ExecuteScalar();

                return (string)referencedTableName;
            }
        }



        // http://blog.sqlauthority.com/2006/11/01/sql-server-query-to-display-foreign-key-relationships-and-name-of-the-constraint-for-each-table-in-database/
        private IList<HasMany> DetermineHasManyRelationships(Table table)
        {
            var hasManyRelationships = new List<HasMany>();
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText =
                        String.Format(
                            @"
                        select DISTINCT
	                         b.TABLE_NAME,
	                         c.TABLE_NAME
                        from
	                        INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS a
	                        join
	                        INFORMATION_SCHEMA.TABLE_CONSTRAINTS b
	                        on
	                        a.CONSTRAINT_SCHEMA = b.CONSTRAINT_SCHEMA and
	                        a.UNIQUE_CONSTRAINT_NAME = b.CONSTRAINT_NAME
	                        join
	                        INFORMATION_SCHEMA.TABLE_CONSTRAINTS c
	                        on
	                        a.CONSTRAINT_SCHEMA = c.CONSTRAINT_SCHEMA and
	                        a.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        where
	                        b.TABLE_NAME = '{0}'
                        order by
	                        1,2",
                            table.Name);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        hasManyRelationships.Add(new HasMany
                        {
                            Reference = reader.GetString(1)
                        });
                    }

                    return hasManyRelationships;
                }
            }
        }
    }
}
