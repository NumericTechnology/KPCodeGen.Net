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
 
using KPCodeGen.Entity.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace KPCodeGen.Core.Util
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public static class Extensions
    {
        public static string GetPreferenceFormattedText(this string text, ApplicationSettings applicationSettings, bool pluralize)
        {
            string formattedText = text.Replace('_', ' ');
            formattedText = formattedText.MakeTitleCase();
            formattedText = formattedText.Replace(" ", "");

            return formattedText;
        }

        public static string GetPreferenceFormattedText(this string text, ApplicationSettings applicationSettings)
        {
            return GetPreferenceFormattedText(text, applicationSettings, false);
        }
    }
}