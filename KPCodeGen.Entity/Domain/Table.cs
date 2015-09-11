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
using System.Linq;

namespace KPCodeGen.Entity.Domain
{
    /// <summary>
    /// Defines a database table entity.
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class Table : ICloneable
    {
        public Table()
        {
            ForeignKeys = new List<ForeignKey>();
            Columns = new List<Column>();
            HasManyRelationships = new List<HasMany>();
        }

        public string Name { get; set; }
        public string OwnerSchema { get; set; }
        public string DatabaseName { get; set; }

        public PrimaryKey PrimaryKey { get; set; }

        public IList<ForeignKey> ForeignKeys { get; set; }
        public IList<Column> Columns { get; set; }
        public IList<HasMany> HasManyRelationships { get; set; }

        public override string ToString() { return Name; }

        /// <summary>
        /// When one table has multiple fields that represent different relationships to the same foreign entity, it is required to give them unique names.
        /// </summary>
        public static void SetUniqueNamesForForeignKeyProperties(IList<ForeignKey> foreignKeys)
        {
            // Create unique names foreign keys that access the same table more than once.
            var groupedForeignKeys = (from fk in foreignKeys
                                      group fk by fk.References
                                          into g
                                          where g.Count() > 1
                                          select g).ToList();

            foreach (var group in groupedForeignKeys)
            {
                foreach (var fk in group)
                {
                    // Use the field name instead of the table name
                    fk.UniquePropertyName = fk.Columns.First().Name;
                }
            }

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}