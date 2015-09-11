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

using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.ORM;
using KPCodeGen.EntityBO.Data;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;

namespace KPCodeGen.EntityBO.BO
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPMappingEntityBO : KPBaseEntityBO<KPMappingEntity>
    {
        public static List<KPMappingEntity> GetKPMappingEntity(Table table)
        {
            List<KPMappingEntity> mappingEntityList = new List<KPMappingEntity>();
            using (ISession session = ActiveRecordConfiguration.GetISession())
            {
                IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                    .Where(x => x.OwnerSchema.Equals(table.OwnerSchema) &&
                                x.Database.Equals(table.DatabaseName) &&
                                x.Table.Equals(table.Name));

                mappingEntityList.AddRange(queryable.ToList());
            }

            return mappingEntityList;
        }
    }
}
