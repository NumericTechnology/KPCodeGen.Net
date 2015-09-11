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
using System.ComponentModel;
using Castle.ActiveRecord;
using KPCodeGen.Enumerator;
using KPCodeGen.Entity.Extension;

namespace KPCodeGen.Entity.Domain
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public class Column : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Fields
        private string name;
        private string displayName;
        private KPComponentsViewEnum columnTypeView;
        private KPComponentsFormsEnum columnTypeForm;
        private ActiveRecordBase entityComponentView;
        private ActiveRecordBase entityComponentForm;
        #endregion

        #region Properties
        [Browsable(false)]
        public Table Table { get; private set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (String.IsNullOrEmpty(DisplayName))
                {
                    DisplayName = name.GetFormattedDisplayName();
                }
            }
        }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsUnique { get; set; }
        public bool IsIdentity { get; set; }
        public string DataType { get; set; }
        public int? DataLength { get; set; }
        public Type MappedDataType { get; set; }
        public bool IsNullable { get; set; }
        public string ConstraintName { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
        public string ForeignKeyTableName { get; set; }
        public string ForeignKeyColumnName { get; set; }
        #endregion

        #region Constructors
        public Column(Table tableFather) 
        {
            Table = tableFather;
        }
        #endregion

        #region Property ViewGrid
        public string DataSize
        {
            get
            {
                if (typeof(String) == MappedDataType)
                {
                    if (DataLength.HasValue && DataLength.Value == -1)
                        return "MAX";

                    return DataLength.HasValue ? DataLength.ToString() : "0";
                }
                else if (typeof(DateTime) == MappedDataType || typeof(DateTime?) == MappedDataType
                      || typeof(TimeSpan) == MappedDataType || typeof(TimeSpan?) == MappedDataType
                      || typeof(bool) == MappedDataType || typeof(bool?) == MappedDataType)
                {
                    return String.Empty;
                }
                else
                {
                    if (!DataPrecision.HasValue || !DataScale.HasValue)
                        return String.Empty;

                    return String.Concat(DataPrecision, ",", DataScale);
                }
            }
        }

        public string EntityTypeName
        {
            get
            {
                return MappedDataType != null ? MappedDataType.EntityTypeName() : "Invalid Type";
            }
            set
            {
                Type type = null;
                if (value.Contains("?"))
                {
                    string typeString = value.Substring(0, value.IndexOf("?", System.StringComparison.Ordinal));
                    type = Type.GetType(String.Concat("System.", typeString));
                    if (type != null)
                    {
                        MappedDataType = type.GetNullableType();
                    }
                }
                else
                {
                    type = Type.GetType(String.Concat("System.", value));
                    if (type != null)
                        MappedDataType = type;
                    else
                    {
                        // TODO: Consegui achar o tipo por AppDomain.CurrentDomain.GetAssemblies(), porém pelo Type.GetType não encontra, Uma solução é criar um método que busca o tipo dentro do Assembly
                        type = Type.GetType(String.Concat("CUBRID.Data.CUBRIDClient.", value));
                        if (type != null)
                            MappedDataType = type;
                    }
                }
                OnPropertyChanged("EntityTypeName");
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        public KPComponentsViewEnum ColumnTypeView
        {
            get
            {
                return columnTypeView;
            }
            set
            {
                columnTypeView = value;
                OnPropertyChanged("ColumnTypeView");
            }
        }

        
        public KPComponentsFormsEnum ColumnTypeForm
        {
            get
            {
                return columnTypeForm;
            }
            set
            {
                columnTypeForm = value;
                OnPropertyChanged("ColumnTypeForm");
            }
        }

        [Browsable(false)]
        public ActiveRecordBase EntityComponentView
        {
            get
            {
                return entityComponentView;
            }
            set
            {
                entityComponentView = value;
                OnPropertyChanged("EntityComponentView");
            }
        }

        [Browsable(false)]
        public ActiveRecordBase EntityComponentForm
        {
            get
            {
                return entityComponentForm;
            }
            set
            {
                entityComponentForm = value;
                OnPropertyChanged("EntityComponentForm");
            }
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                //handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
