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
 
namespace KPCodeGen.Core.Util
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class StringConstants
    {
        public static string ORACLE_CONN_STR_TEMPLATE = "Data Source=database;User Id=user; Password=password;";

        public static string SQL_CONN_STR_TEMPLATE =
            "Data Source=localhost;Initial Catalog=database;User Id=user;Password=password;";

        public static string POSTGRESQL_CONN_STR_TEMPLATE =
            "server=localhost;Port=5432;Database=database;User Id=user;Password=password;";

        public static string MYSQL_CONN_STR_TEMPLATE =
            "Server=localhost;Port=3306;Database=database;Uid=user;Pwd=password;";

        public static string SQLITE_CONN_STR_TEMPLATE =
            "Data Source=database.db;Version=3;New=False;Compress=True;";

        public static string SYBASE_CONN_STR_TEMPLATE =
            "Provider=ASAProv;UID=user;PWD=password;DatabaseName=database;EngineName=enginename;CommLinks=TCPIP{host=servername}";

        public static string INGRES_CONN_STR_TEMPLATE = "Host=localhost;Port=II7;Database=database;User ID=user;Password=password;";

        public static string CUBRID_CONN_STR_TEMPLATE =
            "server=localhost;port=33000;database=database;user=user;password=password";
    }
}