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
using KPCodeGen.Attibute.Converter;
using KPCodeGen.Components;
using KPCodeGen.Core;
using KPCodeGen.Core.Delegates;
using KPCodeGen.Core.Generator;
using KPCodeGen.Core.Items;
using KPCodeGen.Core.Reader;
using KPCodeGen.Core.TextFormatter;
using KPCodeGen.Core.Util;
using KPCodeGen.Entity.Domain;
using KPCodeGen.Entity.ORM;
using KPCodeGen.EntityBO.BO;
using KPCodeGen.EntityBO.Data;
using KPCodeGen.Enumerator;
using KPCodeGen.Globalization;
using KPCodeGen.Entity.Extension;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KPCodeGen.Entity.Base;

namespace KPCodeGen
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public partial class StartForm : Form
    {
        #region Const Columns
        private const string COL_DISPLAYNAME = "col_DisplayName";
        private const string COL_COLUMNTYPEVIEW = "col_ColumnTypeView";
        private const string COL_COLUMNTYPEFORM = "col_ColumnTypeForm";
        private const string COL_DATAPRECISION = "col_DataPrecision";
        private const string COL_DATASIZE = "col_DataSize";
        private const string COL_MAPPEDDATATYPE = "col_MappedDataType";
        private const string COL_DATALENGTH = "col_DataLength";
        private const string COL_FOREIGNKEYCOLUMNNAME = "col_ForeignKeyColumnName";
        private const string COL_FOREIGNKEYTABLENAME = "col_ForeignKeyTableName";
        private const string COL_CONSTRAINTNAME = "col_ConstraintName";
        private const string COL_ISUNIQUEKEY = "col_IsUniqueKey";
        private const string COL_ISIDENTITY = "col_IsIdentity";
        private const string COL_ISNULLABLE = "col_IsNullable";
        private const string COL_ISFOREIGNKEY = "col_IsForeignKey";
        private const string COL_ISPRIMARYKEY = "col_IsPrimaryKey";
        private const string COL_CSHARPTYPE = "col_cSharpType";
        private const string COL_COLUMNDATATYPE = "col_ColumnDataType";
        private const string COL_COLUMNNAME = "col_ColumnName";
        private const string COL_DATASCALE = "col_DataScale";
        #endregion

        #region Properties
        private AbstractMetadataReader MetadataReader { get; set; }
        private ApplicationSettings ApplicationSettings { get; set; }
        private Connection CurrentConnection { get; set; }
        private List<Table> TablesList { get; set; }
        #endregion

        #region ControlsSpecialist
        // Estes componentes foram trocados pelo PropertyGrid
        /*
        private KPFormItemButtonControl FormItemButtonControl { get; set; }
        private KPFormItemCheckBoxControl FormItemCheckBoxControl { get; set; }
        private KPFormItemComboControl FormItemComboControl { get; set; }
        private KPFormItemDateTimeControl FormItemDateTimeControl { get; set; }
        private KPFormItemEntityControl FormItemEntityControl { get; set; }
        private KPFormItemGridControl FormItemGridControl { get; set; }
        private KPFormItemPasswordControl FormItemPasswordControl { get; set; }
        private KPFormItemTextControl FormItemTextControl { get; set; }
        private KPFormItemZoomControl FormItemZoomControl { get; set; }
        private KPBooleanModelControl BooleanModelControl { get; set; }
        private KPColumnModelControl ColumnModelControl { get; set; }
        private KPEntityModelControl EntityModelControl { get; set; }
        private KPEnumModelControl EnumModelControl { get; set; }
         */
        #endregion

        public StartForm()
        {
            ApplicationSettings = ApplicationSettings.Load();
            ApplicationSettings.EventSettingsChanged += ApplicationSettings_EventSettingsChanged;
            StringComboBoxTypeConverter.OnSelectColumn += StringComboBoxTypeConverter_OnSelectColumn;
            if (!String.IsNullOrWhiteSpace(ApplicationSettings.GlobalizationLanguage))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ApplicationSettings.GlobalizationLanguage);
            }

            InitializeComponent();
            BindData();
            LoadApplicationSettings();

            cmbSequencesOracle.Visible = false;
            cmbSequencesOracle.Enabled = false;
            lblSequenceTable.Visible = cmbSequencesOracle.Visible;
            txtTableFilter.Enabled = false;
        }

        protected string[] StringComboBoxTypeConverter_OnSelectColumn(ITypeDescriptorContext context)
        {
            List<string> stringList = new List<string>();
            stringList.Add(String.Empty);
            try
            {
                if (context != null && context.Instance != null)
                {
                    if (context.Instance is IEntityData)
                    {
                        var itemEntityData = (context.Instance as IEntityData);
                        if (itemEntityData.Column != null)
                        {
                            var column = itemEntityData.Column;
                            IList<Column> columnFK = MetadataReader.GetTableDetails(column.ForeignKeyTableName, column.Table.OwnerSchema);
                            foreach (Column item in columnFK)
                            {
                                stringList.Add(item.Name.GetFormattedText());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
            return stringList.ToArray();
        }

        private void ApplicationSettings_EventSettingsChanged(ApplicationSettings sender, SettingsEventArgs e)
        {
        }

        #region Methods

        #region MethodsData

        private void BindData()
        {
            col_cSharpType.DataSource = new DotNetTypes();
            col_ColumnTypeView.DataSource = new KPComponentsView();
            col_ColumnTypeForm.DataSource = new KPComponentsForms();

            cmbGlobalizingLanguage.Items.Clear();
            List<ItemKeyValue> itemKeyValueList = new List<ItemKeyValue>();
            ItemKeyValue languageDefault = new ItemKeyValue("pt-BR", "Português-Brazil");
            itemKeyValueList.Add(languageDefault);
            itemKeyValueList.Add(new ItemKeyValue("en-US", "Inglês-Americano"));
            itemKeyValueList.Add(new ItemKeyValue("es-ES", "Espanhol-Espanha"));
            cmbGlobalizingLanguage.ValueMember = "Key";
            cmbGlobalizingLanguage.DisplayMember = "Value";
            cmbGlobalizingLanguage.DataSource = itemKeyValueList;
            var itemLanguage = itemKeyValueList.FirstOrDefault(x => x.Key.Equals(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString()));
            if (itemLanguage != null)
            {
                cmbGlobalizingLanguage.SelectedItem = itemLanguage;
                ApplicationSettings.GlobalizationLanguage = itemLanguage.Key;
            }

        }

        private void PopulateSchemaOwners()
        {
            var owners = MetadataReader.GetOwners();
            if (owners == null || owners.Count == 0)
            {
                owners = new List<string> { "dbo" };
            }

            cmbSchemaOwners.Items.Clear();
            cmbSchemaOwners.Items.AddRange(owners.ToArray());
            cmbSchemaOwners.SelectedIndex = 0;
        }

        private void PopulateTablesAndSequences()
        {
            try
            {
                SetStatusView("Retrieving tables...");

                if (cmbSchemaOwners.SelectedItem == null)
                {
                    lstTable.DataSource = new List<Table>();
                    return;
                }
                ApplicationSettings.OwnerSchema = cmbSchemaOwners.SelectedItem.ToString();
                TablesList = MetadataReader.GetTables(ApplicationSettings.OwnerSchema);
                lstTable.Enabled = false;
                txtTableFilter.Enabled = false;
                lstTable.DataSource = TablesList;
                lstTable.DisplayMember = "Name";

                if (TablesList != null && TablesList.Any())
                {
                    lstTable.Enabled = true;
                    txtTableFilter.Enabled = true;
                    lstTable.SelectedItem = TablesList.FirstOrDefault();
                }

                var sequences = MetadataReader.GetSequences(ApplicationSettings.OwnerSchema);
                cmbSequencesOracle.Enabled = false;
                cmbSequencesOracle.Visible = false;
                cmbSequencesOracle.Items.Clear();
                if (sequences != null && sequences.Any())
                {
                    cmbSequencesOracle.Items.AddRange(sequences.ToArray());
                    cmbSequencesOracle.Visible = true;
                    cmbSequencesOracle.Enabled = true;
                    cmbSequencesOracle.SelectedIndex = 0;
                }
                lblSequenceTable.Visible = cmbSequencesOracle.Visible;

                SetStatusView(string.Empty);
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
        }

        private void PopulateTableDetails(Table selectedTable)
        {
            SetStatusView(string.Empty);
            try
            {
                gridViewDetails.DataSource = MetadataReader.GetTableDetails(selectedTable) ?? new List<Column>();
                GetTableHistorySettings(selectedTable);

                UpdateApplicationSettings(selectedTable, ApplicationSettings);
                var formatter = TextFormatterFactory.GetTextFormatter(ApplicationSettings);
                txtEntityName.Text = formatter.FormatText(selectedTable.Name);
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
        }

        private void GetTableHistorySettings(Table selectedTable)
        {
            using (ISession session = ActiveRecordConfiguration.GetISession())
            {
                IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                    .Where(x => x.OwnerSchema.Equals(selectedTable.OwnerSchema) &&
                                x.Database.Equals(selectedTable.DatabaseName) &&
                                x.Table.Equals(selectedTable.Name));

                foreach (KPMappingEntity entityMap in queryable.ToList())
                {
                    foreach (Column col in selectedTable.Columns)
                    {
                        if (col.Name.Equals(entityMap.Column))
                        {
                            if (!String.IsNullOrWhiteSpace(entityMap.DisplayName))
                                col.DisplayName = entityMap.DisplayName;

                            if (!String.IsNullOrWhiteSpace(entityMap.ColumnTypeForm))
                            {
                                KPComponentsFormsEnum enumComponentForm = KPComponentsFormsEnum.KPFormItemText;
                                if (Enum.TryParse<KPComponentsFormsEnum>(entityMap.ColumnTypeForm, out enumComponentForm))
                                    col.ColumnTypeForm = enumComponentForm;
                            }

                            if (!String.IsNullOrWhiteSpace(entityMap.ColumnTypeView))
                            {
                                KPComponentsViewEnum enumComponentView = KPComponentsViewEnum.KPColumnModel;
                                if (Enum.TryParse<KPComponentsViewEnum>(entityMap.ColumnTypeView, out enumComponentView))
                                    col.ColumnTypeView = enumComponentView;
                            }

                            if (!String.IsNullOrWhiteSpace(entityMap.CSharpType))
                                col.EntityTypeName = entityMap.CSharpType;

                            break;
                        }
                    }
                }
            }
        }

        #endregion MethodsData

        #endregion MethodsCreateEstrutura

        #region MethodsConfigure

        private void LoadApplicationSettings()
        {
            if (ApplicationSettings != null)
            {
                cmbConnectionName.DataSource = ApplicationSettings.Connections;
                cmbConnectionName.DisplayMember = "Name";
                if (ApplicationSettings.LastUsedConnection != null)
                {
                    var lastUsedConnection = ApplicationSettings.Connections.FirstOrDefault(connection => connection.Id == ApplicationSettings.LastUsedConnection);
                    CurrentConnection = lastUsedConnection ?? ApplicationSettings.Connections.FirstOrDefault();
                    cmbConnectionName.SelectedItem = CurrentConnection;
                }

                if (!String.IsNullOrWhiteSpace(ApplicationSettings.GlobalizationLanguage))
                {
                    var item = (cmbGlobalizingLanguage.DataSource as List<ItemKeyValue>).FirstOrDefault(x => x.Key.Equals(ApplicationSettings.GlobalizationLanguage));
                    if (item != null)
                        cmbGlobalizingLanguage.SelectedItem = item;
                }

                txtNamespaceEntity.Text = ApplicationSettings.NamespaceEntity;
                txtNamespaceEntityBO.Text = ApplicationSettings.NamespaceEntityBO;
                txtNamespaceWebProject.Text = ApplicationSettings.NamespaceWebProject;
                txtDirFileEntity.Text = ApplicationSettings.DirFileEntity;
                txtDirFileEntityBO.Text = ApplicationSettings.DirFileEntityBO;
                txtDirFileForms.Text = ApplicationSettings.DirFileForms;
            }
            else
            {
                CaptureApplicationSettings();
            }
        }

        private void UpdateApplicationSettings(Table tableName, ApplicationSettings appSettings)
        {
            string sequence = string.Empty;
            object sequenceName = null;
            if (cmbSequencesOracle.InvokeRequired)
            {
                cmbSequencesOracle.Invoke(new MethodInvoker(delegate
                {
                    sequenceName = cmbSequencesOracle.SelectedItem;
                }));
            }
            else
            {
                sequenceName = cmbSequencesOracle.SelectedItem;
            }

            if (sequenceName != null)
            {
                sequence = sequenceName.ToString();
            }

            string dirFileEntity = txtDirFileEntity.Text;
            if (!String.IsNullOrEmpty(dirFileEntity) && !Directory.Exists(dirFileEntity))
            {
                Directory.CreateDirectory(dirFileEntity);
            }

            string dirFileEntityBO = txtDirFileEntityBO.Text;
            if (!String.IsNullOrEmpty(dirFileEntityBO) && !Directory.Exists(dirFileEntityBO))
            {
                Directory.CreateDirectory(dirFileEntityBO);
            }

            string dirFileForms = txtDirFileForms.Text;
            if (!String.IsNullOrEmpty(dirFileForms) && !Directory.Exists(dirFileForms))
            {
                Directory.CreateDirectory(dirFileForms);
            }

            if (appSettings == null)
                appSettings = new ApplicationSettings();


            appSettings.DatabaseServerType = CurrentConnection.Type;
            appSettings.DirFileEntity = dirFileEntity;
            appSettings.DirFileEntityBO = dirFileEntityBO;
            appSettings.DirFileForms = dirFileForms;
            appSettings.OwnerSchema = cmbSchemaOwners.SelectedItem != null ? cmbSchemaOwners.SelectedItem.ToString() : String.Empty;
            appSettings.TableName = tableName.Name;
            appSettings.NamespaceEntityBO = txtNamespaceEntityBO.Text;
            appSettings.NamespaceEntity = txtNamespaceEntity.Text;
            appSettings.NamespaceWebProject = txtNamespaceWebProject.Text;
            appSettings.Sequence = sequence;
            appSettings.Language = Language.CSharp;
            appSettings.FieldNamingConvention = FieldNamingConvention.PascalCase;
            appSettings.FieldGenerationConvention = FieldGenerationConvention.Property;
            appSettings.ConnectionString = CurrentConnection.ConnectionString;
            appSettings.ClassNamePrefix = appSettings.ClassNamePrefix;
            appSettings.ValidationStyle = appSettings.ValidationStyle;
            appSettings.EntityName = txtEntityName.Text;

        }

        #endregion MethodsConfigure

        private void SetStatusView(string statusText)
        {
            stripLabelStatus.Text = statusText;
            statusStripFooter.Refresh();
        }

        private void CaptureApplicationSettings()
        {
            if (ApplicationSettings == null)
            {
                ApplicationSettings = new ApplicationSettings();
            }
            ApplicationSettings.NamespaceEntity = txtNamespaceEntity.Text;
            ApplicationSettings.NamespaceEntityBO = txtNamespaceEntityBO.Text;
            ApplicationSettings.NamespaceWebProject = txtNamespaceWebProject.Text;
            ApplicationSettings.Language = Language.CSharp;

            ApplicationSettings.ValidationStyle = ValidationStyle.NhibernateValidator;
            ApplicationSettings.DirFileEntity = txtDirFileEntity.Text;
            ApplicationSettings.DirFileEntityBO = txtDirFileEntityBO.Text;
            ApplicationSettings.DirFileForms = txtDirFileForms.Text;
            ApplicationSettings.FieldNamingConvention = FieldNamingConvention.PascalCase;
            ApplicationSettings.LastUsedConnection = CurrentConnection == null ? (Guid?)null : CurrentConnection.Id;

            if (CurrentConnection != null)
            {
                if (!ApplicationSettings.Connections.Exists(x => x.ConnectionString == CurrentConnection.ConnectionString))
                {
                    ApplicationSettings.Connections.Add(CurrentConnection);
                }
            }
        }

        private void Generate(Table table, ApplicationSettings appSettings)
        {
            UpdateApplicationSettings(table, appSettings);
            foreach (Column column in table.Columns)
            {
                if (column.EntityComponentForm == null)
                    column.EntityComponentForm = KPComponentsFormsBO.GetEntityControlForm(column);

                if (column.EntityComponentView == null)
                    column.EntityComponentView = KPComponentsViewBO.GetEntityControlView(column);
            }

            new ApplicationController(appSettings, MetadataReader).GenerateFiles(table);
        }

        private void GenerateAndDisplayCode(Table table)
        {
            table.PrimaryKey = MetadataReader.DeterminePrimaryKeys(table);
            table.ForeignKeys = MetadataReader.DetermineForeignKeyReferences(table);

            UpdateApplicationSettings(table, ApplicationSettings);
            var applicationController = new ApplicationController(ApplicationSettings, MetadataReader);
            applicationController.Generate(table);
            txtEntityCode.Text = applicationController.GeneratedEntityCode;
            txtEntityBOCode.Text = applicationController.GeneratedEntityBOCode;

            txtFormViewASPXCode.Text = applicationController.GeneratedFormsViewASPXCode;
            txtFormViewDESIGNERCode.Text = applicationController.GeneratedFormsDesignerCode;
            txtFormViewCSCode.Text = applicationController.GeneratedFormsViewCSCode;

            txtFormASPXCode.Text = applicationController.GeneratedFormsASPXCode;
            txtFormDESIGNERCode.Text = applicationController.GeneratedFormsDesignerCode;
            txtFormCSCode.Text = applicationController.GeneratedFormsCSCode;
        }

        private bool IsComponentFill()
        {
            if (!String.IsNullOrWhiteSpace(txtDirFileEntity.Text) &&
                !String.IsNullOrWhiteSpace(txtDirFileEntityBO.Text) &&
                !String.IsNullOrWhiteSpace(txtDirFileForms.Text) &&
                !String.IsNullOrWhiteSpace(txtNamespaceEntity.Text) &&
                !String.IsNullOrWhiteSpace(txtNamespaceEntityBO.Text) &&
                !String.IsNullOrWhiteSpace(txtNamespaceWebProject.Text))
                return false;

            return true;
        }

        #region Events
        private void BtnConnectDatabaseClicked(object sender, EventArgs e)
        {
            if (CurrentConnection == null)
                return;
            try
            {
                if (IsComponentFill())
                    throw new Exception("Favor preencher campos da configuração de geração.");

                SetStatusView(string.Format("Connecting to {0}...", CurrentConnection.Name));
                Cursor.Current = Cursors.WaitCursor;

                lstTable.DataSource = null;
                lstTable.DisplayMember = "Name";
                cmbSequencesOracle.Items.Clear();

                MetadataReader = MetadataFactory.GetReader(CurrentConnection.Type, CurrentConnection.ConnectionString);
                MetadataReader.ColumnsData += MetadataReader_ColumnsData;

                SetStatusView("Retrieving owners...");
                PopulateSchemaOwners();

                CaptureApplicationSettings();
                ApplicationSettings.Save();

                tabCodeGenerate.Enabled = true;
                tabConfiguration.Enabled = true;
                grbActions.Enabled = true;
                grbTables.Enabled = true;

                SetStatusView(string.Empty);
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private IList<Column> MetadataReader_ColumnsData(Table table, IList<Column> columns)
        {
            List<KPMappingEntity> KPMappingEntityList = KPMappingEntityBO.GetKPMappingEntity(table);

            foreach (Column itemColumn in columns)
            {
                itemColumn.ColumnTypeView = KPComponentsViewBO.GetKPComponentsView(itemColumn);
                itemColumn.ColumnTypeForm = KPComponentsFormsBO.GetKPComponentsForms(itemColumn);

                if (KPMappingEntityList != null)
                {
                    var objMapping = KPMappingEntityList.FirstOrDefault(x => x.Column.Equals(itemColumn.Name));
                    if (objMapping != null)
                    {
                        if (!String.IsNullOrWhiteSpace(objMapping.DisplayName))
                            itemColumn.DisplayName = objMapping.DisplayName;
                        if (!String.IsNullOrWhiteSpace(objMapping.CSharpType))
                            itemColumn.EntityTypeName = objMapping.CSharpType;
                    }
                }
            }

            return columns;
        }

        private void BtnDirFileEntitySelectButtonClick(object sender, EventArgs e)
        {
            var diagResult = folderBrowserDialog.ShowDialog();

            if (diagResult == DialogResult.OK)
            {
                txtDirFileEntity.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnDirFileEntityBoSelectButtonClick(object sender, EventArgs e)
        {
            var diagResult = folderBrowserDialog.ShowDialog();

            if (diagResult == DialogResult.OK)
            {
                txtDirFileEntityBO.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnDirFileFormsSelectButtonClick(object sender, EventArgs e)
        {
            var diagResult = folderBrowserDialog.ShowDialog();

            if (diagResult == DialogResult.OK)
            {
                txtDirFileForms.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void BtnGenerateCodeClicked(object sender, EventArgs e)
        {
            SetStatusView(string.Empty);
            var selectedItems = lstTable.SelectedItems;
            if (selectedItems.Count == 0)
            {
                SetStatusView(GlobalizationLanguage.GetString("KP_PleaseSelectTable"));
                return;
            }
            try
            {
                foreach (var selectedItem in selectedItems)
                {
                    SetStatusView(string.Format("Gerando {0} arquivos ...", selectedItem));
                    var table = (Table)selectedItem;
                    UpdatePropertiesControls();
                    CaptureApplicationSettings();
                    Generate(table, ApplicationSettings);
                }

                SetStatusView(@"Arquivos gerados com sucesso.");
                MessageBox.Show(@"Arquivos gerados com sucesso.");
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
        }

        private void TxtTableFilterTextChanged(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;

            // Display the full table list
            if (string.IsNullOrEmpty(textbox.Text))
            {
                SuspendLayout();
                lstTable.ClearSelected();
                lstTable.DataSource = TablesList;
                lstTable.SelectedItem = TablesList.FirstOrDefault();
                ResumeLayout();
                return;
            }

            // Display filtered list of tables
            var query = (from t in TablesList
                         where t.Name.ToLower().Contains(textbox.Text.ToLower())
                         select t).ToList();

            SuspendLayout();
            lstTable.ClearSelected();
            lstTable.DataSource = query;
            Table table = query.FirstOrDefault();
            if (table != null)
            {
                lstTable.SelectedItem = table;
                GenerateCodeGen(table);
            }
            ResumeLayout();
        }

        private void TxtTableFilterEnter(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox == null) return;

            if (textbox.Text == textbox.Tag.ToString())
            {
                textbox.TextChanged -= TxtTableFilterTextChanged;

                // Clear the hint text in the table filter textbox
                textbox.Text = string.Empty;

                textbox.TextChanged += TxtTableFilterTextChanged;
            }
        }

        private void StartFormFormClosing(object sender, FormClosingEventArgs e)
        {
            CaptureApplicationSettings();
            ApplicationSettings.Save();
        }

        private void LstTablesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            SetStatusView(string.Empty);
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (lstTable.SelectedIndex == -1)
                {
                    gridViewDetails.DataSource = new List<Column>();
                    return;
                }

                tabCodeGenerate.SelectedTab = tabTableDetail;

                if (IsComponentFill())
                    throw new Exception("Favor preencher campos da configuração de geração.");

                Table table = GetSelectedTable();
                PopulateTableDetails(table);
                CreateKPMappingEntityForAllColumns(table);
            }
            catch (Exception ex)
            {
                SetStatusView(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void CreateKPMappingEntityForAllColumns(Table table)
        {
            KPMappingEntity mappingEntity = null;
            using (ISession session = ActiveRecordConfiguration.GetISession())
            {
                foreach (Column column in table.Columns)
                {
                    IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                        .Where(x => x.OwnerSchema.Equals(table.OwnerSchema) &&
                                    x.Database.Equals(table.DatabaseName) &&
                                    x.Table.Equals(table.Name) &&
                                    x.Column.Equals(column.Name));


                    mappingEntity = queryable.FirstOrDefault();
                    if (mappingEntity == null)
                    {
                        mappingEntity = new KPMappingEntity()
                        {
                            OwnerSchema = table.OwnerSchema,
                            Database = table.DatabaseName,
                            Table = table.Name,
                            Column = column.Name
                        };

                        mappingEntity.SaveAndFlush();
                    }
                }
            }
        }

        private void GenerateCodeGen(Table table)
        {
            if (table != null)
            {
                CaptureApplicationSettings();

                PopulateTableDetails(table);

                GenerateAndDisplayCode(table);

                // Display entity name based on formatted table name
                UpdateApplicationSettings(table, ApplicationSettings);
                var formatter = TextFormatterFactory.GetTextFormatter(ApplicationSettings);
                txtEntityName.Text = formatter.FormatText(table.Name);
            }
        }

        #region ComboBox
        private void CmbConnectionNameComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConnectionName.SelectedItem == null) return;

            CurrentConnection = (Connection)cmbConnectionName.SelectedItem;

            lblSequenceTable.Hide();
            cmbSequencesOracle.Hide();

            if (CurrentConnection.Type == DatabaseServerType.Oracle)
            {
                lblSequenceTable.Show();
                cmbSequencesOracle.Show();
            }
        }

        private void CmbSchemaOwnersComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PopulateTablesAndSequences();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion


        private void BtnConnectionConfigClick(object sender, EventArgs e)
        {
            if (ApplicationSettings == null)
            {
                LoadApplicationSettings();
            }

            var connectionDialog = new ConnectionDialog();

            if (CurrentConnection != null)
            {
                connectionDialog.Connection = CurrentConnection;
            }

            var result = connectionDialog.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    CurrentConnection = connectionDialog.Connection;
                    var connectionToUpdate = ApplicationSettings.Connections.FirstOrDefault(connection => connection.Id == CurrentConnection.Id);

                    if (connectionToUpdate == null)
                    {
                        ApplicationSettings.Connections.Add(CurrentConnection);
                    }

                    break;
                case DialogResult.Abort:
                    ApplicationSettings.Connections.Remove(CurrentConnection);
                    CurrentConnection = null;
                    break;
            }

            cmbConnectionName.DataSource = null;
            cmbConnectionName.DataSource = ApplicationSettings.Connections;
            cmbConnectionName.DisplayMember = "Name";
            cmbConnectionName.SelectedItem = CurrentConnection;
        }

        #region GridView

        private void GridViewDetailsGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            SetStatusView(string.Format("Error in column {0} of row {1} - {3}. Detail : {2}", e.ColumnIndex, e.RowIndex, e.Exception.Message, (gridViewDetails.DataSource != null ? (gridViewDetails.DataSource as IList<Column>)[e.RowIndex].Name : "")));
        }

        private void gridViewDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Table table = GetSelectedTable();
                if (table == null)
                    return;

                string columnName = gridViewDetails[COL_COLUMNNAME, e.RowIndex].Value.ToString();
                string value = gridViewDetails[e.ColumnIndex, e.RowIndex].Value.ToString();
                using (ISession session = ActiveRecordConfiguration.GetISession())
                {
                    IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                        .Where(x => x.OwnerSchema.Equals(table.OwnerSchema) &&
                                    x.Database.Equals(table.DatabaseName) &&
                                    x.Table.Equals(table.Name) &&
                                    x.Column.Equals(columnName));


                    KPMappingEntity entityList = queryable.FirstOrDefault();
                    if (entityList == null)
                    {
                        entityList = new KPMappingEntity()
                        {
                            OwnerSchema = table.OwnerSchema,
                            Database = table.DatabaseName,
                            Table = table.Name,
                            Column = columnName
                        };
                    }

                    bool reloadComponentProperty = false;
                    switch (gridViewDetails.Columns[e.ColumnIndex].Name)
                    {
                        case COL_DISPLAYNAME:
                            entityList.DisplayName = value;
                            break;

                        case COL_COLUMNTYPEVIEW:
                            entityList.ColumnTypeView = value;
                            reloadComponentProperty = true;
                            break;

                        case COL_COLUMNTYPEFORM:
                            reloadComponentProperty = true;
                            entityList.ColumnTypeForm = value;
                            break;

                        case COL_CSHARPTYPE:
                            entityList.CSharpType = value;
                            break;
                        default:
                            return;
                    }

                    entityList.Save();

                    if (reloadComponentProperty)
                    {
                        LoadComponentsProperties(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void cmbGlobalizingLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbGlobalizingLanguage.SelectedItem != null)
            {
                ItemKeyValue item = cmbGlobalizingLanguage.SelectedItem as ItemKeyValue;
                ApplicationSettings.GlobalizationLanguage = item.Key;
                if (MessageBox.Show("Para alterar o idioma é necessário reiniciar a aplicação.\nQuer que eu faça isto para você? :-)", "Olá", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }
        #endregion Events

        private void gridViewDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LoadComponentsProperties(e.RowIndex);
        }

        private void LoadComponentsProperties(int rowIndex)
        {
            Table table = GetSelectedTable();
            if (table == null)
                return;

            string columnName = gridViewDetails[COL_COLUMNNAME, rowIndex].Value.ToString();
            KPMappingEntity mappingEntity = null;
            using (ISession session = ActiveRecordConfiguration.GetISession())
            {
                IQueryable<KPMappingEntity> queryable = session.Query<KPMappingEntity>()
                    .Where(x => x.OwnerSchema.Equals(table.OwnerSchema) &&
                                x.Database.Equals(table.DatabaseName) &&
                                x.Table.Equals(table.Name) &&
                                x.Column.Equals(columnName));


                mappingEntity = queryable.FirstOrDefault();
                if (mappingEntity == null)
                {
                    mappingEntity = new KPMappingEntity()
                    {
                        OwnerSchema = table.OwnerSchema,
                        Database = table.DatabaseName,
                        Table = table.Name,
                        Column = columnName
                    };

                    mappingEntity.SaveAndFlush();
                }
            }

            var entityColumn = gridViewDetails.Rows[rowIndex].DataBoundItem as Column;
            KPComponentsFormsEnum? componentFormEnum = gridViewDetails[COL_COLUMNTYPEFORM, rowIndex].Value as KPComponentsFormsEnum?;
            if (componentFormEnum.HasValue)
            {
                switch (componentFormEnum.Value)
                {
                    case KPComponentsFormsEnum.KPFormItemButton:
                        break;
                    case KPComponentsFormsEnum.KPFormItemCheckBox:
                        {
                            var entityControl = new KPFormItemCheckBoxEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemCombo:
                        {
                            var entityControl = new KPFormItemComboEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemEntity:
                        {
                            var entityControl = new KPFormItemEntityEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemGrid:
                        {
                            var entityControl = new KPFormItemGridEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemPassword:
                        {
                            var entityControl = new KPFormItemPasswordEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemText:
                        {
                            var entityControl = new KPFormItemTextEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            if (entityColumn.MappedDataType == typeof(DateTime)
                                || entityColumn.MappedDataType == typeof(DateTime?))
                                entityControl.MaskType = KPMaskTypeClassEnum.DATETIME;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemDateTime:
                        {
                            var entityControl = new KPFormItemDateTimeEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsFormsEnum.KPFormItemZoom:
                        {
                            var entityControl = new KPFormItemZoomEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (!entityColumn.IsNullable)
                                entityControl.IsRequired = true;
                            propertyForm.SelectedObject = entityControl;
                        }
                        break;
                    default:
                        propertyForm.SelectedObject = null;
                        break;
                }
            }

            KPComponentsViewEnum? componentViewEnum = gridViewDetails[COL_COLUMNTYPEVIEW, rowIndex].Value as KPComponentsViewEnum?;
            if (componentViewEnum.HasValue)
            {
                switch (componentViewEnum.Value)
                {
                    case KPComponentsViewEnum.KPColumnModel:
                        {
                            var entityControl = new KPColumnModelEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            if (entityColumn.IsPrimaryKey)
                                entityControl.IsVisible = false;
                            if (entityColumn.MappedDataType == typeof(DateTime)
                                || entityColumn.MappedDataType == typeof(DateTime?))
                                entityControl.MaskType = KPMaskTypeClassEnum.DATETIME;
                            propertyView.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsViewEnum.KPEnumModel:
                        {
                            var entityControl = new KPEnumModelEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            propertyView.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsViewEnum.KPEntityModel:
                        {
                            var entityControl = new KPEntityModelEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            propertyView.SelectedObject = entityControl;
                        }
                        break;
                    case KPComponentsViewEnum.KPBooleanModel:
                        {
                            var entityControl = new KPBooleanModelEntity().GetEntityByMapping(mappingEntity, entityColumn);
                            propertyView.SelectedObject = entityControl;
                        }
                        break;
                    default:
                        propertyView.SelectedObject = null;
                        break;
                }
            }
        }

        private Table GetSelectedTable()
        {
            Table table = null;
            if (lstTable.SelectedIndex >= 0)
                table = lstTable.Items[lstTable.SelectedIndex] as Table;

            return table;
        }

        private void tabCodeGenerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table table = GetSelectedTable();
            tabConfiguration.Enabled = false;
            UpdatePropertiesControls();

            if (tabCodeGenerate.SelectedTab.Equals(tabTableDetail))
            {
                tabConfiguration.Enabled = true;
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabEntityCode))
            {
                KPCastleEntityGenerator generator = new KPCastleEntityGenerator(ApplicationSettings, MetadataReader);
                txtEntityCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabEntityBOCode))
            {
                KPCastleEntityBOGenerator generator = new KPCastleEntityBOGenerator(ApplicationSettings, MetadataReader);
                txtEntityBOCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormViewASPX))
            {
                KPAspNetFormsViewGerenator generator = new KPAspNetFormsViewGerenator(ApplicationSettings, MetadataReader);

                txtFormViewASPXCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormViewDesigner))
            {
                KPAspNetFormsViewDesignerGerenator generator = new KPAspNetFormsViewDesignerGerenator(ApplicationSettings, MetadataReader);
                txtFormViewDESIGNERCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormViewCS))
            {
                KPAspNetFormsViewCSGerenator generator = new KPAspNetFormsViewCSGerenator(ApplicationSettings, MetadataReader);
                txtFormViewCSCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormASPX))
            {
                KPAspNetFormsGerenator generator = new KPAspNetFormsGerenator(ApplicationSettings, MetadataReader);
                txtFormASPXCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormDesigner))
            {
                KPAspNetFormsDesignerGerenator generator = new KPAspNetFormsDesignerGerenator(ApplicationSettings, MetadataReader);
                txtFormDESIGNERCode.Text = generator.Generate(table);
            }
            else if (tabCodeGenerate.SelectedTab.Equals(tabFormCS))
            {
                KPAspNetFormsCSGerenator generator = new KPAspNetFormsCSGerenator(ApplicationSettings, MetadataReader);
                txtFormCSCode.Text = generator.Generate(table);
            }
        }

        private void UpdatePropertiesControls()
        {
            Parallel.ForEach(gridViewDetails.DataSource as IList<Column>, column =>
                {
                    if (column.EntityComponentView == null)
                        column.EntityComponentView = KPComponentsViewBO.GetEntityControlView(column);
                    if (column.EntityComponentForm == null)
                        column.EntityComponentForm = KPComponentsFormsBO.GetEntityControlForm(column);
                });
        }

        private void propertyView_Leave(object sender, EventArgs e)
        {
            if (propertyView.SelectedObject == null)
                return;

            if (propertyView.SelectedObject is ActiveRecordBase)
            {
                ActiveRecordBase entity = propertyView.SelectedObject as ActiveRecordBase;
                entity.Save();
            }

            if (gridViewDetails.SelectedCells != null && gridViewDetails.SelectedCells.Count > 0)
            {
                int rowIndex = gridViewDetails.SelectedCells[0].RowIndex;
                Column column = gridViewDetails.Rows[rowIndex].DataBoundItem as Column;
                column.EntityComponentView = KPComponentsViewBO.GetEntityControlView(column);
            }
        }

        private void propertyForm_Leave(object sender, EventArgs e)
        {

            if (propertyForm.SelectedObject == null)
                return;

            if (propertyForm.SelectedObject is ActiveRecordBase)
            {
                ActiveRecordBase entity = propertyForm.SelectedObject as ActiveRecordBase;
                entity.Save();
            }

            if (gridViewDetails.SelectedCells != null && gridViewDetails.SelectedCells.Count > 0)
            {
                int rowIndex = gridViewDetails.SelectedCells[0].RowIndex;
                Column column = gridViewDetails.Rows[rowIndex].DataBoundItem as Column;
                column.EntityComponentForm = KPComponentsFormsBO.GetEntityControlForm(column);
            }
        }

    }
}