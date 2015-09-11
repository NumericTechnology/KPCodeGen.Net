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
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.ORM;
using KPCodeGen.EntityBO.Data;
using KPCodeGen.Enumerator;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPCodeGen.EntityBO.BO
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPComponentsViewBO
    {
        public static KPComponentsViewEnum GetKPComponentsView(Column column)
        {
            if (column.MappedDataType == typeof(bool) || column.MappedDataType == typeof(bool?))
            {
                return KPComponentsViewEnum.KPBooleanModel;
            }
            else if (column.IsForeignKey)
            {
                return KPComponentsViewEnum.KPEntityModel;
            }
            return KPComponentsViewEnum.KPColumnModel;
        }

        public static ActiveRecordBase GetEntityControlView(Column column)
        {
            ActiveRecordBase entity = null;
            if (column == null)
                return null;

            Table selectedTable = column.Table;
            using (ISession session = ActiveRecordConfiguration.GetISession())
            {
                IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                    .Where(x => x.OwnerSchema.Equals(selectedTable.OwnerSchema) &&
                                x.Database.Equals(selectedTable.DatabaseName) &&
                                x.Table.Equals(selectedTable.Name) &&
                                x.Column.Equals(column.Name));

                KPMappingEntity mapEntity = queryable.FirstOrDefault();
                if (mapEntity != null)
                {
                    switch (column.ColumnTypeView)
                    {
                        case KPComponentsViewEnum.KPColumnModel:
                            entity = new KPColumnModelEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsViewEnum.KPEnumModel:
                            entity = new KPEnumModelEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsViewEnum.KPEntityModel:
                            entity = new KPEntityModelEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsViewEnum.KPBooleanModel:
                            entity = new KPBooleanModelEntity().GetEntityByMapping(mapEntity, column);
                            break;
                    }
                }
            }

            return entity;
        }
    }
}
