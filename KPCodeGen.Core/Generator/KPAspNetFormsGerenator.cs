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
 
using KPCodeGen.Entity.Domain;
using KPCodeGen.Core.Reader;
using KPCodeGen.Core.Util;
using KPCodeGen.Entity.ORM;
using KPCodeGen.Entity.Extension;
using KPCodeGen.Enumerator;
using KPCodeGen.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPCodeGen.Core.Generator
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPAspNetFormsGerenator : AbstractGenerator
    {

        public KPAspNetFormsGerenator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
            : base(applicationSettings, metadataReader)
        {
        }

        public override string Generate(Table table)
        {
            return GetStructAspNetForms(table);
        }

        private string GetStructAspNetForms(Table table)
        {
            StringBuilder sbFile = new StringBuilder();
            sbFile.AppendFormat(@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind=""Frm{0}.aspx.cs"" ", table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"MasterPageFile=""~/Master/Register.Master"" Inherits=""{0}.Frm{1}"" %>", ApplicationSettings.NamespaceWebProject, table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"<asp:Content ID=""ContentHead"" ContentPlaceHolderID=""HeadRegister"" runat=""server"">").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"</asp:Content>").Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"<asp:Content ID=""ContentBody"" ContentPlaceHolderID=""FormRegisterBody"" runat=""server"">").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}<KP:KPFormControl runat=""server"" ID=""formControl"" TypeBONamespace=""{1}.{2}BO"">", UtilCharacter.TAB_SPACE, this.ApplicationSettings.NamespaceEntityBO, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}<KPColumnsModel>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            int width = 0;
            string maskConcat = String.Empty;
            List<ZoomConfig> zoomConfigList = new List<ZoomConfig>();
            foreach (Column column in table.Columns)
            {
                KPMaskTypeClassEnum? maskType = null;
                maskConcat = String.Empty;
                width = 0;

                if (column.EntityComponentForm != null)
                {
                    switch (column.ColumnTypeForm)
                    {
                        case KPComponentsFormsEnum.KPFormItemText:
                            maskType = ((KPFormItemTextEntity)column.EntityComponentForm).MaskType;
                            width = ((KPFormItemTextEntity)column.EntityComponentForm).Width;
                            break;
                        case KPComponentsFormsEnum.KPFormItemZoom:
                            // Adiciona na Configuração do Zoom apenas se a Coluna é do Tipo Componente Zoom e é FK
                            if (column.IsForeignKey)
                            {
                                zoomConfigList.Add(new ZoomConfig(column, column.EntityComponentForm as KPFormItemZoomEntity));
                            }
                            maskType = ((KPFormItemZoomEntity)column.EntityComponentForm).MaskType;
                            width = ((KPFormItemZoomEntity)column.EntityComponentForm).Width;
                            break;
                    }

                    if (maskType.HasValue && maskType.Value != KPMaskTypeClassEnum.ALPHANUMERIC)
                        maskConcat = String.Format(@" Mask=""{0}""", maskType.Value.ToString());
                }

                if (width <= 0)
                    width = GetWidthComponent(maskType, column);

                string propertyConcat = String.Format(@" Width=""{0}""", width);
                switch (column.ColumnTypeForm)
                {
                    case KPComponentsFormsEnum.KPFormItemButton:
                        propertyConcat += GetControlKPFormItemButtonEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemCheckBox:
                        propertyConcat += GetControlKPFormItemCheckBoxEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemCombo:
                        propertyConcat += GetControlKPFormItemComboEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemEntity:
                        propertyConcat += GetControlKPFormItemEntityEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemGrid:
                        propertyConcat += GetControlKPFormItemGridEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemPassword:
                        propertyConcat += GetControlKPFormItemPasswordEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemText:
                        propertyConcat += GetControlKPFormItemTextEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemZoom:
                        if (column.IsForeignKey)
                        {
                            string fieldName = String.Format("obj{0}", column.Name.GetFormattedText());
                            propertyConcat += String.Format(@" ZoomIDConfig=""Zoom{0}""", fieldName);
                            propertyConcat += GetControlKPFormItemZoomEntity(column);
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemDateTime:
                        propertyConcat += GetControlKPFormItemDateTimeEntity(column);
                        break;
                    case KPComponentsFormsEnum.KPFormItemSpacer:
                    case KPComponentsFormsEnum.KPFormItemKey:
                    default:
                        break;
                }

                string fieldNameId = column.Name.GetFormattedText();
                if (column.IsForeignKey)
                    fieldNameId = String.Format("obj{0}", fieldNameId);

                sbFile.AppendFormat(@"{0}{0}{0}<KP:{1} FieldName=""{2}""{3}{4} />",
                    UtilCharacter.TAB_SPACE, column.ColumnTypeForm, fieldNameId, propertyConcat, maskConcat).Append(UtilCharacter.NEW_LINE);
            }

            sbFile.AppendFormat(@"{0}{0}</KPColumnsModel>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            foreach (ZoomConfig zoomConfig in zoomConfigList)
            {
                sbFile.AppendFormat(@"{0}{0}<KPFormZoomConfig>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

                IList<Column> tableFKcolumnList = MetadataReader.GetTableDetails(zoomConfig.Column.ForeignKeyTableName, zoomConfig.Column.Table.OwnerSchema);

                string fieldReturnText = zoomConfig.Column.ForeignKeyColumnName;
                var columnsString = tableFKcolumnList.Where(x => x.IsPrimaryKey);
                if (columnsString != null)
                    fieldReturnText = columnsString.First().Name;

                string fieldNameId = String.Format("obj{0}", zoomConfig.Column.Name.GetFormattedText());
                string entityTableName = zoomConfig.Column.ForeignKeyTableName.GetFormattedText();
                sbFile.AppendFormat(@"{0}{0}{0}<KP:KPFormZoomModel ZoomID=""Zoom{1}"" TypeEntityNamespace=""{2}.{3}""", UtilCharacter.TAB_SPACE, fieldNameId, ApplicationSettings.NamespaceEntity, entityTableName);

                if (!String.IsNullOrWhiteSpace(zoomConfig.ZoomEntity.Title))
                    sbFile.AppendFormat(@" WindowTitle=""{0}""", zoomConfig.ZoomEntity.Title).Append(UtilCharacter.NEW_LINE);
                else
                    sbFile.AppendFormat(@" WindowTitle=""{0}""", GlobalizationLanguage.GetString("KPEntity_Title")).Append(UtilCharacter.NEW_LINE);

                sbFile.AppendFormat(@"{0}{0}{0}FieldReturnId=""{1}""", UtilCharacter.TAB_SPACE, zoomConfig.ZoomEntity.FieldReturnId);

                if (!String.IsNullOrWhiteSpace(zoomConfig.ZoomEntity.FieldReturnText))
                    sbFile.AppendFormat(@" FieldReturnText=""{0}""", zoomConfig.ZoomEntity.FieldReturnText);

                if (!String.IsNullOrWhiteSpace(zoomConfig.ZoomEntity.DisplaySearchField))
                    sbFile.AppendFormat(@" DisplaySearchField=""{0}""", zoomConfig.ZoomEntity.DisplaySearchField);

                if (!String.IsNullOrWhiteSpace(zoomConfig.ZoomEntity.SearchByField))
                    sbFile.AppendFormat(@" SearchByField=""{0}""", zoomConfig.ZoomEntity.SearchByField);

                if (zoomConfig.ZoomEntity.WidthZoom > 0)
                    sbFile.AppendFormat(@" WidthZoom=""{0}""", zoomConfig.ZoomEntity.WidthZoom);
                else
                    sbFile.AppendFormat(@" WidthZoom=""400""", zoomConfig.ZoomEntity.WidthZoom);

                if (zoomConfig.ZoomEntity.HeightZoom > 0)
                    sbFile.AppendFormat(@" HeightZoom=""{0}""", zoomConfig.ZoomEntity.HeightZoom);
                else
                    sbFile.AppendFormat(@" HeightZoom=""210""", zoomConfig.ZoomEntity.HeightZoom);

                sbFile.AppendFormat(@">").Append(UtilCharacter.NEW_LINE);

                sbFile.AppendFormat(@"{0}{0}{0}{0}<KPZoomFieldsConfig>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

                foreach (Column columnFK in tableFKcolumnList)
                {
                    if (columnFK.IsForeignKey)
                    {
                        sbFile.AppendFormat(@"{0}{0}{0}{0}{0}<KP:KPEntityModel FieldName=""obj{1}"" Width=""100"" />", UtilCharacter.TAB_SPACE, columnFK.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                    }
                    else
                    {
                        sbFile.AppendFormat(@"{0}{0}{0}{0}{0}<KP:KPColumnModel FieldName=""{1}"" Width=""100"" />", UtilCharacter.TAB_SPACE, columnFK.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                    }
                }

                sbFile.AppendFormat(@"{0}{0}{0}{0}</KPZoomFieldsConfig>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbFile.AppendFormat(@"{0}{0}{0}</KP:KPFormZoomModel>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbFile.AppendFormat(@"{0}{0}</KPFormZoomConfig>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }
            sbFile.AppendFormat(@"{0}</KP:KPFormControl>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"</asp:Content>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }

        #region Private Methods
        private int GetWidthComponent(KPMaskTypeClassEnum? markType, Column column)
        {
            int width = 100;
            if (column.IsPrimaryKey)
                return 100;

            if (Int32.TryParse(column.DataSize, out width))
                width = width * 9;
            else
                width = 400;

            if (width > 400)
                width = 400;

            if (!markType.HasValue)
                return width;

            switch (markType.Value)
            {
                case KPMaskTypeClassEnum.CGC:
                case KPMaskTypeClassEnum.CNPJ:
                case KPMaskTypeClassEnum.CPF:
                    return 135;
                case KPMaskTypeClassEnum.DATE:
                    return 90;
                case KPMaskTypeClassEnum.DATEHOUR:
                    return 110;
                case KPMaskTypeClassEnum.DATEMINUTE:
                    return 130;
                case KPMaskTypeClassEnum.DATETIME:
                    return 150;
                case KPMaskTypeClassEnum.INTEGER:
                    return 70;
                case KPMaskTypeClassEnum.POSTCODE:
                    return 70;
                case KPMaskTypeClassEnum.TELEPHONE:
                    return 106;
            }
            return width;
        }

        private string GetControlKPFormItemButtonEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemButtonEntity entity = column.EntityComponentForm as KPFormItemButtonEntity;
            if (entity == null)
            {
                entity = new KPFormItemButtonEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title")
                };
            }

            var title = entity.Title;
            var eventClick = entity.KPEventClick;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (eventClick)
                propertyConcat += String.Format(@" OnKPClick=""{0}_KPClick""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemCheckBoxEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemCheckBoxEntity entity = column.EntityComponentForm as KPFormItemCheckBoxEntity;
            if (entity == null)
            {
                entity = new KPFormItemCheckBoxEntity()
                {
                    Description = GlobalizationLanguage.GetString("KP_Description"),
                    IsRequired = !column.IsNullable
                };
            }

            var isRequired = entity.IsRequired;
            var isGroupStyleDisplay = entity.IsGroupStyleDisplay;
            var description = entity.Description;
            var eventSelect = entity.KPEventCheckBoxSelect;

            if (!String.IsNullOrWhiteSpace(description))
                propertyConcat += String.Format(@" Description=""{0}""", description);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (isGroupStyleDisplay)
                propertyConcat += String.Format(@" IsGroupStyleDisplay=""{0}""", isGroupStyleDisplay.ToString().ToLower());
            if (eventSelect)
                propertyConcat += String.Format(@" OnKPEventCheckBoxSelect=""{0}_KPEventCheckBoxSelect""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemComboEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemComboEntity entity = column.EntityComponentForm as KPFormItemComboEntity;
            if (entity == null)
            {
                entity = new KPFormItemComboEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            var isRequired = entity.IsRequired;
            var title = entity.Title;
            var namespaceEnum = entity.NamespaceEnum;
            var dataName = entity.DataName;
            var dataValue = entity.DataValue;
            var eventSelectChange = entity.KPEventComboSelectChange;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (!String.IsNullOrWhiteSpace(namespaceEnum))
                propertyConcat += String.Format(@" NamespaceEnum=""{0}""", namespaceEnum);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (!String.IsNullOrWhiteSpace(dataName) && !String.IsNullOrWhiteSpace(dataValue))
                propertyConcat += String.Format(@" DataName=""{0}"" DataValue=""{1}""", dataName, dataValue);
            if (eventSelectChange)
                propertyConcat += String.Format(@" OnKPEventComboSelectChange=""{0}_KPEventComboSelectChange""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemEntityEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemEntityEntity entity = column.EntityComponentForm as KPFormItemEntityEntity;
            if (entity == null)
            {
                entity = new KPFormItemEntityEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            var isRequired = entity.IsRequired;
            var title = entity.Title;
            var fieldNameDescription = entity.FieldNameDescription;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (!String.IsNullOrWhiteSpace(fieldNameDescription))
                propertyConcat += String.Format(@" Description=""{0}""", fieldNameDescription);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());

            return propertyConcat;
        }

        private string GetControlKPFormItemGridEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemGridEntity entity = column.EntityComponentForm as KPFormItemGridEntity;
            if (entity == null)
                entity = new KPFormItemGridEntity();

            var isRequired = entity.IsRequired;
            var height = entity.Height;
            var eventDeleteLine = entity.KPEventDeleteLine;

            if (height > 0)
                propertyConcat += String.Format(@" Height=""{0}""", height);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (eventDeleteLine)
                propertyConcat += String.Format(@" OnKPDeleteLine=""{0}_KPDeleteLine""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemPasswordEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemPasswordEntity entity = column.EntityComponentForm as KPFormItemPasswordEntity;
            if (entity == null)
            {
                entity = new KPFormItemPasswordEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            var title = entity.Title;
            var isRequired = entity.IsRequired;
            var eventLostFocus = entity.KPEventTextLostFocus;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (eventLostFocus)
                propertyConcat += String.Format(@" OnKPEventTextLostFocus=""{0}_KPEventTextLostFocus""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemTextEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemTextEntity entity = column.EntityComponentForm as KPFormItemTextEntity;
            if (entity == null)
            {
                entity = new KPFormItemTextEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            var title = entity.Title;
            var isRequired = entity.IsRequired;
            var isMultiLine = entity.IsMultiline;
            var height = entity.Height;
            var eventLostFocus = entity.KPEventTextLostFocus;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (isMultiLine)
                propertyConcat += String.Format(@" MultiLine=""{0}""", isMultiLine.ToString().ToLower());
            if (height > 0)
                propertyConcat += String.Format(@" Height=""{0}""", height);
            if (eventLostFocus)
                propertyConcat += String.Format(@" OnKPEventTextLostFocus=""{0}_KPEventTextLostFocus""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemZoomEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemZoomEntity entity = column.EntityComponentForm as KPFormItemZoomEntity;
            if (entity == null)
            {
                entity = new KPFormItemZoomEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            var title = entity.Title;
            var isRequired = entity.IsRequired;
            var descriptionWidth = entity.DescriptionWidth;
            var eventLostFocus = entity.KPEventZoomLostFocus;

            if (!String.IsNullOrWhiteSpace(title))
                propertyConcat += String.Format(@" Title=""{0}""", title);
            if (isRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", isRequired.ToString().ToLower());
            if (descriptionWidth > 0)
                propertyConcat += String.Format(@" DescriptionWidth=""{0}""", descriptionWidth);
            if (eventLostFocus)
                propertyConcat += String.Format(@" OnKPEventZoomLostFocus=""{0}_KPEventZoomLostFocus""", column.Name.GetFormattedText());

            return propertyConcat;
        }

        private string GetControlKPFormItemDateTimeEntity(Column column)
        {
            string propertyConcat = String.Empty;
            KPFormItemDateTimeEntity entity = column.EntityComponentForm as KPFormItemDateTimeEntity;
            if (entity == null)
            {
                entity = new KPFormItemDateTimeEntity()
                {
                    Title = GlobalizationLanguage.GetString("KPEntity_Title"),
                    IsRequired = !column.IsNullable
                };
            }

            if (!String.IsNullOrWhiteSpace(entity.Title))
                propertyConcat += String.Format(@" Title=""{0}""", entity.Title);
            if (entity.IsRequired)
                propertyConcat += String.Format(@" IsRequired=""{0}""", entity.IsRequired.ToString().ToLower());
            if (entity.Type.HasValue)
                propertyConcat += String.Format(@" Type=""{0}""", entity.Type.Value.ToString());
            if (entity.KPEventTextLostFocus)
                propertyConcat += String.Format(@" OnKPEventTextLostFocus=""{0}_KPEventTextLostFocus""", column.Name.GetFormattedText());

            return propertyConcat;
        }
        #endregion
    }

    internal class ZoomConfig
    {
        public Column Column { get; private set; }
        public KPFormItemZoomEntity ZoomEntity { get; private set; }
        public string ColumnName
        {
            get
            {
                if (Column != null)
                    return Column.Name;

                return String.Empty;
            }
        }

        public ZoomConfig(Column column, KPFormItemZoomEntity zoomEntity)
        {
            Column = column;
            ZoomEntity = zoomEntity;
            if (ZoomEntity == null)
                ZoomEntity = new KPFormItemZoomEntity();
        }
    }
}
