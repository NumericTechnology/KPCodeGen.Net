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
 
using System.Collections.Generic;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Core.Delegates;
using System.Linq;

namespace KPCodeGen.Core.Reader
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public abstract class AbstractMetadataReader
    {
        public event ColumnsData ColumnsData;
        protected abstract IList<Column> GetTablesDetails(Table table, string owner);
        public abstract List<Table> GetTables(string owner);
        public abstract IList<string> GetOwners();
        public abstract List<string> GetSequences(string owner);
        public abstract PrimaryKey DeterminePrimaryKeys(Table table);
        public abstract IList<ForeignKey> DetermineForeignKeyReferences(Table table);

        public string DatabaseName { get; protected set; }

        public IList<Column> GetTableDetails(Table table)
        {
            IList<Column> columnsData = GetTablesDetails(table, table.OwnerSchema);

            columnsData = columnsData.OrderByDescending(x => x.IsPrimaryKey).
                                            ThenByDescending(x => x.IsForeignKey).
                                                ThenBy(x => x.Name).ToList();

            if (ColumnsData != null)
                columnsData = ColumnsData(table, columnsData);

            return columnsData;
        }

        public IList<Column> GetTableDetails(string tableName, string owner)
        {
            Table table = GetTable(tableName, owner);
            if (table != null)
                return GetTableDetails(table);

            return new List<Column>();
        }

        public Table GetTable(string tableName, string owner)
        {
            IList<Table> tableList = GetTables(owner);

            var tableNameObj = tableList.FirstOrDefault(x => x.Name.Equals(tableName));

            return tableNameObj;
        }
    }
}