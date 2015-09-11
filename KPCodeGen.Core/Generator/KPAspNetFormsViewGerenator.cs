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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.Extension;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Util;
using Microsoft.CSharp;
using KPCodeGen.Core.Items;
using KPCodeGen.Core.Reader;
using KPCodeGen.Enumerator;
using KPCodeGen.Entity.ORM;
using KPCodeGen.Globalization;

namespace KPCodeGen.Core.Generator
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPAspNetFormsViewGerenator : AbstractGenerator
    {
        public KPAspNetFormsViewGerenator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
            : base(applicationSettings, metadataReader)
        {
        }

        public override string Generate(Table table)
        {
            return GetStructAspNetFormsView(table);
        }

        public string GetStructAspNetFormsView(Table table)
        {
            StringBuilder sbFile = new StringBuilder();
            sbFile.AppendFormat(@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind=""Frm{0}View.aspx.cs"" ", table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"MasterPageFile=""~/Master/View.Master"" Inherits=""{0}.Frm{1}View"" %>", ApplicationSettings.NamespaceWebProject, table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);

            sbFile.AppendFormat(@"<asp:Content ID=""ContentHead"" ContentPlaceHolderID=""HeadView"" runat=""server"">{0}</asp:Content>", UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);

            sbFile.AppendFormat(@"<asp:Content ID=""ContentBody"" ContentPlaceHolderID=""BodyView"" runat=""server"">");
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}<KP:KPGridControl ID=""KPGridControl"" TypeEntityNamespace=""{1}.{2}""", UtilCharacter.TAB_SPACE, this.ApplicationSettings.NamespaceEntity, table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            string propertyPK = String.Empty;
            if (table.PrimaryKey != null && table.PrimaryKey.Columns.Count() != 0)
                propertyPK = table.PrimaryKey.Columns[0].Name.GetFormattedText();

            sbFile.AppendFormat(@"{0}{0}runat=""server"" PageFormUrl=""Frm{1}.aspx"">", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}<KPItemsModel>", UtilCharacter.TAB_SPACE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            string maskConcat = String.Empty;
            int width = 0;
            foreach (Column column in table.Columns)
            {
                maskConcat = String.Empty;
                width = 100;

                if (column.EntityComponentView != null)
                {
                    KPMaskTypeClassEnum? maskType = null;

                    switch (column.ColumnTypeView)
                    {
                        case KPComponentsViewEnum.KPColumnModel:
                            maskType = ((KPColumnModelEntity)column.EntityComponentView).MaskType;
                            width = ((KPColumnModelEntity)column.EntityComponentView).Width;
                            break;
                        case KPComponentsViewEnum.KPEntityModel:
                            maskType = ((KPEntityModelEntity)column.EntityComponentView).MaskType;
                            width = ((KPEntityModelEntity)column.EntityComponentView).Width;
                            break;
                    }

                    if (maskType.HasValue && maskType.Value != KPMaskTypeClassEnum.ALPHANUMERIC)
                        maskConcat = String.Format(@" Mask=""{0}""", maskType);

                    if (width <= 0)
                        width = GetWidthComponent(maskType, column);
                }

                IList<Column> tableFKcolumnList = MetadataReader.GetTableDetails(column.ForeignKeyTableName, ApplicationSettings.OwnerSchema);

                /* Primeira coluna string da FK, para simular o descritivo da tabela FK
                string FieldNameDescription = column.ForeignKeyColumnName;
                var columnsString = tableFKcolumnList.Where(x => x.MappedDataType.Equals(typeof(String)));
                if (columnsString != null && columnsString.Count() > 0)
                    FieldNameDescription = columnsString.First().Name;
                 */

                if (column.IsPrimaryKey)
                {
                    string propertyConcat = String.Format(@" Width=""{0}""", width);
                    if (column.EntityComponentView != null)
                    {
                        switch (column.ColumnTypeView)
                        {
                            case KPComponentsViewEnum.KPColumnModel:
                                propertyConcat += GetControlKPColumnModelEntity(column.EntityComponentView as KPColumnModelEntity);
                                break;
                            default:
                                throw new Exception(@"Coluna PrimaryKey deverá ser do tipo componente: ""KPColumnModel""");
                        }
                    }

                    sbFile.AppendFormat(@"{0}{0}{0}<KP:{1} FieldName=""{2}""{3} />", UtilCharacter.TAB_SPACE, column.ColumnTypeView, column.Name.GetFormattedText(), propertyConcat);
                }
                else if (column.IsForeignKey)
                {
                    string propertyConcat = String.Format(@" Width=""{0}""", width);
                    if (column.EntityComponentView != null)
                    {
                        switch (column.ColumnTypeView)
                        {
                            case KPComponentsViewEnum.KPColumnModel:
                                propertyConcat += GetControlKPColumnModelEntity(column.EntityComponentView as KPColumnModelEntity);
                                break;
                            case KPComponentsViewEnum.KPEntityModel:
                                propertyConcat += GetControlKPEntityModelEntity(column.EntityComponentView as KPEntityModelEntity);
                                break;

                            default:
                                throw new Exception(@"Coluna ForeignKey deverá ser do tipo componente: ""KPColumnModel, KPEntityModel""");
                        }
                    }

                    sbFile.AppendFormat(@"{0}{0}{0}<KP:{1} FieldName=""obj{2}""{3}{4} />", UtilCharacter.TAB_SPACE, column.ColumnTypeView, column.Name.GetFormattedText(), propertyConcat, maskConcat);
                }
                else
                {
                    string propertyConcat = String.Format(@" Width=""{0}""", width);
                    if (column.EntityComponentView != null)
                    {
                        switch (column.ColumnTypeView)
                        {
                            case KPComponentsViewEnum.KPColumnModel:
                                propertyConcat += GetControlKPColumnModelEntity(column.EntityComponentView as KPColumnModelEntity);
                                break;
                            case KPComponentsViewEnum.KPBooleanModel:
                                propertyConcat += GetControlKPBooleanModelEntity(column.EntityComponentView as KPBooleanModelEntity);
                                break;
                            case KPComponentsViewEnum.KPEnumModel:
                                propertyConcat += GetControlKPEnumModelEntity(column.EntityComponentView as KPEnumModelEntity);
                                break;
                            default:
                                throw new Exception(@"Coluna comum deverá ser do tipo componente: ""KPColumnModel, KPBooleanModel, KPEnumModel""");
                        }
                    }

                    sbFile.AppendFormat(@"{0}{0}{0}<KP:{1} FieldName=""{2}""{3}{4} />", UtilCharacter.TAB_SPACE, column.ColumnTypeView, column.Name.GetFormattedText(), propertyConcat, maskConcat);
                }
                sbFile.Append(UtilCharacter.NEW_LINE);
            }

            sbFile.AppendFormat(@"{0}{0}</KPItemsModel>", UtilCharacter.TAB_SPACE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}</KP:KPGridControl>", UtilCharacter.TAB_SPACE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"</asp:Content>");
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

        private string GetControlKPColumnModelEntity(KPColumnModelEntity entity)
        {
            string propertyConcat = String.Empty;
            if (entity == null)
            {
                entity = new KPColumnModelEntity()
                {
                    HeaderName = GlobalizationLanguage.GetString("KPEntity_Title")
                };
            }

            var headerName = entity.HeaderName;
            var isVisible = entity.IsVisible;
            var isSortable = entity.IsSortable;

            if (!String.IsNullOrWhiteSpace(headerName))
                propertyConcat += String.Format(@" HeaderName=""{0}""", headerName);
            if (!isVisible)
                propertyConcat += String.Format(@" Visible=""{0}""", isVisible.ToString().ToLower());
            if (!isSortable)
                propertyConcat += String.Format(@" Sortable=""{0}""", isSortable.ToString().ToLower());

            return propertyConcat;
        }

        private string GetControlKPBooleanModelEntity(KPBooleanModelEntity entity)
        {
            string propertyConcat = String.Empty;
            if (entity == null)
            {
                entity = new KPBooleanModelEntity()
                {
                    HeaderName = GlobalizationLanguage.GetString("KPEntity_Title")
                };
            }

            var headerName = entity.HeaderName;
            var isVisible = entity.IsVisible;
            var isSortable = entity.IsSortable;
            var customTrue = entity.CustomTrue;
            var customFalse = entity.CustomFalse;
            if (!String.IsNullOrWhiteSpace(headerName))
                propertyConcat += String.Format(@" HeaderName=""{0}""", headerName);
            if (!String.IsNullOrWhiteSpace(customTrue))
                propertyConcat += String.Format(@" CustomTrue=""{0}""", customTrue);
            if (!String.IsNullOrWhiteSpace(customFalse))
                propertyConcat += String.Format(@" CustomFalse=""{0}""", customFalse);
            if (!isVisible)
                propertyConcat += String.Format(@" Visible=""{0}""", isVisible.ToString().ToLower());
            if (!isSortable)
                propertyConcat += String.Format(@" Sortable=""{0}""", isSortable.ToString().ToLower());

            return propertyConcat;
        }

        private string GetControlKPEnumModelEntity(KPEnumModelEntity entity)
        {
            string propertyConcat = String.Empty;
            if (entity == null)
            {
                entity = new KPEnumModelEntity()
                {
                    HeaderName = GlobalizationLanguage.GetString("KPEntity_Title")
                };
            }

            var headerName = entity.HeaderName;
            var isVisible = entity.IsVisible;
            var isSortable = entity.IsSortable;
            var namespaceEnum = entity.NamespaceEnum;
            if (!String.IsNullOrWhiteSpace(headerName))
                propertyConcat += String.Format(@" HeaderName=""{0}""", headerName);
            if (!String.IsNullOrWhiteSpace(namespaceEnum))
                propertyConcat += String.Format(@" NamespaceEnum=""{0}""", namespaceEnum);
            if (!isVisible)
                propertyConcat += String.Format(@" Visible=""{0}""", isVisible.ToString().ToLower());
            if (!isSortable)
                propertyConcat += String.Format(@" Sortable=""{0}""", isSortable.ToString().ToLower());

            return propertyConcat;
        }

        private string GetControlKPEntityModelEntity(KPEntityModelEntity entity)
        {
            string propertyConcat = String.Empty;
            if (entity == null)
            {
                entity = new KPEntityModelEntity()
                {
                    HeaderName = GlobalizationLanguage.GetString("KPEntity_Title")
                };
            }

            var headerName = entity.HeaderName;
            var isVisible = entity.IsVisible;
            var isSortable = entity.IsSortable;
            var fieldNameDescription = entity.FieldNameDescription;
            if (!String.IsNullOrWhiteSpace(headerName))
                propertyConcat += String.Format(@" HeaderName=""{0}""", headerName);
            if (!String.IsNullOrWhiteSpace(fieldNameDescription))
                propertyConcat += String.Format(@" FieldNameDescription=""{0}""", fieldNameDescription);
            if (!isVisible)
                propertyConcat += String.Format(@" Visible=""{0}""", isVisible.ToString().ToLower());
            if (!isSortable)
                propertyConcat += String.Format(@" Sortable=""{0}""", isSortable.ToString().ToLower());

            return propertyConcat;
        }
        #endregion
    }
}
