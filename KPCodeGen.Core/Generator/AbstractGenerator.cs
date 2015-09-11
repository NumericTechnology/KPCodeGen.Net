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
using System.IO;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Reader;

namespace KPCodeGen.Core.Generator
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public abstract class AbstractGenerator : IGenerator
    {
        protected string ClassNamePrefix { get; set; }
        protected ApplicationSettings ApplicationSettings;
        protected AbstractMetadataReader MetadataReader;


        protected AbstractGenerator(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
        {
            this.ApplicationSettings = applicationSettings;
            this.MetadataReader = metadataReader;
        }

        public abstract string Generate(Table table);
    }
}