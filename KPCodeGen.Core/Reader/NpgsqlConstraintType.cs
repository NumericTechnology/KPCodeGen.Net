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

namespace KPCodeGen.Core.Reader
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public sealed class NpgsqlConstraintType
    {
        public static readonly NpgsqlConstraintType PrimaryKey = new NpgsqlConstraintType(1, "PRIMARY KEY");
        public static readonly NpgsqlConstraintType ForeignKey = new NpgsqlConstraintType(2, "FOREIGN KEY");
        public static readonly NpgsqlConstraintType Check = new NpgsqlConstraintType(3, "CHECK");
        public static readonly NpgsqlConstraintType Unique = new NpgsqlConstraintType(4, "UNIQUE");
        private readonly String name;
        private readonly int value;

        private NpgsqlConstraintType(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }
    }
}
