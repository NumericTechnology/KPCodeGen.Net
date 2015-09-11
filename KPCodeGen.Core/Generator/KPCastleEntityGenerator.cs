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
using Castle.ActiveRecord;
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
    public class KPCastleEntityGenerator : AbstractGenerator
    {
        public KPCastleEntityGenerator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
            : base(applicationSettings, metadataReader)
        {
        }

        public override string Generate(Table table)
        {
            return GetStructEntity(table);
        }

        private string GetStructEntity(Table table)
        {
            StringBuilder sbFile = new StringBuilder();
            sbFile.Append(AddStandardHeader());
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat("namespace {0}", this.ApplicationSettings.NamespaceEntity).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat("{{").Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}[Serializable]", UtilCharacter.TAB_SPACE);
            sbFile.Append(UtilCharacter.NEW_LINE);
            var columCompany = table.Columns.Where(x => x.IsForeignKey == true && x.ForeignKeyTableName == "FRW_COMPANY" && x.ForeignKeyColumnName == "ID_COMPANY");
            if (columCompany != null && columCompany.Count() > 0)
                sbFile.AppendFormat(@"{0}[KPEntityTable(""{1}"", ""obj{2}"")]", UtilCharacter.TAB_SPACE, table.Name, columCompany.First().Name.GetFormattedText());
            else
                sbFile.AppendFormat(@"{0}[KPEntityTable(""{1}"")]", UtilCharacter.TAB_SPACE, table.Name);
            sbFile.Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"{0}public class {1} : KPActiveRecordBase<{1}>", UtilCharacter.TAB_SPACE, table.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat("{0}{{", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.Append(UtilCharacter.NEW_LINE);



            #region PK and FK Columns
            foreach (var columnItem in table.Columns.Where(x => x.IsPrimaryKey == true || x.IsForeignKey == true).OrderByDescending(x => x.IsPrimaryKey))
            {
                sbFile.AppendFormat(@"{0}{0}#region {1}", UtilCharacter.TAB_SPACE, columnItem.Name).Append(UtilCharacter.NEW_LINE);
                if (!columnItem.IsNullable && columnItem.MappedDataType == typeof(String) && !columnItem.IsPrimaryKey)
                {
                    sbFile.AppendFormat("{0}{0}[NotNull, NotEmpty]", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                }
                else if (!columnItem.IsNullable && !columnItem.IsPrimaryKey)
                {
                    sbFile.AppendFormat("{0}{0}[NotNull]", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                }

                sbFile.AppendFormat(@"{0}{0}[KPDisplayName(""{1}"")]", UtilCharacter.TAB_SPACE, columnItem.DisplayName).Append(UtilCharacter.NEW_LINE);

                if (table.PrimaryKey != null && table.PrimaryKey.Columns.Count() != 0)
                {
                    if (columnItem.IsPrimaryKey)
                    {
                        sbFile.AppendFormat(@"{0}{0}[PrimaryKey(Column = ""{1}"")]", UtilCharacter.TAB_SPACE, columnItem.Name).Append(UtilCharacter.NEW_LINE);
                    }
                }

                if (columnItem.DataLength.HasValue && columnItem.DataLength > 0)
                {
                    sbFile.AppendFormat(@"{0}{0}[Length(Max = {1})]", UtilCharacter.TAB_SPACE, columnItem.DataLength).Append(UtilCharacter.NEW_LINE);
                }

                var foreignKey = table.ForeignKeys.FirstOrDefault(x => x.Name.Equals(columnItem.ConstraintName));
                if (!columnItem.IsPrimaryKey && foreignKey != null)
                {
                    sbFile.AppendFormat(@"{0}{0}[BelongsTo(""{1}"", Lazy = FetchWhen.OnInvoke)]", UtilCharacter.TAB_SPACE, columnItem.Name).Append(UtilCharacter.NEW_LINE);
                    sbFile.AppendFormat(@"{0}{0}public virtual {1} obj{2} {{ get; set; }}", UtilCharacter.TAB_SPACE, foreignKey.References.GetFormattedText(), columnItem.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                }
                else
                {
                    if ((columnItem.DataPrecision.HasValue && columnItem.DataPrecision.Value > 0 && columnItem.DataScale.HasValue && columnItem.DataScale.Value > 0))
                    {
                        sbFile.AppendFormat(@"{0}{0}[Digits(IntegerDigits = {1}, FractionalDigits = {2})]", UtilCharacter.TAB_SPACE, columnItem.DataPrecision, columnItem.DataScale).Append(UtilCharacter.NEW_LINE);
                    }

                    sbFile.AppendFormat(@"{0}{0}public virtual {1} {2} {{ get; set; }}", UtilCharacter.TAB_SPACE, columnItem.MappedDataType.EntityTypeName(), columnItem.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);
                }

                sbFile.AppendFormat(@"{0}{0}#endregion {1}", UtilCharacter.TAB_SPACE, columnItem.Name).Append(UtilCharacter.NEW_LINE);
                sbFile.Append(UtilCharacter.NEW_LINE);
            }
            #endregion PK and FK Columns

            #region Normal Columns
            foreach (var property in table.Columns.Where(x => x.IsPrimaryKey != true && x.IsForeignKey != true))
            {
                sbFile.AppendFormat(@"{0}{0}#region {1}", UtilCharacter.TAB_SPACE, property.Name).Append(UtilCharacter.NEW_LINE);
                if (!property.IsNullable && property.MappedDataType == typeof(String))
                {
                    sbFile.AppendFormat("{0}{0}[NotNull, NotEmpty]", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                }
                else if (!property.IsNullable)
                {
                    sbFile.AppendFormat("{0}{0}[NotNull]", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
                }

                sbFile.AppendFormat(@"{0}{0}[KPDisplayName(""{1}"")]", UtilCharacter.TAB_SPACE, property.DisplayName).Append(UtilCharacter.NEW_LINE);

                if (property.DataLength.HasValue && property.DataLength > 0)
                {
                    sbFile.AppendFormat(@"{0}{0}[Length(Max = {1})]", UtilCharacter.TAB_SPACE, property.DataLength).Append(UtilCharacter.NEW_LINE);
                }
                else if ((property.DataPrecision.HasValue && property.DataPrecision.Value > 0 && property.DataScale.HasValue && property.DataScale.Value > 0))
                {
                    sbFile.AppendFormat(@"{0}{0}[Digits(IntegerDigits = {1}, FractionalDigits = {2})]", UtilCharacter.TAB_SPACE, property.DataPrecision, property.DataScale).Append(UtilCharacter.NEW_LINE);
                }

                sbFile.AppendFormat(@"{0}{0}[Property(Column = ""{1}"")]", UtilCharacter.TAB_SPACE, property.Name).Append(UtilCharacter.NEW_LINE);

                sbFile.AppendFormat(@"{0}{0}public virtual {1} {2} {{ get; set; }}", UtilCharacter.TAB_SPACE, property.MappedDataType.EntityTypeName(), property.Name.GetFormattedText()).Append(UtilCharacter.NEW_LINE);

                sbFile.AppendFormat(@"{0}{0}#endregion {1}", UtilCharacter.TAB_SPACE, property.Name).Append(UtilCharacter.NEW_LINE);
                sbFile.Append(UtilCharacter.NEW_LINE);
            }
            #endregion Normal Columns

            sbFile.AppendFormat(@"{0}}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);
            sbFile.AppendFormat(@"}}", UtilCharacter.TAB_SPACE).Append(UtilCharacter.NEW_LINE);

            return sbFile.ToString();
        }

        private static string AddStandardHeader()
        {
            StringBuilder sbImportUsing = new StringBuilder();
            sbImportUsing.AppendFormat("using Castle.ActiveRecord;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using KPAttributes;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using KPCore.KPValidator;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using KPEntity.ORM;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using NHibernate.Validator.Constraints;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using SpecialistEnumerator;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using System;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using System.Collections.Generic;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using System.ComponentModel;").Append(Environment.NewLine);
            sbImportUsing.AppendFormat("using System.Text;").Append(Environment.NewLine);

            return sbImportUsing.ToString();
        }
    }
}