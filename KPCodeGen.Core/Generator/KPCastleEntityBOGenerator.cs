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
 
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.Extension;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Util;
using Microsoft.CSharp;
using System.Text;
using KPCodeGen.Core.Reader;

namespace KPCodeGen.Core.Generator
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPCastleEntityBOGenerator : AbstractGenerator
    {
        public KPCastleEntityBOGenerator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
            : base(applicationSettings, metadataReader)
        {
        }

        public override string Generate(Table table)
        {
            return GetStructEntityBO(table);
        }

        private string GetStructEntityBO(Table table)
        {
            StringBuilder sbFile = new StringBuilder();
            sbFile.Append(AddStandardHeader());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"namespace {0} {{", this.ApplicationSettings.NamespaceEntityBO).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}public class {1}BO : BaseBO<{1}>", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}public {1}BO({1} entity)", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}: base(entity)", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// This method is responsible for save and return the Entity,", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// so if you need to save some extra data you could use this method.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}///", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// TODO: This method is Auto-generated by the KP Mapping Generator.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// </summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <returns>The Entity saved in the database</returns>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}public override {1} SaveEntity()", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}// Your code here", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}return base.SaveEntity(); // Default framework SaveEntity, it call's the Validate method.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// This method is responsible for save and return the Entity,", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// so if you need to save some extra data you could use this method.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}///", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// TODO: This method is Auto-generated by the KP Mapping Generator.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// </summary>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}/// <returns>True if the Entity is validated and False if not, e.g: if some required field is empty, returns false.</returns>", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}public override bool Validate()", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}// Your code here", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}{0}return base.Validate(); // Default framework validation.", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }

        private string AddStandardHeader()
        {
            string entireContent = String.Empty;
            entireContent = "using Castle.ActiveRecord;" + Environment.NewLine + entireContent;
            entireContent = "using KPBO;" + Environment.NewLine + entireContent;
            entireContent = String.Format("using {0};", this.ApplicationSettings.NamespaceEntity) + Environment.NewLine + entireContent;
            entireContent = "using System;" + Environment.NewLine + entireContent;
            entireContent = "using System.Collections.Generic;" + Environment.NewLine + entireContent;
            entireContent = "using System.Linq;" + Environment.NewLine + entireContent;
            entireContent = "using System.Text;" + Environment.NewLine + entireContent;
            return entireContent;
        }


    }
}
