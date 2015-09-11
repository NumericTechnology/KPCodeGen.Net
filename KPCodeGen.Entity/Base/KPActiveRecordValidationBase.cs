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
using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.ORM;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KPCodeGen.Entity.Base
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPEntitySQLLiteBase<Entity> : ActiveRecordValidationBase<Entity>, IEntityData where Entity : class
    {
        [Browsable(false)]
        public Column Column { get; set; }

        public KPEntitySQLLiteBase()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            try
            {
                foreach (PropertyInfo property in this.GetType().GetProperties())
                {
                    var attributes = property.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                    if (attributes != null)
                    {
                        foreach (DefaultValueAttribute attibute in (DefaultValueAttribute[])attributes)
                        {
                            property.SetValue(this, attibute.Value, null);
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Entity GetEntityByMapping(KPMappingEntity mappingEntity, Column column)
        {
            Entity entity = ActiveRecordBase<Entity>.FindFirst(Expression.Eq("objIdKPMapping", mappingEntity));

            if (entity == null)
            {
                entity = (Entity)Activator.CreateInstance(typeof(Entity));
                PropertyInfo property = entity.GetType().GetProperty("objIdKPMapping");
                if (property != null)
                {
                    property.SetValue(entity, mappingEntity, null);
                }
            }

            PropertyInfo propertyIsRequired = entity.GetType().GetProperty("IsRequired");
            if (propertyIsRequired != null)
            {
                propertyIsRequired.SetValue(entity, !column.IsNullable, null);
            }

            PropertyInfo propertyColumn = entity.GetType().GetProperty("Column");
            if (propertyColumn != null)
            {
                propertyColumn.SetValue(entity, column, null);
            }

            return entity;
        }

        [Browsable(false)]
        public override System.Collections.IDictionary PropertiesValidationErrorMessages
        {
            get
            {
                return base.PropertiesValidationErrorMessages;
            }
        }

        [Browsable(false)]
        public override string[] ValidationErrorMessages
        {
            get
            {
                return base.ValidationErrorMessages;
            }
        }


    }
}
