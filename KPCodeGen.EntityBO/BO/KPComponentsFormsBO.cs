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
    public class KPComponentsFormsBO
    {
        public static KPComponentsFormsEnum GetKPComponentsForms(Column column)
        {
            if (column.MappedDataType == typeof(bool) || column.MappedDataType == typeof(bool?))
            {
                return KPComponentsFormsEnum.KPFormItemCheckBox;
            }
            else if (column.MappedDataType == typeof(DateTime) || column.MappedDataType == typeof(DateTime?))
            {
                return KPComponentsFormsEnum.KPFormItemDateTime;
            }
            else if (column.IsForeignKey)
            {
                return KPComponentsFormsEnum.KPFormItemZoom;
            }
            else if (column.IsPrimaryKey)
            {
                return KPComponentsFormsEnum.KPFormItemKey;
            }
            return KPComponentsFormsEnum.KPFormItemText;
        }

        public static ActiveRecordBase GetEntityControlForm(Column column)
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
                    switch (column.ColumnTypeForm)
                    {
                        case KPComponentsFormsEnum.KPFormItemButton:
                            entity = new KPFormItemButtonEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemCheckBox:
                            entity = new KPFormItemCheckBoxEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemCombo:
                            entity = new KPFormItemComboEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemEntity:
                            entity = new KPFormItemEntityEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemGrid:
                            entity = new KPFormItemGridEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemPassword:
                            entity = new KPFormItemPasswordEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemText:
                            entity = new KPFormItemTextEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemDateTime:
                            entity = new KPFormItemDateTimeEntity().GetEntityByMapping(mapEntity, column);
                            break;
                        case KPComponentsFormsEnum.KPFormItemZoom:
                            entity = new KPFormItemZoomEntity().GetEntityByMapping(mapEntity, column);
                            break;
                    }
                }
            }

            return entity;
        }
    }
}
