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
using KPCodeGen.Entity.Extension;
using KPCodeGen.Core.Items;
using KPCodeGen.Core.Reader;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Util;
using KPCodeGen.Entity.ORM;
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
    public class KPAspNetFormsCSGerenator : AbstractGenerator
    {
        public KPAspNetFormsCSGerenator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
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

            sbFile.AppendFormat(@"using KPComponents;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using KPExtension;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using SpecialistEnumerator;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Collections.Generic;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Linq;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web.UI;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web.UI.WebControls;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);

            sbFile.AppendFormat(@"namespace {0}", ApplicationSettings.NamespaceWebProject).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{{").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}public partial class Frm{1} : KPPage", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}public Frm{1}()", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}: base(WindowEnum.{1}_FORM)", UtilCharacter.TAB_SPACE, table.Name.ToUpper()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);

            sbFile.AppendFormat(@"{0}{0}#region Events", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}protected void Page_Load(object sender, EventArgs e)", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            string concatEvent = String.Empty;
            foreach (Column column in table.Columns)
            {
                if (column.EntityComponentForm != null)
                {
                    switch (column.ColumnTypeForm)
                    {
                        case KPComponentsFormsEnum.KPFormItemButton:
                            concatEvent += GetControlKPFormItemButtonEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemCheckBox:
                            concatEvent += GetControlKPFormItemCheckBoxEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemCombo:
                            concatEvent += GetControlKPFormItemComboEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemEntity:
                            break;
                        case KPComponentsFormsEnum.KPFormItemGrid:
                            concatEvent += GetControlKPFormItemGridEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemKey:
                            break;
                        case KPComponentsFormsEnum.KPFormItemPassword:
                            concatEvent += GetControlKPFormItemPasswordEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemSpacer:
                            break;
                        case KPComponentsFormsEnum.KPFormItemText:
                            concatEvent += GetControlKPFormItemTextEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemZoom:
                            concatEvent += GetControlKPFormItemZoomEntity(column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemDateTime:
                            concatEvent += GetControlKPFormItemDateTimeEntity(column);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (!String.IsNullOrWhiteSpace(concatEvent))
            {
                sbFile.Append(UtilCharacter.NEW_LINE);
                sbFile.AppendFormat(@"{0}", concatEvent).Append(UtilCharacter.NEW_LINE);
            }

            sbFile.AppendFormat(@"{0}{0}#endregion Events", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE); ;
            sbFile.AppendFormat(@"{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"}}").Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }

        private string GetControlKPFormItemButtonEntity(Column column)
        {
            KPFormItemButtonEntity entity = column.EntityComponentForm as KPFormItemButtonEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventClick)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPClick(object sender, EventArgs e)", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemTextEntity(Column column)
        {
            KPFormItemTextEntity entity = column.EntityComponentForm as KPFormItemTextEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventTextLostFocus)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventTextLostFocus()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemCheckBoxEntity(Column column)
        {
            KPFormItemCheckBoxEntity entity = column.EntityComponentForm as KPFormItemCheckBoxEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventCheckBoxSelect)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventCheckBoxSelect()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemComboEntity(Column column)
        {
            KPFormItemComboEntity entity = column.EntityComponentForm as KPFormItemComboEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventComboSelectChange)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventComboSelectChange()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemDateTimeEntity(Column column)
        {
            KPFormItemDateTimeEntity entity = column.EntityComponentForm as KPFormItemDateTimeEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventTextLostFocus)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventTextLostFocus()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemGridEntity(Column column)
        {
            KPFormItemGridEntity entity = column.EntityComponentForm as KPFormItemGridEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventDeleteLine)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPDeleteLine(object sender, GridViewDeleteEventArgs e)", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemPasswordEntity(Column column)
        {
            KPFormItemPasswordEntity entity = column.EntityComponentForm as KPFormItemPasswordEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventTextLostFocus)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventTextLostFocus()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }

        private string GetControlKPFormItemZoomEntity(Column column)
        {
            KPFormItemZoomEntity entity = column.EntityComponentForm as KPFormItemZoomEntity;
            StringBuilder sbEvent = new StringBuilder();
            if (entity == null)
                return String.Empty;

            if (entity.KPEventZoomLostFocus)
            {
                sbEvent.AppendFormat(@"{0}{0}protected void {1}_KPEventZoomLostFocus()", UtilCharacter.TAB_SPACE, column.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                sbEvent.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            }

            return sbEvent.ToString();
        }
    }
}
