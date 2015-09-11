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
 
using KPCodeGen.Core.Reader;
using KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;

namespace KPCodeGen.Core
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class MetadataFactory
    {
        public static AbstractMetadataReader GetReader(DatabaseServerType serverType, string connectionStr)
        {
            switch (serverType)
            {
                case DatabaseServerType.Oracle:
                    return new OracleMetadataReader(connectionStr);
                case DatabaseServerType.SqlServer:
                    return new SqlServerMetadataReader(connectionStr);
                case DatabaseServerType.MySQL:
                    return new MysqlMetadataReader(connectionStr);
                case DatabaseServerType.SQLite:
                    return new SqliteMetadataReader(connectionStr);
                case DatabaseServerType.Sybase:
                    return new SybaseMetadataReader(connectionStr);
                case DatabaseServerType.Ingres:
                    return new IngresMetadataReader(connectionStr);
                default:
                    return new NpgsqlMetadataReader(connectionStr);
            }
        }
    }
}