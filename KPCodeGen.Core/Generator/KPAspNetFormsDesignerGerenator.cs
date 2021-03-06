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
 
using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.Extension;
using KPCodeGen.Core.Items;
using KPCodeGen.Core.Reader;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Util;
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
    public class KPAspNetFormsDesignerGerenator : AbstractGenerator
    {

        public KPAspNetFormsDesignerGerenator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
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

            sbFile.AppendFormat(@"namespace {0} {{", ApplicationSettings.NamespaceWebProject).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}public partial class Frm{1} {{", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// formControl control.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// </summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <remarks>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// Auto-generated field.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// To modify move field declaration from designer file to code-behind file.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// </remarks>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}protected global::KPComponents.KPFormControl formControl;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"}}").Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }
    }
}
