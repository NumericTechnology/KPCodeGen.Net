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
 
using KPCodeGen.Core.Util;
using KPCodeGen.Entity.Extension;
using NHibernate.Hql.Ast.ANTLR.Tree;
using System;
using System.Collections.Generic;

namespace KPCodeGen.Core.Items
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class DotNetTypes : List<String>
    {
        public DotNetTypes()
        {
            Add(typeof(String).EntityTypeName());
            Add(typeof(Int16).EntityTypeName());
            Add(typeof(Int16?).EntityTypeName());
            Add(typeof(Int32).EntityTypeName());
            Add(typeof(Int32?).EntityTypeName());
            Add(typeof(Int64).EntityTypeName());
            Add(typeof(Int64?).EntityTypeName());
            Add(typeof(double).EntityTypeName());
            Add(typeof(double?).EntityTypeName());
            Add(typeof(decimal).EntityTypeName());
            Add(typeof(decimal?).EntityTypeName());
            Add(typeof(CUBRID.Data.CUBRIDClient.CUBRIDBlob).EntityTypeName());
            Add(typeof(CUBRID.Data.CUBRIDClient.CUBRIDClob).EntityTypeName());
            Add(typeof(object[]).EntityTypeName());
            Add(typeof(DateTime).EntityTypeName());
            Add(typeof(DateTime?).EntityTypeName());
            Add(typeof(TimeSpan).EntityTypeName());
            Add(typeof(TimeSpan?).EntityTypeName());
            Add(typeof(Byte).EntityTypeName());
            Add(typeof(Byte?).EntityTypeName());
            Add(typeof(byte[]).EntityTypeName());
            Add(typeof(bool).EntityTypeName());
            Add(typeof(bool?).EntityTypeName());
            Add(typeof(Single).EntityTypeName());
            Add(typeof(Single?).EntityTypeName());
            Add(typeof(Guid).EntityTypeName());
            Add(typeof(Guid?).EntityTypeName());

            this.Sort();
        }
    }
}