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
 
using Castle.ActiveRecord;
using KPCodeGen.Core.Delegates;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace KPCodeGen.Core
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class ApplicationSettings
    {
        #region Events
        public event ChangedSettingsEventHandler EventSettingsChanged;
        #endregion

        #region Fields
        private string ownerSchema;
        private string tableName;
        private string dirFileEntity;
        private string dirFileEntityBO;
        private string dirFileForms;
        private string namespaceEntity;
        private string namespaceEntityBO;
        private string namespaceWebProject;
        private string globalizationLanguage;
        private string entityName;
        private string classNamePrefix;

        private List<Connection> connections;
        #endregion

        #region Properties
        public List<Connection> Connections { get { return connections; } set { connections = value; } }

        public Guid? LastUsedConnection { get; set; }

        public DatabaseServerType DatabaseServerType { get; set; }

        public string ConnectionString { get; set; }

        public string Sequence { get; set; }

        public Language Language { get; set; }

        public FieldNamingConvention FieldNamingConvention { get; set; }

        public FieldGenerationConvention FieldGenerationConvention { get; set; }

        public ValidationStyle ValidationStyle { get; set; }

        public string OwnerSchema
        {
            get
            {
                return ownerSchema;
            }
            set
            {
                ownerSchema = value;
                OnSettingsChanged("OwnerSchema");
            }
        }

        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
                OnSettingsChanged("TableName");
            }
        }

        public string DirFileEntity
        {
            get
            {
                return dirFileEntity;
            }
            set
            {
                dirFileEntity = value;
                OnSettingsChanged("DirFileEntity");
            }
        }

        public string DirFileEntityBO
        {
            get
            {
                return dirFileEntityBO;
            }
            set
            {
                dirFileEntityBO = value;
                OnSettingsChanged("DirFileEntityBO");
            }
        }

        public string DirFileForms
        {
            get
            {
                return dirFileForms;
            }
            set
            {
                dirFileForms = value;
                OnSettingsChanged("DirFileForms");
            }
        }

        public string NamespaceEntity
        {
            get
            {
                return namespaceEntity;
            }
            set
            {
                namespaceEntity = value;
                OnSettingsChanged("NamespaceEntity");
            }
        }

        public string NamespaceEntityBO
        {
            get
            {
                return namespaceEntityBO;
            }
            set
            {
                namespaceEntityBO = value;
                OnSettingsChanged("NamespaceEntityBO");
            }
        }

        public string NamespaceWebProject
        {
            get
            {
                return namespaceWebProject;
            }
            set
            {
                namespaceWebProject = value;
                OnSettingsChanged("NamespaceWebProject");
            }
        }

        public string GlobalizationLanguage
        {
            get
            {
                return globalizationLanguage;
            }
            set
            {
                globalizationLanguage = value;
                OnSettingsChanged("GlobalizationLanguage");
            }
        }

        public string EntityName
        {
            get
            {
                return entityName;
            }
            set
            {
                entityName = value;
                OnSettingsChanged("EntityName");
            }
        }

        public string ClassNamePrefix
        {
            get
            {
                return classNamePrefix;
            }
            set
            {
                classNamePrefix = value;
                OnSettingsChanged("ClassNamePrefix");
            }
        }
        #endregion

        #region Constructor
        public ApplicationSettings()
        {
            Connections = new List<Connection>();
            FieldNamingConvention = FieldNamingConvention.PascalCase;
            FieldGenerationConvention = FieldGenerationConvention.Property;
        }
        #endregion

        #region Methods
        public void Save()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var streamWriter = new StreamWriter(Path.Combine(folder, @"KPCodeGen.config"), false);
            using (streamWriter)
            {
                var xmlSerializer = new XmlSerializer(typeof(ApplicationSettings));
                xmlSerializer.Serialize(streamWriter, this);
            }
        }

        private void OnSettingsChanged(string propertyName)
        {
            /*
                if (EventSettingsChanged != null)
                {
                    SettingsEventArgs e = new SettingsEventArgs(propertyName);
                    EventSettingsChanged(this, e);
                }
             */
        }

        public static ApplicationSettings Load()
        {
            ApplicationSettings appSettings = null;
            var xmlSerializer = new XmlSerializer(typeof(ApplicationSettings));
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fi = new FileInfo(Path.Combine(folder, @"KPCodeGen.config"));
            if (fi.Exists)
            {
                try
                {
                    using (FileStream fileStream = fi.OpenRead())
                    {
                        appSettings = (ApplicationSettings)xmlSerializer.Deserialize(fileStream);
                    }
                }
                catch (Exception)
                {
                    appSettings = new ApplicationSettings();
                }
            }
            else
                appSettings = new ApplicationSettings();

            return appSettings;
        }
        #endregion
    }
}