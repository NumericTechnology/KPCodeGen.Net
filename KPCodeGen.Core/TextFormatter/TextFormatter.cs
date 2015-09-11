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
using KPCodeGen.Core.Generator;
using KPCodeGen.Enumerator;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KPCodeGen.Core.TextFormatter
{
    public interface ITextFormatter
    {
        string FormatText(string text);
        string FormatSingular(string text);
        string FormatPlural(string text);
    }

    public abstract class AbstractTextFormatter : ITextFormatter
    {
        public virtual string FormatText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Cannot have class or property with not allowed chars
            var result = text
                 .Replace("%", "Porcentaje") //Means Percentage in spanish
                 .Replace("á", "a")
                 .Replace("é", "e")
                 .Replace("í", "i")
                 .Replace("ó", "o")
                 .Replace("ú", "u");

            // Split by capitals to preserve pascal/camelcasing in original text value
            // Preserves TLAs. See http://stackoverflow.com/a/1098039
            result = Regex.Replace(result, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim();

            // Omit any chars except letters and numbers in class or properties.
            result = result.Replace(" ", "_");
            result = Regex.Replace(result, "[^a-zA-Z0-9_]", String.Empty); //And Underscore

            if (result.Length != 0 && char.IsNumber(result.ToCharArray(0, 1)[0]))
            {
                // Cannot start class or property with a number
                result = "_" + result;
            }

            return result;
        }

        public string FormatSingular(string text)
        {
            return FormatText(text).MakeSingular();
        }

        public string FormatPlural(string text)
        {
            return FormatText(text).MakePlural();
        }
    }

    public class UnformattedTextFormatter : AbstractTextFormatter { }

    public class CamelCaseTextFormatter : AbstractTextFormatter
    {
        public override string FormatText(string text)
        {
            return base.FormatText(text).ToCamelCase();
        }
    }

    public class PascalCaseTextFormatter : AbstractTextFormatter
    {
        public override string FormatText(string text)
        {
            return base.FormatText(text).ToPascalCase();
        }
    }

    public class PrefixedTextFormatter : AbstractTextFormatter
    {
        public PrefixedTextFormatter(string prefix)
        {
            Prefix = prefix;
        }

        private string Prefix { get; set; }

        public override string FormatText(string text)
        {
            return Prefix + base.FormatText(text);
        }
    }

    public static class TextFormatterFactory
    {
        public static ITextFormatter GetTextFormatter(ApplicationSettings applicationSettings)
        {
            ITextFormatter formatter;
            switch (applicationSettings.FieldNamingConvention)
            {
                case FieldNamingConvention.PascalCase:
                    formatter = new PascalCaseTextFormatter();
                    break;
                default:
                    throw new Exception("Invalid or unsupported field naming convention.");
            }

            return formatter;
        }
    }
}