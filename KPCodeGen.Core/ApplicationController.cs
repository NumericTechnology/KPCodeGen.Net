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
 
using KPCodeGen.Core;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Core.Generator;
using KPCodeGen.Core.Reader;
using System;
using System.IO;
using System.Text;

namespace KPCodeGen.Core
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class ApplicationController
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly KPCastleEntityGenerator _castleEntityGenerator;
        private readonly KPCastleEntityBOGenerator _castleEntityBoGenerator;
        private readonly KPAspNetFormsViewGerenator _kpAspNetFormsViewGerenator;
        private readonly KPAspNetFormsViewCSGerenator _kpAspNetFormsViewCSGerenator;
        private readonly KPAspNetFormsViewDesignerGerenator _kpAspNetFormsViewDesignerGerenator;
        private readonly KPAspNetFormsGerenator _kpAspNetFormsGerenator;
        private readonly KPAspNetFormsCSGerenator _kpAspNetFormsCSGerenator;
        private readonly KPAspNetFormsDesignerGerenator _kpAspNetFormsDesignerGerenator;

        public ApplicationController(ApplicationSettings applicationSettings, AbstractMetadataReader metadataReader)
        {
            _applicationSettings = applicationSettings;
            _castleEntityGenerator = new KPCastleEntityGenerator(applicationSettings, metadataReader);
            _castleEntityBoGenerator = new KPCastleEntityBOGenerator(applicationSettings, metadataReader);
            _kpAspNetFormsViewGerenator = new KPAspNetFormsViewGerenator(applicationSettings, metadataReader);
            _kpAspNetFormsViewCSGerenator = new KPAspNetFormsViewCSGerenator(applicationSettings, metadataReader);
            _kpAspNetFormsViewDesignerGerenator = new KPAspNetFormsViewDesignerGerenator(applicationSettings, metadataReader);
            _kpAspNetFormsGerenator = new KPAspNetFormsGerenator(applicationSettings, metadataReader);
            _kpAspNetFormsCSGerenator = new KPAspNetFormsCSGerenator(applicationSettings, metadataReader);
            _kpAspNetFormsDesignerGerenator = new KPAspNetFormsDesignerGerenator(applicationSettings, metadataReader);
        }

        public string GeneratedEntityBOCode { get; set; }
        public string GeneratedEntityCode { get; set; }
        public string GeneratedFormsViewASPXCode { get; set; }
        public string GeneratedFormsViewCSCode { get; set; }
        public string GeneratedFormsViewDesignerCode { get; set; }
        public string GeneratedFormsASPXCode { get; set; }
        public string GeneratedFormsCSCode { get; set; }
        public string GeneratedFormsDesignerCode { get; set; }

        public void Generate(Table table)
        {
            GeneratedEntityBOCode = _castleEntityBoGenerator.Generate(table);
            GeneratedEntityCode = _castleEntityGenerator.Generate(table);
            GeneratedFormsViewASPXCode = _kpAspNetFormsViewGerenator.Generate(table);
            GeneratedFormsViewCSCode = _kpAspNetFormsViewCSGerenator.Generate(table);
            GeneratedFormsViewDesignerCode = _kpAspNetFormsViewDesignerGerenator.Generate(table);
            GeneratedFormsASPXCode = _kpAspNetFormsGerenator.Generate(table);
            GeneratedFormsCSCode = _kpAspNetFormsCSGerenator.Generate(table);
            GeneratedFormsDesignerCode = _kpAspNetFormsDesignerGerenator.Generate(table);
        }


        public void GenerateFiles(Table table)
        {
            Generate(table);

            CreateFile(_applicationSettings.DirFileEntity + "\\" + _applicationSettings.EntityName + ".cs", GeneratedEntityCode);
            CreateFile(_applicationSettings.DirFileEntityBO + "\\" + _applicationSettings.EntityName + "BO.cs", GeneratedEntityBOCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + "View.aspx", GeneratedFormsViewASPXCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + "View.aspx.cs", GeneratedFormsViewCSCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + "View.aspx.designer.cs", GeneratedFormsViewDesignerCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + ".aspx", GeneratedFormsASPXCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + ".aspx.cs", GeneratedFormsCSCode);
            CreateFile(_applicationSettings.DirFileForms + "\\Frm" + _applicationSettings.EntityName + ".aspx.designer.cs", GeneratedFormsDesignerCode);

        }

        private void CreateFile(string path, string contentFile)
        {
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, contentFile);
            }
        }

        private void AddText(FileStream fs, string value)
        {
            var encoding = new UTF8Encoding(true);
            using (StreamWriter sw = new StreamWriter(fs, encoding))
            {
                sw.Write(value);
            }
        }
    }
}