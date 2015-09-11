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
    [ActiveRecord(Table = "KPFORMITEMZOOM")]
    public class KPFormItemZoomEntity : KPEntitySQLLiteBase<KPFormItemZoomEntity>
    {
        [Browsable(false)]
        [PrimaryKey(Column = "ID_KPFORMITEMZOOM")]
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
        [KPDisplayName("KPEntity_DescriptionWidth")]
        [KPDescription("KPEntity_DescriptionWidth_Description")]
        [Property(Column = "DESCRIPTION_WIDTH")]
        public virtual int DescriptionWidth { get; set; }

        [DefaultValue(KPMaskTypeClassEnum.ALPHANUMERIC)]
        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_MaskType")]
        [KPDescription("KPEntity_MaskType_Description")]
        [Property(Column = "MASK_TYPE", Length = 255)]
        public virtual KPMaskTypeClassEnum? MaskType { get; set; }

        [KPCategory("KPEntity_Category_Property")]
        [KPDisplayName("KPEntity_Width")]
        [KPDescription("KPEntity_Width_Description")]
        [Property(Column = "WIDTH")]
        public virtual int Width { get; set; }

        #region Zoom Configuration
        [KPCategory("KPEntity_Category_ZoomConfig")]
        [KPDisplayName("KPEntity_Title_Zoom")]
        [KPDescription("KPEntity_Title_Zoom_Description")]
        [Property(Column = "WINDOW_TITLE")]
        public virtual string WindowTitle { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [TypeConverter(typeof(StringComboBoxTypeConverter))]
        [KPDisplayName("KPEntity_FieldReturnId")]
        [KPDescription("KPEntity_FieldReturnId_Description")]
        [Property(Column = "FIELD_RETURN_ID")]
        public virtual string FieldReturnId { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [TypeConverter(typeof(StringComboBoxTypeConverter))]
        [KPDisplayName("KPEntity_FieldReturnText")]
        [KPDescription("KPEntity_FieldReturnText_Description")]
        [Property(Column = "FIELD_RETURN_TEXT")]
        public virtual string FieldReturnText { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [TypeConverter(typeof(StringComboBoxTypeConverter))]
        [KPDisplayName("KPEntity_SearchByField")]
        [KPDescription("KPEntity_SearchByField_Description")]
        [Property(Column = "SEARCH_BY_FIELD")]
        public virtual string SearchByField { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [TypeConverter(typeof(StringComboBoxTypeConverter))]
        [KPDisplayName("KPEntity_DisplaySearchField")]
        [KPDescription("KPEntity_DisplaySearchField_Description")]
        [Property(Column = "DISPLAY_SEARCH_FIELD")]
        public virtual string DisplaySearchField { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [KPDisplayName("KPEntity_WidthZoom")]
        [KPDescription("KPEntity_WidthZoom_Description")]
        [Property(Column = "WIDTH_ZOOM")]
        public virtual int WidthZoom { get; set; }

        [KPCategory("KPEntity_Category_ZoomConfig")]
        [KPDisplayName("KPEntity_HeightZoom")]
        [KPDescription("KPEntity_HeightZoom_Description")]
        [Property(Column = "HEIGHT_ZOOM")]
        public virtual int HeightZoom { get; set; }
        #endregion Zoom Configuration

        #region Events
        [DefaultValue(false)]
        [KPCategory("KPEntity_Events")]
        [KPDescription("KPEntity_KPEventZoomLostFocus_Description")]
        [Property(Column = "KPEVENT_ZOOMLOSTFOCUS")]
        public virtual bool KPEventZoomLostFocus { get; set; }
        #endregion
    }
}
