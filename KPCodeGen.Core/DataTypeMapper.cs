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
using KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;

namespace KPCodeGen.Core
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class DataTypeMapper
    {
        public static bool IsNumericType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }

        public Type MapFromDBType(DatabaseServerType serverType, string dataType, int? dataLength, int? dataPrecision,
                                  int? dataScale, bool isPrimaryKey)
        {
            switch (serverType)
            {
                case DatabaseServerType.SqlServer:
                    return MapFromSqlServerDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
                case DatabaseServerType.Oracle:
                    return MapFromOracleDBType(dataType, dataLength, dataPrecision, dataScale);
                case DatabaseServerType.MySQL:
                    return MapFromMySqlDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
                case DatabaseServerType.PostgreSQL:
                    return MapFromPostgreDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
                case DatabaseServerType.Sybase:
                    return MapFromSqlServerDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
                case DatabaseServerType.Ingres:
                    return MapFromIngresDbType(dataType, dataLength, dataPrecision, dataScale);
                case DatabaseServerType.CUBRID:
                    return MapFromCUBRIDDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
            }
            return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
        }

        private Type MapFromSqlServerDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
        }

        private Type MapFromOracleDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale)
        {
            if (string.Equals(dataType, "DATE", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "TIMESTAMP", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "TIMESTAMP WITH TIME ZONE", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "TIMESTAMP WITH LOCAL TIME ZONE", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.DateTime);
            }

            if (string.Equals(dataType, "NUMBER", StringComparison.OrdinalIgnoreCase))
            {
                if (dataScale.GetValueOrDefault(0) == 0) //Integer type
                {
                    if (dataPrecision.GetValueOrDefault(0) >= 1 && dataPrecision.GetValueOrDefault(0) <= 4)
                        return typeof(System.Int16);
                    if (dataPrecision.GetValueOrDefault(0) >= 5 && dataPrecision.GetValueOrDefault(0) <= 9)
                        return typeof(System.Int32);
                    if (dataPrecision.GetValueOrDefault(0) >= 10 && dataPrecision.GetValueOrDefault(0) <= 18)
                        return typeof(System.Int64);
                }
                if (dataScale.GetValueOrDefault(0) > 0) //Floating type
                {
                    if (dataPrecision.GetValueOrDefault(0) >= 1 && dataPrecision.GetValueOrDefault(0) <= 7)
                        return typeof(System.Single);
                    if (dataPrecision.GetValueOrDefault(0) >= 8 && dataPrecision.GetValueOrDefault(0) <= 15)
                        return typeof(System.Double);
                }
                return typeof(System.Decimal);
            }

            if (string.Equals(dataType, "BLOB", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "BFILE *", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "LONG RAW", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "RAW", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(byte[]);
            }

            if (string.Equals(dataType, "INTERVAL DAY TO SECOND", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.TimeSpan);
            }

            if (string.Equals(dataType, "INTERVAL YEAR TO MONTH", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "LONG", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.Int64);
            }

            if (string.Equals(dataType, "BINARY_FLOAT", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.Single);
            }

            if (string.Equals(dataType, "BINARY_DOUBLE", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.Double);
            }

            if (string.Equals(dataType, "BINARY_INTEGER", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "FLOAT", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "REAL", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.Decimal);
            }

            return typeof(System.String);
        }

        private Type MapFromMySqlDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
        }

        private Type MapFromPostgreDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
        }

        private Type MapFromSqliteDbType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
        }

        private Type MapFromCUBRIDDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            switch (dataType)
            {
                case "MONETARY":
                    return typeof(decimal);
                case "SEQUENCE":
                    return typeof(object[]);
                case "SET":
                    return typeof(object[]);
                case "MULTISET":
                    return typeof(object[]);
                case "ENUM":
                    return typeof(String);
                case "CLOB":
                    return typeof(CUBRID.Data.CUBRIDClient.CUBRIDClob);
                case "BLOB":
                    return typeof(CUBRID.Data.CUBRIDClient.CUBRIDBlob);
                case "BIT VARYING":
                    return typeof(byte[]);
                default:
                    return MapFromDBType(dataType, dataLength, dataPrecision, dataScale, isPrimaryKey);
            }
        }

        private Type MapFromDBType(string dataType, int? dataLength, int? dataPrecision, int? dataScale, bool isPrimaryKey)
        {
            switch (dataType.ToUpperInvariant())
            {
                case "DATE":
                case "DATETIME":
                case "DATETIME2":
                case "TIMESTAMP":
                case "TIMESTAMP WITH TIME ZONE":
                case "TIMESTAMP WITH LOCAL TIME ZONE":
                case "SMALLDATETIME":
                case "TIME":
                    if (isPrimaryKey)
                        return typeof(DateTime);
                    else
                        return typeof(DateTime?);

                case "NUMBER":
                case "LONG":
                case "BIGINT":
                    if (isPrimaryKey)
                        return typeof(long);
                    else
                        return typeof(long?);

                case "SMALLINT":
                    if (isPrimaryKey)
                        return typeof(Int16);
                    else
                        return typeof(Int16?);


                case "TINYINT":
                    if (isPrimaryKey)
                        return typeof(Byte);
                    else
                        return typeof(Byte?);

                case "INT":
                case "INTERVAL YEAR TO MONTH":
                case "BINARY_INTEGER":
                case "INTEGER":
                    if (isPrimaryKey)
                        return typeof(int);
                    else
                        return typeof(int?);

                case "BINARY_DOUBLE":
                case "NUMERIC":
                    if (isPrimaryKey)
                        return typeof(double);
                    else
                        return typeof(double?);

                case "FLOAT":
                case "BINARY_FLOAT":
                    if (isPrimaryKey)
                        return typeof(float);
                    else
                        return typeof(float?);

                case "BLOB":
                case "BFILE *":
                case "LONG RAW":
                case "BINARY":
                case "IMAGE":
                case "VARBINARY":
                    return typeof(byte[]);

                case "INTERVAL DAY TO SECOND":
                    if (isPrimaryKey)
                        return typeof(TimeSpan);
                    else
                        return typeof(TimeSpan?);

                case "BIT":
                case "BOOLEAN":
                    if (isPrimaryKey)
                        return typeof(Boolean);
                    else
                        return typeof(Boolean?);

                case "DECIMAL":
                case "MONEY":
                case "SMALLMONEY":
                    if (isPrimaryKey)
                        return typeof(decimal);
                    else
                        return typeof(decimal?);

                case "REAL":
                    if (isPrimaryKey)
                        return typeof(Single);
                    else
                        return typeof(Single?);

                case "UNIQUEIDENTIFIER":
                    if (isPrimaryKey)
                        return typeof(Guid);
                    else
                        return typeof(Guid?);

                default:
                    return dataType.Contains("int")
                               ? typeof(int?)
                               : // CHAR, CLOB, NCLOB, NCHAR, XMLTYPE, VARCHAR2, NCHAR, NTEXT
                           typeof(string);
            }
        }

        private Type MapFromIngresDbType(string dataType, int? dataLength, int? dataPrecision, int? dataScale)
        {
            if (string.Equals(dataType, "INGRESDATE", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.DateTime);
            }

            if (string.Equals(dataType, "INTEGER", StringComparison.OrdinalIgnoreCase))
            {
                if (dataPrecision.HasValue)
                {
                    switch (dataPrecision.Value)
                    {
                        case 1:
                        case 2:
                            return typeof(System.Int16);
                        case 4:
                            return typeof(System.Int32);
                        case 8:
                            return typeof(System.Int64);
                    }
                }
            }

            if (string.Equals(dataType, "DECIMAL", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(dataType, "MONEY", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(System.Decimal);
            }

            if (string.Equals(dataType, "FLOAT", StringComparison.OrdinalIgnoreCase))
            {
                if (dataPrecision.HasValue)
                {
                    switch (dataPrecision.Value)
                    {
                        case 4:
                            return typeof(System.Single);
                        case 8:
                            return typeof(System.Double);
                    }
                }
            }

            if (string.Equals(dataType, "BYTE", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(byte[]);
            }

            return typeof(System.String);
        }

    }
}