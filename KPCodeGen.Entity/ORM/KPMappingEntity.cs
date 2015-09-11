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
using System.Text;
using System;
using Castle.ActiveRecord;

namespace KPCodeGen.Entity.ORM
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    [ActiveRecord(Table = "KPMAPPING")]
    public class KPMappingEntity : ActiveRecordValidationBase<KPMappingEntity>
    {
        [PrimaryKey(Column = "ID_KPMAPPING")]
        public virtual int? Id { get; set; }

        [Property(Column = "SCHEMA_NAME", Length = 255, NotNull = true)]
        public virtual string OwnerSchema { get; set; }

        [Property(Column = "DATABASE_NAME", Length = 255, NotNull = true)]
        public virtual string Database { get; set; }

        [Property(Column = "TABLE_NAME", Length = 255, NotNull = true)]
        public virtual string Table { get; set; }

        [Property(Column = "COLUMN_NAME", Length = 255, NotNull = true)]
        public virtual string Column { get; set; }

        [Property(Column = "CSHARP_TYPE", Length = 255)]
        public virtual string CSharpType { get; set; }

        [Property(Column = "DISPLAY_NAME", Length = 255)]
        public virtual string DisplayName { get; set; }

        [Property(Column = "COLUMN_TYPE_VIEW", Length = 255)]
        public virtual string ColumnTypeView { get; set; }

        [Property(Column = "COLUMN_TYPE_FORM", Length = 255)]
        public virtual string ColumnTypeForm { get; set; }
    }
}
