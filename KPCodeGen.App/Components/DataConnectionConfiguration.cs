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

using KPCodeGen.Enumerator;
using Microsoft.Data.ConnectionUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace KPCodeGen.Components
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class DataConnectionConfiguration : IDataConnectionConfiguration
    {
        private const string CONFIG_FILE_NAME = @"DataConnection.xml";
        private readonly string fullFilePath = null;
        private readonly XDocument xDoc = null;

        private IDictionary<string, DataSource> _dataSources;
        private IDictionary<string, DataProvider> _dataProviders;

        public DataConnectionConfiguration(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                fullFilePath = Path.GetFullPath(Path.Combine(path, CONFIG_FILE_NAME));
            }
            else
            {
                fullFilePath = Path.Combine(System.Environment.CurrentDirectory, CONFIG_FILE_NAME);
            }
            if (!String.IsNullOrEmpty(fullFilePath) && File.Exists(fullFilePath))
            {
                xDoc = XDocument.Load(fullFilePath);
            }
            else
            {
                xDoc = new XDocument();
                xDoc.Add(new XElement("ConnectionDialog", new XElement("DataSourceSelection")));
            }

            this.RootElement = xDoc.Root;
        }

        public XElement RootElement { get; set; }

        public void LoadConfiguration(DataConnectionDialog dialog, DatabaseServerType serverType)
        {

            this._dataSources = new Dictionary<string, DataSource>();
            this._dataProviders = new Dictionary<string, DataProvider>();

            switch (serverType)
            {
                case DatabaseServerType.Oracle:
                    dialog.DataSources.Add(DataSource.OracleDataSource);
                    dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OracleDataProvider);
                    this._dataSources.Add(DataSource.OracleDataSource.Name, DataSource.OracleDataSource);
                    this._dataProviders.Add(DataProvider.OracleDataProvider.Name, DataProvider.OracleDataProvider);
                    break;
                case DatabaseServerType.SqlServer:
                    dialog.DataSources.Add(DataSource.SqlDataSource);
                    this._dataSources.Add(DataSource.SqlDataSource.Name, DataSource.SqlDataSource);
                    this._dataProviders.Add(DataProvider.SqlDataProvider.Name, DataProvider.SqlDataProvider);
                    break;
                default:
                    dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OleDBDataProvider);
                    this._dataProviders.Add(DataProvider.OleDBDataProvider.Name, DataProvider.OleDBDataProvider);
                    dialog.DataSources.Add(dialog.UnspecifiedDataSource);
                    this._dataSources.Add(dialog.UnspecifiedDataSource.DisplayName, dialog.UnspecifiedDataSource);
                    break;
            }

            DataSource ds = null;
            string dsName = this.GetSelectedSource();
            if (!String.IsNullOrEmpty(dsName) && this._dataSources.TryGetValue(dsName, out ds))
            {
                dialog.SelectedDataSource = ds;
            }

            DataProvider dp = null;
            string dpName = this.GetSelectedProvider();
            if (!String.IsNullOrEmpty(dpName) && this._dataProviders.TryGetValue(dpName, out dp))
            {
                dialog.SelectedDataProvider = dp;
            }
        }

        public void SaveConfiguration(DataConnectionDialog dcd)
        {
            if (dcd.SaveSelection)
            {
                DataSource ds = dcd.SelectedDataSource;
                if (ds != null)
                {
                    if (ds == dcd.UnspecifiedDataSource)
                    {
                        this.SaveSelectedSource(ds.DisplayName);
                    }
                    else
                    {
                        this.SaveSelectedSource(ds.Name);
                    }
                }
                DataProvider dp = dcd.SelectedDataProvider;
                if (dp != null)
                {
                    this.SaveSelectedProvider(dp.Name);
                }

                xDoc.Save(fullFilePath);
            }
        }

        public string GetSelectedSource()
        {
            try
            {
                XElement xElem = this.RootElement.Element("DataSourceSelection");
                XElement sourceElem = xElem.Element("SelectedSource");
                if (sourceElem != null)
                {
                    return sourceElem.Value as string;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public string GetSelectedProvider()
        {
            try
            {
                XElement xElem = this.RootElement.Element("DataSourceSelection");
                XElement providerElem = xElem.Element("SelectedProvider");
                if (providerElem != null)
                {
                    return providerElem.Value as string;
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public void SaveSelectedSource(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                try
                {
                    XElement xElem = this.RootElement.Element("DataSourceSelection");
                    XElement sourceElem = xElem.Element("SelectedSource");
                    if (sourceElem != null)
                    {
                        sourceElem.Value = source;
                    }
                    else
                    {
                        xElem.Add(new XElement("SelectedSource", source));
                    }
                }
                catch
                {
                    throw;
                }
            }

        }

        public void SaveSelectedProvider(string provider)
        {
            if (!String.IsNullOrEmpty(provider))
            {
                try
                {
                    XElement xElem = this.RootElement.Element("DataSourceSelection");
                    XElement sourceElem = xElem.Element("SelectedProvider");
                    if (sourceElem != null)
                    {
                        sourceElem.Value = provider;
                    }
                    else
                    {
                        xElem.Add(new XElement("SelectedProvider", provider));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
