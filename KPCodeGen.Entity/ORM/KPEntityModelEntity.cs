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
using System.ComponentModel;
using NHibernate.Criterion;
using KPCodeGen.Entity.Base;
using KPCodeGen.Enumerator;
using KPCodeGen.Attibute;
using KPCodeGen.Attibute.Converter;

namespace KPCodeGen.Entity.ORM
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    [ActiveRecord(Table = "KPENTITYMODEL")]
    public class KPEntityModelEntity : KPEntitySQLLiteBase<KPEntityModelEntity>
    {
        [Browsable(false)]
        [PrimaryKey(Column = "ID_KPENTITYMODEL")]
        public virtual int? Id { get; set; }

        [Browsable(false)]
        [KPDisplayName("KPEntity_Mapping_FK")]
        [BelongsTo("ID_KPMAPPING", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        public virtual KPMappingEntity objIdKPMapping { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Title")]
        [KPDescription("KPEntity_HeaderName_Description")]
        [Property(Column = "HEADER_NAME", Length = 255)]
        public virtual string HeaderName { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Width")]
        [KPDescription("KPEntity_Width_Description")]
        [Property(Column = "WIDTH")]
        public virtual int Width { get; set; }

        [DefaultValue(KPMaskTypeClassEnum.ALPHANUMERIC)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_MaskType")]
        [KPDescription("KPEntity_MaskType_Description")]
        [Property(Column = "MASK_TYPE", Length = 255)]
        public virtual KPMaskTypeClassEnum? MaskType { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [TypeConverter(typeof(StringComboBoxTypeConverter))]
        [KPDisplayName("KPEntity_FieldNameDescription")]
        [KPDescription("KPEntity_FieldNameDescription_Description")]
        [Property(Column = "FIELD_NAME_DESCRIPTION", Length = 255)]
        public virtual string FieldNameDescription { get; set; }

        [DefaultValue(false)]
        [Browsable(false)]
        [KPCategory("KPEntity_Category_Property")]
        [Property(Column = "IS_EDITABLE")]
        public virtual bool IsEditable { get; set; }

        [DefaultValue(true)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_IsSortable")]
        [KPDescription("KPEntity_IsSortable_Description")]
        [Property(Column = "IS_SORTABLE")]
        public virtual bool IsSortable { get; set; }

        [DefaultValue(true)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_IsVisible")]
        [KPDescription("KPEntity_IsVisible_Description")]
        [Property(Column = "IS_VISIBLE")]
        public virtual bool IsVisible { get; set; }

    }
}
