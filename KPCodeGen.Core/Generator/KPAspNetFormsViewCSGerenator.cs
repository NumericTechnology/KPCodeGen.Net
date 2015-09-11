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

namespace KPCodeGen.Core.Generator
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPAspNetFormsViewCSGerenator : AbstractGenerator
    {
        public KPAspNetFormsViewCSGerenator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
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

            sbFile.AppendFormat(@"using KPComponents;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using KPExtension;", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using SpecialistEnumerator;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Collections.Generic;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Linq;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web.UI;").Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"using System.Web.UI.WebControls;").Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);

            sbFile.AppendFormat(@"namespace {0}", ApplicationSettings.NamespaceWebProject).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}public partial class Frm{1}View :  KPPage", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}public Frm{1}View()", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}: base(WindowEnum.{1}_VIEW)", UtilCharacter.TAB_SPACE, table.Name.ToUpper()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}protected void Page_Load(object sender, EventArgs e)", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"}}").Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }
    }
}
