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
 
using KPCodeGen.Globalization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KPCodeGen.Globalization
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public static partial class KPEnumExtension
    {
        public static string GetResource(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            Resources[] attributes =
                (Resources[])fi.GetCustomAttributes(
                typeof(Resources),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].ResourceName;
            else
                return value.ToString();
        }

    }
}
