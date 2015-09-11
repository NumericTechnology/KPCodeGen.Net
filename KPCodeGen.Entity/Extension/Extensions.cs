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
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KPCodeGen.Entity.Extension
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static string EntityTypeName(this Type type)
        {
            if (type == typeof(string)) return type.Name;
            if (type == typeof(String)) return type.Name;
            if (type == typeof(Int16)) return type.Name;
            if (type == typeof(Int16?)) return String.Concat(typeof(Int16).Name, "?");
            if (type == typeof(Int32)) return type.Name;
            if (type == typeof(Int32?)) return String.Concat(typeof(Int32).Name, "?");
            if (type == typeof(Int64)) return type.Name;
            if (type == typeof(Int64?)) return String.Concat(typeof(Int64).Name, "?");
            if (type == typeof(double)) return type.Name;
            if (type == typeof(double?)) return String.Concat(typeof(double).Name, "?");
            if (type == typeof(decimal)) return type.Name;
            if (type == typeof(decimal?)) return String.Concat(typeof(decimal).Name, "?");
            if (type == typeof(CUBRID.Data.CUBRIDClient.CUBRIDBlob)) return type.Name;
            if (type == typeof(CUBRID.Data.CUBRIDClient.CUBRIDClob)) return type.Name;
            if (type == typeof(object[])) return type.Name;
            if (type == typeof(DateTime)) return type.Name;
            if (type == typeof(DateTime?)) return String.Concat(typeof(DateTime).Name, "?");
            if (type == typeof(TimeSpan)) return type.Name;
            if (type == typeof(TimeSpan?)) return String.Concat(typeof(TimeSpan).Name, "?");
            if (type == typeof(byte)) return type.Name;
            if (type == typeof(byte?)) return String.Concat(typeof(byte).Name, "?");
            if (type == typeof(Byte)) return type.Name;
            if (type == typeof(Byte?)) return String.Concat(typeof(Byte).Name, "?");
            if (type == typeof(byte[])) return type.Name;
            if (type == typeof(bool)) return type.Name;
            if (type == typeof(bool?)) return String.Concat(typeof(bool).Name, "?");
            if (type == typeof(Boolean)) return type.Name;
            if (type == typeof(Boolean?)) return String.Concat(typeof(Boolean).Name, "?");
            if (type == typeof(Single)) return type.Name;
            if (type == typeof(Single?)) return String.Concat(typeof(Single).Name, "?");
            if (type == typeof(Guid)) return type.Name;
            if (type == typeof(Guid?)) return String.Concat(typeof(Guid).Name, "?");

            return "Invalid Type";
        }

        public static Type GetNullableType(this Type type)
        {
            if (type == null)
                return null;

            if (type.IsValueType && type != typeof(void))
                return typeof(Nullable<>).MakeGenericType(type);

            return null;
        }

        public static string GetFormattedText(this string text)
        {
            string formattedText = text.Replace('_', ' ');
            formattedText = formattedText.MakeTitleCase();
            formattedText = formattedText.Replace(" ", "");
            return formattedText;
        }

        public static string GetFormattedDisplayName(this string text)
        {
            string formattedText = text.Replace('_', ' ');
            formattedText = formattedText.MakeTitleCase();
            return formattedText;
        }

        public static string ReplaceAt(this string text, int index, char charToUse)
        {
            var buffer = text.ToCharArray();
            buffer[index] = charToUse;
            return new string(buffer);
        }

        public static string MakeFirstCharLowerCase(this string text)
        {
            char lower = char.ToLower(text[0]);
            text = text.Remove(0, 1);
            text = lower + text;
            return text;
        }

        public static string MakeFirstCharUpperCase(this string text)
        {
            char upper = char.ToUpper(text[0]);
            text = text.Remove(0, 1);
            text = upper + text;
            return text;
        }

        public static string MakeTitleCase(this string text)
        {
            text = text.Trim();
            text = text.ToLower();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }
    }
}