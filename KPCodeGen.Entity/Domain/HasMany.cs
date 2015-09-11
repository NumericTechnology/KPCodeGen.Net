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
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class HasMany
    {
        public HasMany()
        {
            AllReferenceColumns = new List<string>();
        }

        /// <summary>
        /// An identifier for a constraint so that we might detect from querying the database whether a relationship has one is a composite key.
        /// </summary>
        public string ConstraintName { get; set; }
        public string Reference { get; set; }

        /// <summary>
        /// In support of relationships that use composite keys.
        /// </summary>
        public IList<string> AllReferenceColumns { get; set; }

        /// <summary>
        /// Provide the first (and very often the only) column used to define a foreign key relationship.
        /// </summary>
        public string ReferenceColumn
        {
            get { return AllReferenceColumns.Count > 0 ? AllReferenceColumns[0] : ""; }
            set { AllReferenceColumns = new List<string> { value }; }
        }

        public string PKTableName { get; set; }
    }
}
