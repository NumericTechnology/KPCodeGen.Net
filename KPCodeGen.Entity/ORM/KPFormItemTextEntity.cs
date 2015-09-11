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
 
using Castle.ActiveRecord;
using KPCodeGen.Attibute;
using KPCodeGen.Entity.Base;
using KPCodeGen.Enumerator;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KPCodeGen.Entity.ORM
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    [ActiveRecord(Table = "KPFORMITEMTEXT")]
    public class KPFormItemTextEntity : KPEntitySQLLiteBase<KPFormItemTextEntity>
    {
        [Browsable(false)]
        [PrimaryKey(Column = "ID_KPFORMITEMTEXT")]
        public virtual int? Id { get; set; }

        [Browsable(false)]
        [KPDisplayName("KPEntity_Mapping_FK")]
        [BelongsTo("ID_KPMAPPING", Lazy = FetchWhen.OnInvoke, NotNull = true)]
        public virtual KPMappingEntity objIdKPMapping { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Title")]
        [KPDescription("KPEntity_Title_Description")]
        [Property(Column = "TITLE", Length = 255)]
        public virtual string Title { get; set; }

        [DefaultValue(false)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_IsRequired")]
        [KPDescription("KPEntity_IsRequired_Description")]
        [Property(Column = "IS_REQUIRED")]
        public virtual bool IsRequired { get; set; }

        [DefaultValue(false)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_IsMultiline")]
        [KPDescription("KPEntity_IsMultiline_Description")]
        [Property(Column = "IS_MULTILINE")]
        public virtual bool IsMultiline { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Width")]
        [KPDescription("KPEntity_Width_Description")]
        [Property(Column = "WIDTH")]
        public virtual int Width { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Height")]
        [KPDescription("KPEntity_Height_Description")]
        [Property(Column = "HEIGHT")]
        public virtual int Height { get; set; }

        [DefaultValue(KPMaskTypeClassEnum.ALPHANUMERIC)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_MaskType")]
        [KPDescription("KPEntity_MaskType_Description")]
        [Property(Column = "MASK_TYPE", Length = 255)]
        public virtual KPMaskTypeClassEnum? MaskType { get; set; } 

        #region Events
        [DefaultValue(false)]
        [KPCategory("KPEntity_Events")]
        [KPDescription("KPEntity_KPEventTextLostFocus_Description")]
        [Property(Column = "KPEVENT_TEXTLOSTFOCUS")]
        public virtual bool KPEventTextLostFocus { get; set; }
        #endregion
    }
}
