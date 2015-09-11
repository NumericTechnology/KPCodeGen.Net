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
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using System.IO;
using KPCodeGen.EntityBO.Data.SQLLite;

namespace KPCodeGen.EntityBO.Data
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class ActiveRecordSQLLiteConfiguration : ActiveRecordConfiguration
    {
        public static bool Initialize(FileInfo databaseFile)
        {
            try
            {
                bool existsSchema = true;

                IDictionary<string, string> properties = new Dictionary<string, string>();

                properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
                properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
                properties.Add("query.substitutions", "true=1;false=0");
                properties.Add("proxyfactory.factory_class", "NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu");
                properties.Add("show_sql", "true");
                properties.Add("format_sql", "true");
                properties.Add("isDebug", "true");
                properties.Add("connection.connection_string", String.Format("Data Source={0};New=False;", databaseFile.FullName));

                InPlaceConfigurationSource source = new InPlaceConfigurationSource();
                source.Add(typeof(ActiveRecordBase), properties);

                List<Assembly> assembliesEntityc = new List<Assembly>();
                assembliesEntityc.Add(Assembly.Load("KPCodeGen.Entity"));
                if (!ActiveRecordConfiguration.IsInitialized)
                {
                    try
                    {
                        // Criação do Banco de dados SQLLite
                        if (!SQLLiteHelper.SQLLiteDatabaseFile.Exists)
                            existsSchema = false;

                        SQLLiteHelper.CreateDatabaseFile(databaseFile);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    ActiveRecordStarter.Initialize(assembliesEntityc.ToArray(), source);
                }

                if (!existsSchema)
                    ActiveRecordStarter.CreateSchema();
                else
                    ActiveRecordStarter.UpdateSchema();


                return ActiveRecordStarter.IsInitialized;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
