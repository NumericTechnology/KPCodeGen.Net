﻿/*
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
using KPCodeGen.Attibute;

namespace KPCodeGen.Entity.ORM
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    [ActiveRecord(Table = "KPFORMITEMCOMBO")]
    public class KPFormItemComboEntity : KPEntitySQLLiteBase<KPFormItemComboEntity>
    {
        [Browsable(false)]
        [PrimaryKey(Column = "ID_KPFORMITEMCOMBO")]
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

        [KPCategory("KPEntity_Category_Property")]
        [Property(Column = "DATA_NAME", Length = 255)]
        public virtual string DataName { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [Property(Column = "DATA_VALUE", Length = 255)]
        public virtual string DataValue { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [Property(Column = "NAMESPACE_ENUM", Length = 255)]
        public virtual string NamespaceEnum { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Width")]
        [KPDescription("KPEntity_Width_Description")]
        [Property(Column = "WIDTH")]
        public virtual int Width { get; set; }

        #region Events
        [DefaultValue(false)]
        [KPCategory("KPEntity_Events")]
        [Property(Column = "KPEVENT_COMBOSELECTCHANGE")]
        public virtual bool KPEventComboSelectChange { get; set; }
        #endregion

    }
}
