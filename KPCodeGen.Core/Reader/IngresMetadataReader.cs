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
 
using Ingres.Client;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace KPCodeGen.Core.Reader
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class IngresMetadataReader : AbstractMetadataReader
    {
        public IngresMetadataReader(string connectionStr)
        {
            _connectionString = connectionStr;
            DbConnectionStringBuilder connStr = new DbConnectionStringBuilder();
            connStr.ConnectionString = _connectionString;
            DatabaseName = connStr["Database"] as String;
        }

        #region Implementation of IMetadataReader

        protected override IList<Column> GetTablesDetails(Table table, string owner)
        {
            table.OwnerSchema = owner;
            var columns = new List<Column>();
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var tableDetailsCommand = conn.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format("SELECT" +
                                                                        " column_name," +
                                                                        " column_datatype," +
                                                                        " column_nulls," +
                                                                        " column_length," +
                                                                        " column_scale " +
                                                                        "FROM iicolumns " +
                                                                        "WHERE table_owner = '{0}' " +
                                                                        "AND table_name = '{1}' " +
                                                                        "ORDER BY column_sequence",
                                                                        owner, table.Name);

                        using (var sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            while (sqlDataReader.Read())
                            {
                                string columnName = sqlDataReader.GetString(0).TrimEnd();
                                string dataType = sqlDataReader.GetString(1).TrimEnd();
                                bool isNullable = sqlDataReader.GetString(2).Equals("Y", StringComparison.CurrentCultureIgnoreCase);
                                int characterMaxLenth = sqlDataReader.GetInt32(3);
                                int numericPrecision = sqlDataReader.GetInt32(3);
                                int numericScale = sqlDataReader.GetInt32(4);

                                var m = new DataTypeMapper();
                                bool isPrimaryKey = IsPrimaryKey(owner, table.Name, columnName);

                                columns.Add(new Column(table)
                                    {
                                        Name = columnName,
                                        DataType = dataType,
                                        IsNullable = isNullable,
                                        IsPrimaryKey = isPrimaryKey,
                                        IsForeignKey = IsForeignKey(owner, table.Name, columnName),
                                        MappedDataType = m.MapFromDBType(DatabaseServerType.Ingres, dataType, characterMaxLenth, numericPrecision, numericScale, isPrimaryKey),
                                        DataLength = characterMaxLenth,
                                        ConstraintName = GetConstraintName(owner, table.Name, columnName),
                                        DataPrecision = numericPrecision,
                                        DataScale = numericScale
                                    });

                                table.Columns = columns;
                            }
                            table.PrimaryKey = DeterminePrimaryKeys(table);
                            table.ForeignKeys = DetermineForeignKeyReferences(table);
                            table.HasManyRelationships = DetermineHasManyRelationships(table);
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return columns;
        }

        private IList<HasMany> DetermineHasManyRelationships(Table table)
        {
            var hasManyRelationships = new List<HasMany>();
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            using (conn)
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT f.table_name " +
                                                        "FROM iiref_constraints rc " +
                                                        "INNER JOIN iikeys p " +
                                                        "ON p.schema_name = rc.unique_schema_name " +
                                                        "AND p.constraint_name = rc.unique_constraint_name " +
                                                        "INNER JOIN iiconstraints c " +
                                                        "ON c.schema_name = rc.ref_schema_name " +
                                                        "AND c.constraint_name = rc.ref_constraint_name " +
                                                        "INNER JOIN iikeys f " +
                                                        "ON f.constraint_name = rc.ref_constraint_name " +
                                                        "AND p.key_position = f.key_position " +
                                                        "WHERE p.schema_name = '{0}' " +
                                                        "AND p.table_name = '{1}'",
                                                        table.OwnerSchema,
                                                        table.Name);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasManyRelationships.Add(new HasMany
                                {
                                    Reference = reader.GetString(0).TrimEnd(),
                                });
                        }
                    }

                    return hasManyRelationships;
                }
            }
        }

        public override PrimaryKey DeterminePrimaryKeys(Table table)
        {
            IList<Column> primaryKeys = table.Columns.Where(x => x.IsPrimaryKey.Equals(true)).ToList();

            if (primaryKeys.Count() == 1)
            {
                var c = primaryKeys.First();
                var key = new PrimaryKey
                    {
                        Type = PrimaryKeyType.PrimaryKey,
                        Columns =
                            {
                                new Column(table)
                                    {
                                        DataType = c.DataType,
                                        Name = c.Name
                                    }
                            }
                    };
                return key;
            }
            else
            {
                var key = new PrimaryKey
                    {
                        Type = PrimaryKeyType.CompositeKey
                    };
                foreach (var primaryKey in primaryKeys)
                {
                    key.Columns.Add(new Column(table)
                        {
                            DataType = primaryKey.DataType,
                            Name = primaryKey.Name
                        });
                }
                return key;
            }
        }

        public override IList<ForeignKey> DetermineForeignKeyReferences(Table table)
        {
            var constraints = table.Columns.Where(x => x.IsForeignKey.Equals(true)).Select(x => x.ConstraintName).Distinct().ToList();
            var foreignKeys = new List<ForeignKey>();
            constraints.ForEach(c =>
                {
                    var fkColumns = table.Columns.Where(x => x.ConstraintName.Equals(c)).ToArray();
                    var fk = new ForeignKey
                        {
                            Name = fkColumns[0].Name,
                            References = GetForeignKeyReferenceTableName(table.OwnerSchema, table.Name, fkColumns[0].Name),
                            Columns = fkColumns
                        };
                    foreignKeys.Add(fk);
                });

            Table.SetUniqueNamesForForeignKeyProperties(foreignKeys);

            return foreignKeys;
        }

        private bool IsPrimaryKey(string owner, string tableName, string columnName)
        {
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var tableDetailsCommand = conn.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format("SELECT COUNT(0) " +
                                                                        "FROM iikeys k " +
                                                                        "INNER JOIN iiconstraints c " +
                                                                        "ON k.constraint_name = c.constraint_name " +
                                                                        "WHERE c.constraint_type = 'P' " +
                                                                        "AND k.schema_name = '{0}' " +
                                                                        "AND k.table_name = '{1}' " +
                                                                        "AND k.column_name = '{2}'",
                                                                        owner, tableName, columnName);
                        var obj = tableDetailsCommand.ExecuteScalar();

                        int result;
                        if (obj != null &&
                            Int32.TryParse(obj.ToString(), out result))
                            return result > 0;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        private bool IsForeignKey(string owner, string tableName, string columnName)
        {
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var tableDetailsCommand = conn.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format("SELECT COUNT(0) " +
                                                                        "FROM iikeys k " +
                                                                        "INNER JOIN iiconstraints c " +
                                                                        "ON k.constraint_name = c.constraint_name " +
                                                                        "WHERE c.constraint_type = 'R' " +
                                                                        "AND k.schema_name = '{0}' " +
                                                                        "AND k.table_name = '{1}' " +
                                                                        "AND k.column_name = '{2}'",
                                                                        owner, tableName, columnName);
                        var obj = tableDetailsCommand.ExecuteScalar();

                        int result;
                        if (obj != null &&
                            Int32.TryParse(obj.ToString(), out result))
                            return result > 0;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        private string GetConstraintName(string owner, string tableName, string columnName)
        {
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var tableDetailsCommand = conn.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format("SELECT k.constraint_name " +
                                                                        "FROM iikeys k " +
                                                                        "INNER JOIN iiconstraints c " +
                                                                        "ON k.constraint_name = c.constraint_name " +
                                                                        "WHERE c.constraint_type = 'R' " +
                                                                        "AND k.schema_name = '{0}' " +
                                                                        "AND k.table_name = '{1}' " +
                                                                        "AND k.column_name = '{2}'",
                                                                        owner, tableName, columnName);
                        var result = tableDetailsCommand.ExecuteScalar();
                        return result == null ? String.Empty : result.ToString();
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private string GetForeignKeyReferenceTableName(string owner, string tableName, string columnName)
        {
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var tableCommand = conn.CreateCommand())
                    {
                        tableCommand.CommandText = String.Format("SELECT p.table_name " +
                                                                 "FROM iiref_constraints rc " +
                                                                 "INNER JOIN iikeys p " +
                                                                 "ON p.schema_name = rc.unique_schema_name " +
                                                                 "AND p.constraint_name = rc.unique_constraint_name " +
                                                                 "INNER JOIN iiconstraints c " +
                                                                 "ON c.schema_name = rc.ref_schema_name " +
                                                                 "AND c.constraint_name = rc.ref_constraint_name " +
                                                                 "INNER JOIN iikeys f " +
                                                                 "ON f.schema_name = rc.ref_schema_name " +
                                                                 "AND f.constraint_name = rc.ref_constraint_name " +
                                                                 "AND p.key_position = f.key_position " +
                                                                 "WHERE f.schema_name = '{0}' " +
                                                                 "AND f.table_name = '{1}' " +
                                                                 "AND f.column_name = '{2}'",
                                                                 owner,
                                                                 tableName,
                                                                 columnName);
                        return tableCommand.ExecuteScalar().ToString();
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public override List<Table> GetTables(string owner)
        {
            var tables = new List<Table>();
            var conn = new IngresConnection(_connectionString);
            conn.Open();
            try
            {
                using (conn)
                {
                    var tableCommand = conn.CreateCommand();
                    tableCommand.CommandText = String.Format("SELECT table_name " +
                                                             "FROM iitables " +
                                                             "WHERE table_owner = '{0}' " +
                                                             "AND table_type in ('T', 'V') " +
                                                             "AND table_name NOT LIKE 'ii%'",
                                                             owner);

                    var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        var tableName = sqlDataReader.GetString(0).TrimEnd();
                        tables.Add(new Table
                        {
                            Name = tableName,
                            OwnerSchema = owner,
                            DatabaseName = this.DatabaseName
                        });
                    }
                }
                tables.Sort((x, y) => String.CompareOrdinal(x.Name, y.Name));
            }
            finally
            {
                conn.Close();
            }
            return tables;
        }

        public override IList<string> GetOwners()
        {
            var owners = new List<string>();
            var conn = new IngresConnection(_connectionString);

            conn.Open();
            try
            {
                using (conn)
                {
                    var tableCommand = conn.CreateCommand();
                    tableCommand.CommandText = "SELECT DISTINCT table_owner FROM iitables WHERE table_owner <> '$ingres'";
                    var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        var ownerName = sqlDataReader.GetString(0).TrimEnd();
                        owners.Add(ownerName);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return owners;
        }

        public override List<string> GetSequences(string owner)
        {
            return new List<string>();
        }

        #endregion

        private readonly string _connectionString;
    }
}