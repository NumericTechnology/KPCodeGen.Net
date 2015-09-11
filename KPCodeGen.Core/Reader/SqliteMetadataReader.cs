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
using System.Data.SQLite;
using System.Linq;
using  KPCodeGen.Entity.Domain;
using System.Data.Common;
using KPCodeGen.Enumerator;

namespace KPCodeGen.Core.Reader
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class SqliteMetadataReader : AbstractMetadataReader
    {
        private readonly string _connectionString;

        public SqliteMetadataReader(string connectionStr)
        {
            _connectionString = connectionStr;
            DbConnectionStringBuilder connStr = new DbConnectionStringBuilder();
            connStr.ConnectionString = _connectionString;
            DatabaseName = connStr["Database"] as String;
        }

        protected override IList<Column> GetTablesDetails(Table table, string owner)
        {
            var columns = new List<Column>();

            using (var sqlCon = new SQLiteConnection(_connectionString))
            {
                try
                {
                    using (var tableDetailsCommand = sqlCon.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format("SELECT * FROM {0}", table.Name);
                        sqlCon.Open();

                        var dr = tableDetailsCommand.ExecuteReader(CommandBehavior.SchemaOnly);
                        var dt = dr.GetSchemaTable();
                        var m = new DataTypeMapper();

                        foreach (DataRow row in dt.Rows)
                        {
                            bool isPrimaryKey = (bool)row["IsKey"];
                            columns.Add(
                                    new Column(table)
                                    {
                                        Name = row["ColumnName"].ToString(),
                                        IsNullable = (bool)row["AllowDBNull"],
                                        IsPrimaryKey = (bool)row["IsKey"],
                                        MappedDataType = m.MapFromDBType(DatabaseServerType.SQLite, row["DataTypeName"].ToString(), (int)row["ColumnSize"], null, null, isPrimaryKey),
                                        DataLength = (int)row["ColumnSize"],
                                        DataType = row["DataTypeName"].ToString(),
                                        IsUnique = (bool)row["IsUnique"]
                                    });
                            dr.Close();
                        }
                    }

                    table.OwnerSchema = owner;
                    table.Columns = columns;
                    table.PrimaryKey = DeterminePrimaryKeys(table);
                    table.ForeignKeys = new List<ForeignKey>();// DetermineForeignKeyReferences(table);
                    table.HasManyRelationships = new List<HasMany>();// DetermineHasManyRelationships(table);
                }
                finally
                {
                    sqlCon.Close();
                }
            }

            return columns;
        }

        public override List<Table> GetTables(string owner)
        {
            var tables = new List<Table>();

            using (var sqlCon = new SQLiteConnection(_connectionString))
            {
                sqlCon.Open();
                try
                {
                    using (var tableDetailsCommand = sqlCon.CreateCommand())
                    {
                        tableDetailsCommand.CommandText =
                            "SELECT name FROM sqlite_master WHERE type in ('table', 'view') AND name not like 'sqlite?_%' escape '?'";
                        using (var sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            string tableName = sqlDataReader.GetString(0);
                            while (sqlDataReader.Read())
                            {
                                tables.Add(new Table
                                {
                                    Name = tableName,
                                    OwnerSchema = owner,
                                    DatabaseName = this.DatabaseName
                                });
                            }
                        }
                    }
                }
                finally
                {
                    sqlCon.Close();
                }
            }

            return tables;
        }

        public override IList<string> GetOwners()
        {
            return new List<string> { "master" };
        }

        public override List<string> GetSequences(string owner)
        {
            return new List<string>();
        }

        public override PrimaryKey DeterminePrimaryKeys(Table table)
        {
            var primaryKeys = table.Columns.Where(x => x.IsPrimaryKey.Equals(true)).ToList();

            if (primaryKeys.Count() == 1)
            {
                var c = primaryKeys.First();
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
                                       Name = c.Name,
                                       References = c.ForeignKeyTableName,
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

    }

    public class SqliteDataType
    {
        public SqliteDataType(string sqliteType)
        {
            if (sqliteType.Contains("("))
            {
                var typeSplit = sqliteType.Replace(")", string.Empty).Split('(');
                DataType = typeSplit[0];
                DataLength = int.Parse(typeSplit[1]);
            }
        }
        public string DataType { get; set; }
        public int? DataLength { get; set; }
    }
}