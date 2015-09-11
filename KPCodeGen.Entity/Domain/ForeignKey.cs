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
using System.Text;

namespace KPCodeGen.Entity.Domain
{
    /// <summary>
    /// Defines a foreign key entity.
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class ForeignKey
    {
        /// <summary>
        /// Foreign key constraint name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// One or more columns linked to the foreign key (more than one being a composite fk)
        /// </summary>
        public IList<Column> Columns { get; set; }

        /// <summary>
        /// Defines what table the foreign key references.
        /// </summary>
        public string References { get; set; }

        /// <summary>
        /// When one table has multiple fields that represent different relationships to the same foreign entity, it is required to give them unique names.
        /// </summary>
        public string UniquePropertyName { get; set; }

        public bool IsNullable { get; set; }

        public override string ToString() { return Name; }
    }
}
