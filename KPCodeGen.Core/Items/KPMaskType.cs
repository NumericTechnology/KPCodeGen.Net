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
 
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;
using System;
using System.Collections.Generic;

namespace KPCodeGen.Core.Items
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPMaskType : List<KPMaskTypeClassEnum>
    {
        public KPMaskType()
        {
            Add(KPMaskTypeClassEnum.ALPHANUMERIC);
            Add(KPMaskTypeClassEnum.CGC);
            Add(KPMaskTypeClassEnum.CNPJ);
            Add(KPMaskTypeClassEnum.CPF);
            Add(KPMaskTypeClassEnum.DATE);
            Add(KPMaskTypeClassEnum.DATEHOUR);
            Add(KPMaskTypeClassEnum.DATEMINUTE);
            Add(KPMaskTypeClassEnum.DATETIME);
            Add(KPMaskTypeClassEnum.DECIMAL);
            Add(KPMaskTypeClassEnum.INTEGER);
            Add(KPMaskTypeClassEnum.LICENSEPLATE);
            Add(KPMaskTypeClassEnum.POSTCODE);
            Add(KPMaskTypeClassEnum.TELEPHONE);
            Add(KPMaskTypeClassEnum.TIME);
            Add(KPMaskTypeClassEnum.PERCENT);
            Add(KPMaskTypeClassEnum.MONEY);

            this.Sort();
        }

        public static KPMaskTypeClassEnum GetKPMaskType(Column column)
        {
            if ("TELEPHONE".Equals(column.Name.ToUpper()) || "TELEFONE".Equals(column.Name.ToUpper()) || "CELULAR".Equals(column.Name.ToUpper()) || "CELLPHONE".Equals(column.Name.ToUpper()) || "CELL PHONE".Equals(column.Name.ToUpper()) || "CELLULAR".Equals(column.Name.ToUpper()) || "PHONE".Equals(column.Name.ToUpper()))
            {
                return KPMaskTypeClassEnum.TELEPHONE;
            }
            else if ("POSTCODE".Equals(column.Name.ToUpper()) || "CEP".Equals(column.Name.ToUpper()))
            {
                return KPMaskTypeClassEnum.POSTCODE;
            }
            else if ("CNPJ".Equals(column.Name.ToUpper()))
            {
                return KPMaskTypeClassEnum.CNPJ;
            }
            else if ("CPF".Equals(column.Name.ToUpper()))
            {
                return KPMaskTypeClassEnum.CPF;
            }
            else if ("CPFCNPJ".Equals(column.Name.ToUpper()))
            {
                return KPMaskTypeClassEnum.CGC;
            }
            else if (typeof(DateTime) == column.MappedDataType)
            {
                return KPMaskTypeClassEnum.DATE;
            }
            else if (typeof(Int32) == column.MappedDataType)
            {
                return KPMaskTypeClassEnum.INTEGER;
            }
            return KPMaskTypeClassEnum.ALPHANUMERIC;
        }
    }
}
