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
using System.Text;
using Castle.ActiveRecord.Attributes;
using  KPCodeGen.Entity.Domain;
using KPCodeGen.Enumerator;

namespace KPCodeGen.Core.Items
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class KPComponentsForms : List<KPComponentsFormsEnum>
    {
        public KPComponentsForms()
        {
            Add(KPComponentsFormsEnum.KPFormItemButton);
            Add(KPComponentsFormsEnum.KPFormItemCheckBox); //bool
            Add(KPComponentsFormsEnum.KPFormItemCombo);
            Add(KPComponentsFormsEnum.KPFormItemDateTime);
            Add(KPComponentsFormsEnum.KPFormItemEntity);
            Add(KPComponentsFormsEnum.KPFormItemGrid);
            Add(KPComponentsFormsEnum.KPFormItemKey); //pk
            Add(KPComponentsFormsEnum.KPFormItemPassword);
            //Add(KPComponentsFormsEnum.KPFormItemSpacer);
            Add(KPComponentsFormsEnum.KPFormItemText); //padrão
            Add(KPComponentsFormsEnum.KPFormItemZoom); //fk
        }
    }
}
