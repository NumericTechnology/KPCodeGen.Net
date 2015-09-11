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

using KPCodeGen.Properties;
using KPCodeGen.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace KPCodeGen
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grbTables = new System.Windows.Forms.GroupBox();
            this.txtTableFilter = new System.Windows.Forms.TextBox();
            this.lstTable = new System.Windows.Forms.ListBox();
            this.cmbSchemaOwners = new System.Windows.Forms.ComboBox();
            this.tabConfiguration = new System.Windows.Forms.TabControl();
            this.tabComponentView = new System.Windows.Forms.TabPage();
            this.propertyView = new System.Windows.Forms.PropertyGrid();
            this.tabComponentForm = new System.Windows.Forms.TabPage();
            this.propertyForm = new System.Windows.Forms.PropertyGrid();
            this.grbActions = new System.Windows.Forms.GroupBox();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.tabCodeGenerate = new System.Windows.Forms.TabControl();
            this.tabTableDetail = new System.Windows.Forms.TabPage();
            this.gridViewDetails = new System.Windows.Forms.DataGridView();
            this.col_ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ColumnDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cSharpType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_IsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_IsForeignKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_IsNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_IsIdentity = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_IsUniqueKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_ConstraintName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ForeignKeyTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ForeignKeyColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DataLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MappedDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DataSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DataPrecision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DataScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ColumnTypeView = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.col_ColumnTypeForm = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabEntityCode = new System.Windows.Forms.TabPage();
            this.txtEntityCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabEntityBOCode = new System.Windows.Forms.TabPage();
            this.txtEntityBOCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormViewASPX = new System.Windows.Forms.TabPage();
            this.txtFormViewASPXCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormViewDesigner = new System.Windows.Forms.TabPage();
            this.txtFormViewDESIGNERCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormViewCS = new System.Windows.Forms.TabPage();
            this.txtFormViewCSCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormASPX = new System.Windows.Forms.TabPage();
            this.txtFormASPXCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormDesigner = new System.Windows.Forms.TabPage();
            this.txtFormDESIGNERCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabFormCS = new System.Windows.Forms.TabPage();
            this.txtFormCSCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.lblConnStr = new System.Windows.Forms.Label();
            this.btnConnectDatabase = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.grbConnection = new System.Windows.Forms.GroupBox();
            this.lblSequenceTable = new System.Windows.Forms.Label();
            this.cmbSequencesOracle = new System.Windows.Forms.ComboBox();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.cmbConnectionName = new System.Windows.Forms.ComboBox();
            this.btnConnectionConfig = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.grbOthersConfig = new System.Windows.Forms.GroupBox();
            this.cmbGlobalizingLanguage = new System.Windows.Forms.ComboBox();
            this.lblGlobalization = new System.Windows.Forms.Label();
            this.grbConfigAssembly = new System.Windows.Forms.GroupBox();
            this.lblNamespaceWebProject = new System.Windows.Forms.Label();
            this.txtNamespaceWebProject = new System.Windows.Forms.TextBox();
            this.btnDirFileFormsSelect = new System.Windows.Forms.Button();
            this.lblDirFileForms = new System.Windows.Forms.Label();
            this.txtDirFileForms = new System.Windows.Forms.TextBox();
            this.lblDirFileEntityBO = new System.Windows.Forms.Label();
            this.lblDirFileEntity = new System.Windows.Forms.Label();
            this.txtDirFileEntityBO = new System.Windows.Forms.TextBox();
            this.txtDirFileEntity = new System.Windows.Forms.TextBox();
            this.btnDirFileEntityBOSelect = new System.Windows.Forms.Button();
            this.btnDirFileEntitySelect = new System.Windows.Forms.Button();
            this.lblNamespaceEntityBO = new System.Windows.Forms.Label();
            this.lblNamespaceEntity = new System.Windows.Forms.Label();
            this.txtNamespaceEntityBO = new System.Windows.Forms.TextBox();
            this.txtNamespaceEntity = new System.Windows.Forms.TextBox();
            this.statusStripFooter = new System.Windows.Forms.StatusStrip();
            this.stripLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grbTables.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.tabComponentView.SuspendLayout();
            this.tabComponentForm.SuspendLayout();
            this.grbActions.SuspendLayout();
            this.tabCodeGenerate.SuspendLayout();
            this.tabTableDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).BeginInit();
            this.tabEntityCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntityCode)).BeginInit();
            this.tabEntityBOCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntityBOCode)).BeginInit();
            this.tabFormViewASPX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewASPXCode)).BeginInit();
            this.tabFormViewDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewDESIGNERCode)).BeginInit();
            this.tabFormViewCS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewCSCode)).BeginInit();
            this.tabFormASPX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormASPXCode)).BeginInit();
            this.tabFormDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormDESIGNERCode)).BeginInit();
            this.tabFormCS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCSCode)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.grbConnection.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.grbOthersConfig.SuspendLayout();
            this.grbConfigAssembly.SuspendLayout();
            this.statusStripFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(3, 66);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grbTables);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabConfiguration);
            this.splitContainer.Panel2.Controls.Add(this.grbActions);
            this.splitContainer.Panel2.Controls.Add(this.tabCodeGenerate);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.splitContainer.Size = new System.Drawing.Size(1159, 613);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 23;
            // 
            // grbTables
            // 
            this.grbTables.Controls.Add(this.txtTableFilter);
            this.grbTables.Controls.Add(this.lstTable);
            this.grbTables.Controls.Add(this.cmbSchemaOwners);
            this.grbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTables.Enabled = false;
            this.grbTables.Location = new System.Drawing.Point(0, 0);
            this.grbTables.Name = "grbTables";
            this.grbTables.Size = new System.Drawing.Size(200, 613);
            this.grbTables.TabIndex = 0;
            this.grbTables.TabStop = false;
            this.grbTables.Text = "Owner do banco de dados";
            // 
            // txtTableFilter
            // 
            this.txtTableFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTableFilter.Location = new System.Drawing.Point(6, 47);
            this.txtTableFilter.Name = "txtTableFilter";
            this.txtTableFilter.Size = new System.Drawing.Size(189, 20);
            this.txtTableFilter.TabIndex = 1;
            this.txtTableFilter.Tag = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_FilterTable;
            this.txtTableFilter.Text = "Filtrar tabela...";
            this.txtTableFilter.TextChanged += new System.EventHandler(this.TxtTableFilterTextChanged);
            this.txtTableFilter.Enter += new System.EventHandler(this.TxtTableFilterEnter);
            // 
            // lstTable
            // 
            this.lstTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTable.FormattingEnabled = true;
            this.lstTable.Location = new System.Drawing.Point(6, 73);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(188, 537);
            this.lstTable.TabIndex = 2;
            this.lstTable.SelectedIndexChanged += new System.EventHandler(this.LstTablesListBoxSelectedIndexChanged);
            // 
            // cmbSchemaOwners
            // 
            this.cmbSchemaOwners.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSchemaOwners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchemaOwners.FormattingEnabled = true;
            this.cmbSchemaOwners.Location = new System.Drawing.Point(6, 20);
            this.cmbSchemaOwners.Name = "cmbSchemaOwners";
            this.cmbSchemaOwners.Size = new System.Drawing.Size(189, 21);
            this.cmbSchemaOwners.TabIndex = 0;
            this.cmbSchemaOwners.SelectedIndexChanged += new System.EventHandler(this.CmbSchemaOwnersComboBoxSelectedIndexChanged);
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConfiguration.Controls.Add(this.tabComponentView);
            this.tabConfiguration.Controls.Add(this.tabComponentForm);
            this.tabConfiguration.Enabled = false;
            this.tabConfiguration.Location = new System.Drawing.Point(706, 6);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.SelectedIndex = 0;
            this.tabConfiguration.Size = new System.Drawing.Size(249, 534);
            this.tabConfiguration.TabIndex = 3;
            // 
            // tabComponentView
            // 
            this.tabComponentView.Controls.Add(this.propertyView);
            this.tabComponentView.Location = new System.Drawing.Point(4, 22);
            this.tabComponentView.Name = "tabComponentView";
            this.tabComponentView.Padding = new System.Windows.Forms.Padding(3);
            this.tabComponentView.Size = new System.Drawing.Size(241, 508);
            this.tabComponentView.TabIndex = 0;
            this.tabComponentView.Text = "Componente View";
            this.tabComponentView.UseVisualStyleBackColor = true;
            // 
            // propertyView
            // 
            this.propertyView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyView.Location = new System.Drawing.Point(3, 3);
            this.propertyView.Name = "propertyView";
            this.propertyView.Size = new System.Drawing.Size(235, 502);
            this.propertyView.TabIndex = 2;
            this.propertyView.Leave += new System.EventHandler(this.propertyView_Leave);
            // 
            // tabComponentForm
            // 
            this.tabComponentForm.Controls.Add(this.propertyForm);
            this.tabComponentForm.Location = new System.Drawing.Point(4, 22);
            this.tabComponentForm.Name = "tabComponentForm";
            this.tabComponentForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabComponentForm.Size = new System.Drawing.Size(241, 508);
            this.tabComponentForm.TabIndex = 1;
            this.tabComponentForm.Text = "Componente Form";
            this.tabComponentForm.UseVisualStyleBackColor = true;
            // 
            // propertyForm
            // 
            this.propertyForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyForm.Location = new System.Drawing.Point(3, 3);
            this.propertyForm.Name = "propertyForm";
            this.propertyForm.Size = new System.Drawing.Size(235, 502);
            this.propertyForm.TabIndex = 3;
            this.propertyForm.Leave += new System.EventHandler(this.propertyForm_Leave);
            // 
            // grbActions
            // 
            this.grbActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grbActions.Controls.Add(this.btnGenerateCode);
            this.grbActions.Enabled = false;
            this.grbActions.Location = new System.Drawing.Point(706, 546);
            this.grbActions.Name = "grbActions";
            this.grbActions.Size = new System.Drawing.Size(249, 65);
            this.grbActions.TabIndex = 2;
            this.grbActions.TabStop = false;
            this.grbActions.Text = "Ações";
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Location = new System.Drawing.Point(155, 19);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(88, 31);
            this.btnGenerateCode.TabIndex = 24;
            this.btnGenerateCode.Text = "&Gerar arquivos";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.BtnGenerateCodeClicked);
            // 
            // tabCodeGenerate
            // 
            this.tabCodeGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCodeGenerate.Controls.Add(this.tabTableDetail);
            this.tabCodeGenerate.Controls.Add(this.tabEntityCode);
            this.tabCodeGenerate.Controls.Add(this.tabEntityBOCode);
            this.tabCodeGenerate.Controls.Add(this.tabFormViewASPX);
            this.tabCodeGenerate.Controls.Add(this.tabFormViewDesigner);
            this.tabCodeGenerate.Controls.Add(this.tabFormViewCS);
            this.tabCodeGenerate.Controls.Add(this.tabFormASPX);
            this.tabCodeGenerate.Controls.Add(this.tabFormDesigner);
            this.tabCodeGenerate.Controls.Add(this.tabFormCS);
            this.tabCodeGenerate.Enabled = false;
            this.tabCodeGenerate.Location = new System.Drawing.Point(0, 6);
            this.tabCodeGenerate.Name = "tabCodeGenerate";
            this.tabCodeGenerate.SelectedIndex = 0;
            this.tabCodeGenerate.Size = new System.Drawing.Size(704, 607);
            this.tabCodeGenerate.TabIndex = 0;
            this.tabCodeGenerate.SelectedIndexChanged += new System.EventHandler(this.tabCodeGenerate_SelectedIndexChanged);
            // 
            // tabTableDetail
            // 
            this.tabTableDetail.BackColor = System.Drawing.Color.White;
            this.tabTableDetail.CausesValidation = false;
            this.tabTableDetail.Controls.Add(this.gridViewDetails);
            this.tabTableDetail.Location = new System.Drawing.Point(4, 22);
            this.tabTableDetail.Name = "tabTableDetail";
            this.tabTableDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabTableDetail.Size = new System.Drawing.Size(696, 581);
            this.tabTableDetail.TabIndex = 0;
            this.tabTableDetail.Text = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_TableDefinition;
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.AllowUserToAddRows = false;
            this.gridViewDetails.AllowUserToDeleteRows = false;
            this.gridViewDetails.AllowUserToResizeRows = false;
            this.gridViewDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridViewDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ColumnName,
            this.col_ColumnDataType,
            this.col_cSharpType,
            this.col_IsPrimaryKey,
            this.col_IsForeignKey,
            this.col_IsNullable,
            this.col_IsIdentity,
            this.col_IsUniqueKey,
            this.col_ConstraintName,
            this.col_ForeignKeyTableName,
            this.col_ForeignKeyColumnName,
            this.col_DataLength,
            this.col_MappedDataType,
            this.col_DataSize,
            this.col_DataPrecision,
            this.col_DataScale,
            this.col_DisplayName,
            this.col_ColumnTypeView,
            this.col_ColumnTypeForm});
            this.gridViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewDetails.Location = new System.Drawing.Point(3, 3);
            this.gridViewDetails.MultiSelect = false;
            this.gridViewDetails.Name = "gridViewDetails";
            this.gridViewDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridViewDetails.Size = new System.Drawing.Size(690, 575);
            this.gridViewDetails.TabIndex = 0;
            this.gridViewDetails.VirtualMode = true;
            this.gridViewDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewDetails_CellEndEdit);
            this.gridViewDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GridViewDetailsGridViewDataError);
            this.gridViewDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewDetails_RowEnter);
            // 
            // col_ColumnName
            // 
            this.col_ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ColumnName.DataPropertyName = "Name";
            this.col_ColumnName.FillWeight = 40F;
            this.col_ColumnName.Frozen = true;
            this.col_ColumnName.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_Column;
            this.col_ColumnName.MinimumWidth = 130;
            this.col_ColumnName.Name = "col_ColumnName";
            this.col_ColumnName.ReadOnly = true;
            this.col_ColumnName.Width = 130;
            // 
            // col_ColumnDataType
            // 
            this.col_ColumnDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ColumnDataType.DataPropertyName = "DataType";
            this.col_ColumnDataType.FillWeight = 40F;
            this.col_ColumnDataType.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_Type;
            this.col_ColumnDataType.MinimumWidth = 50;
            this.col_ColumnDataType.Name = "col_ColumnDataType";
            this.col_ColumnDataType.ReadOnly = true;
            this.col_ColumnDataType.Width = 50;
            // 
            // col_cSharpType
            // 
            this.col_cSharpType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_cSharpType.DataPropertyName = "EntityTypeName";
            this.col_cSharpType.FillWeight = 40F;
            this.col_cSharpType.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_CSharpType;
            this.col_cSharpType.MinimumWidth = 80;
            this.col_cSharpType.Name = "col_cSharpType";
            this.col_cSharpType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_cSharpType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_cSharpType.Width = 80;
            // 
            // col_IsPrimaryKey
            // 
            this.col_IsPrimaryKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_IsPrimaryKey.DataPropertyName = "IsPrimaryKey";
            this.col_IsPrimaryKey.FillWeight = 40F;
            this.col_IsPrimaryKey.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_PK;
            this.col_IsPrimaryKey.MinimumWidth = 30;
            this.col_IsPrimaryKey.Name = "col_IsPrimaryKey";
            this.col_IsPrimaryKey.ReadOnly = true;
            this.col_IsPrimaryKey.ToolTipText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_PrimaryKey;
            this.col_IsPrimaryKey.Width = 30;
            // 
            // col_IsForeignKey
            // 
            this.col_IsForeignKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_IsForeignKey.DataPropertyName = "IsForeignKey";
            this.col_IsForeignKey.FillWeight = 40F;
            this.col_IsForeignKey.HeaderText = "FK";
            this.col_IsForeignKey.MinimumWidth = 30;
            this.col_IsForeignKey.Name = "col_IsForeignKey";
            this.col_IsForeignKey.ReadOnly = true;
            this.col_IsForeignKey.ToolTipText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_ForeingKey;
            this.col_IsForeignKey.Visible = false;
            this.col_IsForeignKey.Width = 30;
            // 
            // col_IsNullable
            // 
            this.col_IsNullable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_IsNullable.DataPropertyName = "IsNullable";
            this.col_IsNullable.FillWeight = 40F;
            this.col_IsNullable.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_Null;
            this.col_IsNullable.MinimumWidth = 30;
            this.col_IsNullable.Name = "col_IsNullable";
            this.col_IsNullable.ReadOnly = true;
            this.col_IsNullable.ToolTipText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_AllowNull;
            this.col_IsNullable.Width = 30;
            // 
            // col_IsIdentity
            // 
            this.col_IsIdentity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_IsIdentity.DataPropertyName = "IsIdentity";
            this.col_IsIdentity.FillWeight = 40F;
            this.col_IsIdentity.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_AI;
            this.col_IsIdentity.MinimumWidth = 30;
            this.col_IsIdentity.Name = "col_IsIdentity";
            this.col_IsIdentity.ReadOnly = true;
            this.col_IsIdentity.ToolTipText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_AutoIncrement;
            this.col_IsIdentity.Visible = false;
            this.col_IsIdentity.Width = 30;
            // 
            // col_IsUniqueKey
            // 
            this.col_IsUniqueKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_IsUniqueKey.DataPropertyName = "IsUnique";
            this.col_IsUniqueKey.FillWeight = 40F;
            this.col_IsUniqueKey.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_UN;
            this.col_IsUniqueKey.MinimumWidth = 30;
            this.col_IsUniqueKey.Name = "col_IsUniqueKey";
            this.col_IsUniqueKey.ReadOnly = true;
            this.col_IsUniqueKey.ToolTipText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_UniqueKey;
            this.col_IsUniqueKey.Visible = false;
            this.col_IsUniqueKey.Width = 30;
            // 
            // col_ConstraintName
            // 
            this.col_ConstraintName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ConstraintName.DataPropertyName = "ConstraintName";
            this.col_ConstraintName.FillWeight = 40F;
            this.col_ConstraintName.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_Constraint;
            this.col_ConstraintName.MinimumWidth = 130;
            this.col_ConstraintName.Name = "col_ConstraintName";
            this.col_ConstraintName.ReadOnly = true;
            this.col_ConstraintName.Visible = false;
            this.col_ConstraintName.Width = 130;
            // 
            // col_ForeignKeyTableName
            // 
            this.col_ForeignKeyTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ForeignKeyTableName.DataPropertyName = "ForeignKeyTableName";
            this.col_ForeignKeyTableName.FillWeight = 40F;
            this.col_ForeignKeyTableName.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_FKTable;
            this.col_ForeignKeyTableName.MinimumWidth = 80;
            this.col_ForeignKeyTableName.Name = "col_ForeignKeyTableName";
            this.col_ForeignKeyTableName.ReadOnly = true;
            // 
            // col_ForeignKeyColumnName
            // 
            this.col_ForeignKeyColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ForeignKeyColumnName.DataPropertyName = "ForeignKeyColumnName";
            this.col_ForeignKeyColumnName.FillWeight = 40F;
            this.col_ForeignKeyColumnName.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_FKColumn;
            this.col_ForeignKeyColumnName.MinimumWidth = 80;
            this.col_ForeignKeyColumnName.Name = "col_ForeignKeyColumnName";
            this.col_ForeignKeyColumnName.ReadOnly = true;
            // 
            // col_DataLength
            // 
            this.col_DataLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_DataLength.DataPropertyName = "DataLength";
            this.col_DataLength.FillWeight = 40F;
            this.col_DataLength.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_DataLength;
            this.col_DataLength.MinimumWidth = 40;
            this.col_DataLength.Name = "col_DataLength";
            this.col_DataLength.ReadOnly = true;
            this.col_DataLength.Visible = false;
            this.col_DataLength.Width = 40;
            // 
            // col_MappedDataType
            // 
            this.col_MappedDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_MappedDataType.DataPropertyName = "MappedDataType";
            this.col_MappedDataType.FillWeight = 40F;
            this.col_MappedDataType.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_MappedDataType;
            this.col_MappedDataType.MinimumWidth = 40;
            this.col_MappedDataType.Name = "col_MappedDataType";
            this.col_MappedDataType.ReadOnly = true;
            this.col_MappedDataType.Visible = false;
            this.col_MappedDataType.Width = 40;
            // 
            // col_DataSize
            // 
            this.col_DataSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_DataSize.DataPropertyName = "DataSize";
            this.col_DataSize.FillWeight = 40F;
            this.col_DataSize.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_Lenght;
            this.col_DataSize.MinimumWidth = 60;
            this.col_DataSize.Name = "col_DataSize";
            this.col_DataSize.ReadOnly = true;
            this.col_DataSize.Width = 60;
            // 
            // col_DataPrecision
            // 
            this.col_DataPrecision.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_DataPrecision.DataPropertyName = "DataPrecision";
            this.col_DataPrecision.FillWeight = 50F;
            this.col_DataPrecision.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_NumericPrecision;
            this.col_DataPrecision.MinimumWidth = 50;
            this.col_DataPrecision.Name = "col_DataPrecision";
            this.col_DataPrecision.ReadOnly = true;
            this.col_DataPrecision.Visible = false;
            this.col_DataPrecision.Width = 50;
            // 
            // col_DataScale
            // 
            this.col_DataScale.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_DataScale.DataPropertyName = "DataScale";
            this.col_DataScale.FillWeight = 50F;
            this.col_DataScale.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_NumericScale;
            this.col_DataScale.MinimumWidth = 50;
            this.col_DataScale.Name = "col_DataScale";
            this.col_DataScale.ReadOnly = true;
            this.col_DataScale.Visible = false;
            this.col_DataScale.Width = 50;
            // 
            // col_DisplayName
            // 
            this.col_DisplayName.DataPropertyName = "DisplayName";
            this.col_DisplayName.FillWeight = 40F;
            this.col_DisplayName.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_DisplayName;
            this.col_DisplayName.MinimumWidth = 110;
            this.col_DisplayName.Name = "col_DisplayName";
            // 
            // col_ColumnTypeView
            // 
            this.col_ColumnTypeView.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ColumnTypeView.DataPropertyName = "ColumnTypeView";
            this.col_ColumnTypeView.FillWeight = 40F;
            this.col_ColumnTypeView.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_ComponentView;
            this.col_ColumnTypeView.MinimumWidth = 120;
            this.col_ColumnTypeView.Name = "col_ColumnTypeView";
            this.col_ColumnTypeView.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_ColumnTypeView.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_ColumnTypeView.Width = 120;
            // 
            // col_ColumnTypeForm
            // 
            this.col_ColumnTypeForm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.col_ColumnTypeForm.DataPropertyName = "ColumnTypeForm";
            this.col_ColumnTypeForm.FillWeight = 40F;
            this.col_ColumnTypeForm.HeaderText = global::KPCodeGen.Properties.KPCodeGenResource.KPControl_ComponentForm;
            this.col_ColumnTypeForm.MinimumWidth = 130;
            this.col_ColumnTypeForm.Name = "col_ColumnTypeForm";
            this.col_ColumnTypeForm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_ColumnTypeForm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_ColumnTypeForm.Width = 130;
            // 
            // tabEntityCode
            // 
            this.tabEntityCode.Controls.Add(this.txtEntityCode);
            this.tabEntityCode.Location = new System.Drawing.Point(4, 22);
            this.tabEntityCode.Name = "tabEntityCode";
            this.tabEntityCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityCode.Size = new System.Drawing.Size(696, 581);
            this.tabEntityCode.TabIndex = 1;
            this.tabEntityCode.Text = "Entity";
            this.tabEntityCode.UseVisualStyleBackColor = true;
            // 
            // txtEntityCode
            // 
            this.txtEntityCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtEntityCode.BackBrush = null;
            this.txtEntityCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEntityCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtEntityCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEntityCode.IsReplaceMode = false;
            this.txtEntityCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtEntityCode.LeftBracket = '(';
            this.txtEntityCode.Location = new System.Drawing.Point(3, 3);
            this.txtEntityCode.Name = "txtEntityCode";
            this.txtEntityCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtEntityCode.ReadOnly = true;
            this.txtEntityCode.RightBracket = ')';
            this.txtEntityCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtEntityCode.Size = new System.Drawing.Size(690, 575);
            this.txtEntityCode.TabIndex = 0;
            // 
            // tabEntityBOCode
            // 
            this.tabEntityBOCode.Controls.Add(this.txtEntityBOCode);
            this.tabEntityBOCode.Location = new System.Drawing.Point(4, 22);
            this.tabEntityBOCode.Name = "tabEntityBOCode";
            this.tabEntityBOCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityBOCode.Size = new System.Drawing.Size(696, 581);
            this.tabEntityBOCode.TabIndex = 2;
            this.tabEntityBOCode.Text = "EntityBO";
            this.tabEntityBOCode.UseVisualStyleBackColor = true;
            // 
            // txtEntityBOCode
            // 
            this.txtEntityBOCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtEntityBOCode.BackBrush = null;
            this.txtEntityBOCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEntityBOCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtEntityBOCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEntityBOCode.IsReplaceMode = false;
            this.txtEntityBOCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtEntityBOCode.LeftBracket = '(';
            this.txtEntityBOCode.Location = new System.Drawing.Point(3, 3);
            this.txtEntityBOCode.Name = "txtEntityBOCode";
            this.txtEntityBOCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtEntityBOCode.ReadOnly = true;
            this.txtEntityBOCode.RightBracket = ')';
            this.txtEntityBOCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtEntityBOCode.Size = new System.Drawing.Size(690, 575);
            this.txtEntityBOCode.TabIndex = 0;
            // 
            // tabFormViewASPX
            // 
            this.tabFormViewASPX.Controls.Add(this.txtFormViewASPXCode);
            this.tabFormViewASPX.Location = new System.Drawing.Point(4, 22);
            this.tabFormViewASPX.Name = "tabFormViewASPX";
            this.tabFormViewASPX.Size = new System.Drawing.Size(696, 581);
            this.tabFormViewASPX.TabIndex = 3;
            this.tabFormViewASPX.Text = "FormView ASPX";
            this.tabFormViewASPX.UseVisualStyleBackColor = true;
            // 
            // txtFormViewASPXCode
            // 
            this.txtFormViewASPXCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormViewASPXCode.BackBrush = null;
            this.txtFormViewASPXCode.CommentPrefix = null;
            this.txtFormViewASPXCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormViewASPXCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormViewASPXCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormViewASPXCode.IsReplaceMode = false;
            this.txtFormViewASPXCode.Language = FastColoredTextBoxNS.Language.HTML;
            this.txtFormViewASPXCode.LeftBracket = '<';
            this.txtFormViewASPXCode.LeftBracket2 = '(';
            this.txtFormViewASPXCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormViewASPXCode.Name = "txtFormViewASPXCode";
            this.txtFormViewASPXCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormViewASPXCode.ReadOnly = true;
            this.txtFormViewASPXCode.RightBracket = '>';
            this.txtFormViewASPXCode.RightBracket2 = ')';
            this.txtFormViewASPXCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormViewASPXCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormViewASPXCode.TabIndex = 1;
            // 
            // tabFormViewDesigner
            // 
            this.tabFormViewDesigner.Controls.Add(this.txtFormViewDESIGNERCode);
            this.tabFormViewDesigner.Location = new System.Drawing.Point(4, 22);
            this.tabFormViewDesigner.Name = "tabFormViewDesigner";
            this.tabFormViewDesigner.Size = new System.Drawing.Size(696, 581);
            this.tabFormViewDesigner.TabIndex = 5;
            this.tabFormViewDesigner.Text = "FormView Designer";
            this.tabFormViewDesigner.UseVisualStyleBackColor = true;
            // 
            // txtFormViewDESIGNERCode
            // 
            this.txtFormViewDESIGNERCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormViewDESIGNERCode.BackBrush = null;
            this.txtFormViewDESIGNERCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormViewDESIGNERCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormViewDESIGNERCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormViewDESIGNERCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtFormViewDESIGNERCode.IsReplaceMode = false;
            this.txtFormViewDESIGNERCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtFormViewDESIGNERCode.LeftBracket = '(';
            this.txtFormViewDESIGNERCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormViewDESIGNERCode.Name = "txtFormViewDESIGNERCode";
            this.txtFormViewDESIGNERCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormViewDESIGNERCode.ReadOnly = true;
            this.txtFormViewDESIGNERCode.RightBracket = ')';
            this.txtFormViewDESIGNERCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormViewDESIGNERCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormViewDESIGNERCode.TabIndex = 1;
            // 
            // tabFormViewCS
            // 
            this.tabFormViewCS.Controls.Add(this.txtFormViewCSCode);
            this.tabFormViewCS.Location = new System.Drawing.Point(4, 22);
            this.tabFormViewCS.Name = "tabFormViewCS";
            this.tabFormViewCS.Size = new System.Drawing.Size(696, 581);
            this.tabFormViewCS.TabIndex = 6;
            this.tabFormViewCS.Text = "FormView CS";
            this.tabFormViewCS.UseVisualStyleBackColor = true;
            // 
            // txtFormViewCSCode
            // 
            this.txtFormViewCSCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormViewCSCode.BackBrush = null;
            this.txtFormViewCSCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormViewCSCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormViewCSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormViewCSCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtFormViewCSCode.IsReplaceMode = false;
            this.txtFormViewCSCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtFormViewCSCode.LeftBracket = '(';
            this.txtFormViewCSCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormViewCSCode.Name = "txtFormViewCSCode";
            this.txtFormViewCSCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormViewCSCode.ReadOnly = true;
            this.txtFormViewCSCode.RightBracket = ')';
            this.txtFormViewCSCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormViewCSCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormViewCSCode.TabIndex = 1;
            // 
            // tabFormASPX
            // 
            this.tabFormASPX.Controls.Add(this.txtFormASPXCode);
            this.tabFormASPX.Location = new System.Drawing.Point(4, 22);
            this.tabFormASPX.Name = "tabFormASPX";
            this.tabFormASPX.Size = new System.Drawing.Size(696, 581);
            this.tabFormASPX.TabIndex = 4;
            this.tabFormASPX.Text = "Form ASPX";
            this.tabFormASPX.UseVisualStyleBackColor = true;
            // 
            // txtFormASPXCode
            // 
            this.txtFormASPXCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormASPXCode.BackBrush = null;
            this.txtFormASPXCode.CommentPrefix = null;
            this.txtFormASPXCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormASPXCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormASPXCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormASPXCode.IsReplaceMode = false;
            this.txtFormASPXCode.Language = FastColoredTextBoxNS.Language.HTML;
            this.txtFormASPXCode.LeftBracket = '<';
            this.txtFormASPXCode.LeftBracket2 = '(';
            this.txtFormASPXCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormASPXCode.Name = "txtFormASPXCode";
            this.txtFormASPXCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormASPXCode.ReadOnly = true;
            this.txtFormASPXCode.RightBracket = '>';
            this.txtFormASPXCode.RightBracket2 = ')';
            this.txtFormASPXCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormASPXCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormASPXCode.TabIndex = 2;
            // 
            // tabFormDesigner
            // 
            this.tabFormDesigner.Controls.Add(this.txtFormDESIGNERCode);
            this.tabFormDesigner.Location = new System.Drawing.Point(4, 22);
            this.tabFormDesigner.Name = "tabFormDesigner";
            this.tabFormDesigner.Size = new System.Drawing.Size(696, 581);
            this.tabFormDesigner.TabIndex = 7;
            this.tabFormDesigner.Text = "Form Designer";
            this.tabFormDesigner.UseVisualStyleBackColor = true;
            // 
            // txtFormDESIGNERCode
            // 
            this.txtFormDESIGNERCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormDESIGNERCode.BackBrush = null;
            this.txtFormDESIGNERCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormDESIGNERCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormDESIGNERCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormDESIGNERCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtFormDESIGNERCode.IsReplaceMode = false;
            this.txtFormDESIGNERCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtFormDESIGNERCode.LeftBracket = '(';
            this.txtFormDESIGNERCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormDESIGNERCode.Name = "txtFormDESIGNERCode";
            this.txtFormDESIGNERCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormDESIGNERCode.ReadOnly = true;
            this.txtFormDESIGNERCode.RightBracket = ')';
            this.txtFormDESIGNERCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormDESIGNERCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormDESIGNERCode.TabIndex = 1;
            // 
            // tabFormCS
            // 
            this.tabFormCS.Controls.Add(this.txtFormCSCode);
            this.tabFormCS.Location = new System.Drawing.Point(4, 22);
            this.tabFormCS.Name = "tabFormCS";
            this.tabFormCS.Size = new System.Drawing.Size(696, 581);
            this.tabFormCS.TabIndex = 8;
            this.tabFormCS.Text = "Form CS";
            this.tabFormCS.UseVisualStyleBackColor = true;
            // 
            // txtFormCSCode
            // 
            this.txtFormCSCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtFormCSCode.BackBrush = null;
            this.txtFormCSCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFormCSCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtFormCSCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormCSCode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtFormCSCode.IsReplaceMode = false;
            this.txtFormCSCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtFormCSCode.LeftBracket = '(';
            this.txtFormCSCode.Location = new System.Drawing.Point(0, 0);
            this.txtFormCSCode.Name = "txtFormCSCode";
            this.txtFormCSCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtFormCSCode.ReadOnly = true;
            this.txtFormCSCode.RightBracket = ')';
            this.txtFormCSCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtFormCSCode.Size = new System.Drawing.Size(696, 581);
            this.txtFormCSCode.TabIndex = 1;
            // 
            // lblConnStr
            // 
            this.lblConnStr.AutoSize = true;
            this.lblConnStr.Location = new System.Drawing.Point(2, 15);
            this.lblConnStr.Name = "lblConnStr";
            this.lblConnStr.Size = new System.Drawing.Size(105, 13);
            this.lblConnStr.TabIndex = 0;
            this.lblConnStr.Text = "Conexão com banco";
            // 
            // btnConnectDatabase
            // 
            this.btnConnectDatabase.Location = new System.Drawing.Point(234, 30);
            this.btnConnectDatabase.Name = "btnConnectDatabase";
            this.btnConnectDatabase.Size = new System.Drawing.Size(59, 23);
            this.btnConnectDatabase.TabIndex = 3;
            this.btnConnectDatabase.Text = "&Conectar";
            this.btnConnectDatabase.UseVisualStyleBackColor = true;
            this.btnConnectDatabase.Click += new System.EventHandler(this.BtnConnectDatabaseClicked);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabBasic);
            this.tabMain.Controls.Add(this.tabSettings);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1173, 708);
            this.tabMain.TabIndex = 0;
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.splitContainer);
            this.tabBasic.Controls.Add(this.grbConnection);
            this.tabBasic.Location = new System.Drawing.Point(4, 22);
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabBasic.Size = new System.Drawing.Size(1165, 682);
            this.tabBasic.TabIndex = 1;
            this.tabBasic.Text = "Basico";
            this.tabBasic.UseVisualStyleBackColor = true;
            // 
            // grbConnection
            // 
            this.grbConnection.Controls.Add(this.lblSequenceTable);
            this.grbConnection.Controls.Add(this.cmbSequencesOracle);
            this.grbConnection.Controls.Add(this.lblEntityName);
            this.grbConnection.Controls.Add(this.txtEntityName);
            this.grbConnection.Controls.Add(this.cmbConnectionName);
            this.grbConnection.Controls.Add(this.lblConnStr);
            this.grbConnection.Controls.Add(this.btnConnectionConfig);
            this.grbConnection.Controls.Add(this.btnConnectDatabase);
            this.grbConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbConnection.Location = new System.Drawing.Point(3, 3);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.Size = new System.Drawing.Size(1159, 63);
            this.grbConnection.TabIndex = 0;
            this.grbConnection.TabStop = false;
            this.grbConnection.Tag = "";
            // 
            // lblSequenceTable
            // 
            this.lblSequenceTable.AutoSize = true;
            this.lblSequenceTable.Location = new System.Drawing.Point(594, 15);
            this.lblSequenceTable.Name = "lblSequenceTable";
            this.lblSequenceTable.Size = new System.Drawing.Size(174, 13);
            this.lblSequenceTable.TabIndex = 23;
            this.lblSequenceTable.Text = "Sequencia para tabela selecionada";
            // 
            // cmbSequencesOracle
            // 
            this.cmbSequencesOracle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSequencesOracle.DropDownWidth = 449;
            this.cmbSequencesOracle.FormattingEnabled = true;
            this.cmbSequencesOracle.Location = new System.Drawing.Point(597, 31);
            this.cmbSequencesOracle.Name = "cmbSequencesOracle";
            this.cmbSequencesOracle.Size = new System.Drawing.Size(171, 21);
            this.cmbSequencesOracle.TabIndex = 24;
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(296, 15);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(67, 13);
            this.lblEntityName.TabIndex = 21;
            this.lblEntityName.Text = "Entity Name:";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(299, 31);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.ReadOnly = true;
            this.txtEntityName.Size = new System.Drawing.Size(292, 20);
            this.txtEntityName.TabIndex = 22;
            // 
            // cmbConnectionName
            // 
            this.cmbConnectionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionName.FormattingEnabled = true;
            this.cmbConnectionName.Location = new System.Drawing.Point(6, 31);
            this.cmbConnectionName.Name = "cmbConnectionName";
            this.cmbConnectionName.Size = new System.Drawing.Size(187, 21);
            this.cmbConnectionName.Sorted = true;
            this.cmbConnectionName.TabIndex = 1;
            this.cmbConnectionName.SelectedIndexChanged += new System.EventHandler(this.CmbConnectionNameComboBoxSelectedIndexChanged);
            // 
            // btnConnectionConfig
            // 
            this.btnConnectionConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConnectionConfig.Image")));
            this.btnConnectionConfig.Location = new System.Drawing.Point(198, 30);
            this.btnConnectionConfig.Name = "btnConnectionConfig";
            this.btnConnectionConfig.Size = new System.Drawing.Size(30, 23);
            this.btnConnectionConfig.TabIndex = 2;
            this.btnConnectionConfig.UseVisualStyleBackColor = true;
            this.btnConnectionConfig.Click += new System.EventHandler(this.BtnConnectionConfigClick);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.grbOthersConfig);
            this.tabSettings.Controls.Add(this.grbConfigAssembly);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(1165, 682);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Configurações";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // grbOthersConfig
            // 
            this.grbOthersConfig.Controls.Add(this.cmbGlobalizingLanguage);
            this.grbOthersConfig.Controls.Add(this.lblGlobalization);
            this.grbOthersConfig.Location = new System.Drawing.Point(8, 202);
            this.grbOthersConfig.Name = "grbOthersConfig";
            this.grbOthersConfig.Size = new System.Drawing.Size(661, 62);
            this.grbOthersConfig.TabIndex = 1;
            this.grbOthersConfig.TabStop = false;
            this.grbOthersConfig.Text = "Configurações gerais";
            // 
            // cmbGlobalizingLanguage
            // 
            this.cmbGlobalizingLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGlobalizingLanguage.FormattingEnabled = true;
            this.cmbGlobalizingLanguage.Location = new System.Drawing.Point(139, 28);
            this.cmbGlobalizingLanguage.Name = "cmbGlobalizingLanguage";
            this.cmbGlobalizingLanguage.Size = new System.Drawing.Size(146, 21);
            this.cmbGlobalizingLanguage.Sorted = true;
            this.cmbGlobalizingLanguage.TabIndex = 1;
            this.cmbGlobalizingLanguage.SelectionChangeCommitted += new System.EventHandler(this.cmbGlobalizingLanguage_SelectionChangeCommitted);
            // 
            // lblGlobalization
            // 
            this.lblGlobalization.AutoSize = true;
            this.lblGlobalization.Location = new System.Drawing.Point(58, 32);
            this.lblGlobalization.Name = "lblGlobalization";
            this.lblGlobalization.Size = new System.Drawing.Size(77, 13);
            this.lblGlobalization.TabIndex = 0;
            this.lblGlobalization.Text = "Idioma padrão:";
            // 
            // grbConfigAssembly
            // 
            this.grbConfigAssembly.Controls.Add(this.lblNamespaceWebProject);
            this.grbConfigAssembly.Controls.Add(this.txtNamespaceWebProject);
            this.grbConfigAssembly.Controls.Add(this.btnDirFileFormsSelect);
            this.grbConfigAssembly.Controls.Add(this.lblDirFileForms);
            this.grbConfigAssembly.Controls.Add(this.txtDirFileForms);
            this.grbConfigAssembly.Controls.Add(this.lblDirFileEntityBO);
            this.grbConfigAssembly.Controls.Add(this.lblDirFileEntity);
            this.grbConfigAssembly.Controls.Add(this.txtDirFileEntityBO);
            this.grbConfigAssembly.Controls.Add(this.txtDirFileEntity);
            this.grbConfigAssembly.Controls.Add(this.btnDirFileEntityBOSelect);
            this.grbConfigAssembly.Controls.Add(this.btnDirFileEntitySelect);
            this.grbConfigAssembly.Controls.Add(this.lblNamespaceEntityBO);
            this.grbConfigAssembly.Controls.Add(this.lblNamespaceEntity);
            this.grbConfigAssembly.Controls.Add(this.txtNamespaceEntityBO);
            this.grbConfigAssembly.Controls.Add(this.txtNamespaceEntity);
            this.grbConfigAssembly.Location = new System.Drawing.Point(8, 6);
            this.grbConfigAssembly.Name = "grbConfigAssembly";
            this.grbConfigAssembly.Size = new System.Drawing.Size(661, 190);
            this.grbConfigAssembly.TabIndex = 0;
            this.grbConfigAssembly.TabStop = false;
            this.grbConfigAssembly.Text = "Configuração de geração";
            // 
            // lblNamespaceWebProject
            // 
            this.lblNamespaceWebProject.AutoSize = true;
            this.lblNamespaceWebProject.Location = new System.Drawing.Point(6, 162);
            this.lblNamespaceWebProject.Name = "lblNamespaceWebProject";
            this.lblNamespaceWebProject.Size = new System.Drawing.Size(132, 13);
            this.lblNamespaceWebProject.TabIndex = 13;
            this.lblNamespaceWebProject.Text = "Namespace (WebProject):";
            // 
            // txtNamespaceWebProject
            // 
            this.txtNamespaceWebProject.Location = new System.Drawing.Point(139, 158);
            this.txtNamespaceWebProject.Name = "txtNamespaceWebProject";
            this.txtNamespaceWebProject.Size = new System.Drawing.Size(477, 20);
            this.txtNamespaceWebProject.TabIndex = 14;
            // 
            // btnDirFileFormsSelect
            // 
            this.btnDirFileFormsSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnDirFileFormsSelect.Image")));
            this.btnDirFileFormsSelect.Location = new System.Drawing.Point(623, 80);
            this.btnDirFileFormsSelect.Name = "btnDirFileFormsSelect";
            this.btnDirFileFormsSelect.Size = new System.Drawing.Size(29, 23);
            this.btnDirFileFormsSelect.TabIndex = 8;
            this.btnDirFileFormsSelect.UseVisualStyleBackColor = true;
            this.btnDirFileFormsSelect.Click += new System.EventHandler(this.BtnDirFileFormsSelectButtonClick);
            // 
            // lblDirFileForms
            // 
            this.lblDirFileForms.AutoSize = true;
            this.lblDirFileForms.Location = new System.Drawing.Point(40, 85);
            this.lblDirFileForms.Name = "lblDirFileForms";
            this.lblDirFileForms.Size = new System.Drawing.Size(98, 13);
            this.lblDirFileForms.TabIndex = 6;
            this.lblDirFileForms.Text = "Dir Arquivos Forms:";
            // 
            // txtDirFileForms
            // 
            this.txtDirFileForms.Location = new System.Drawing.Point(139, 80);
            this.txtDirFileForms.Name = "txtDirFileForms";
            this.txtDirFileForms.Size = new System.Drawing.Size(477, 20);
            this.txtDirFileForms.TabIndex = 7;
            // 
            // lblDirFileEntityBO
            // 
            this.lblDirFileEntityBO.AutoSize = true;
            this.lblDirFileEntityBO.Location = new System.Drawing.Point(30, 57);
            this.lblDirFileEntityBO.Name = "lblDirFileEntityBO";
            this.lblDirFileEntityBO.Size = new System.Drawing.Size(108, 13);
            this.lblDirFileEntityBO.TabIndex = 3;
            this.lblDirFileEntityBO.Text = "Dir. arquivo EntityBO:";
            // 
            // lblDirFileEntity
            // 
            this.lblDirFileEntity.AutoSize = true;
            this.lblDirFileEntity.Location = new System.Drawing.Point(45, 31);
            this.lblDirFileEntity.Name = "lblDirFileEntity";
            this.lblDirFileEntity.Size = new System.Drawing.Size(93, 13);
            this.lblDirFileEntity.TabIndex = 0;
            this.lblDirFileEntity.Text = "Dir. arquivo Entity:";
            // 
            // txtDirFileEntityBO
            // 
            this.txtDirFileEntityBO.Location = new System.Drawing.Point(139, 54);
            this.txtDirFileEntityBO.Name = "txtDirFileEntityBO";
            this.txtDirFileEntityBO.Size = new System.Drawing.Size(477, 20);
            this.txtDirFileEntityBO.TabIndex = 4;
            // 
            // txtDirFileEntity
            // 
            this.txtDirFileEntity.Location = new System.Drawing.Point(139, 27);
            this.txtDirFileEntity.Name = "txtDirFileEntity";
            this.txtDirFileEntity.Size = new System.Drawing.Size(477, 20);
            this.txtDirFileEntity.TabIndex = 1;
            // 
            // btnDirFileEntityBOSelect
            // 
            this.btnDirFileEntityBOSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnDirFileEntityBOSelect.Image")));
            this.btnDirFileEntityBOSelect.Location = new System.Drawing.Point(623, 53);
            this.btnDirFileEntityBOSelect.Name = "btnDirFileEntityBOSelect";
            this.btnDirFileEntityBOSelect.Size = new System.Drawing.Size(29, 23);
            this.btnDirFileEntityBOSelect.TabIndex = 5;
            this.btnDirFileEntityBOSelect.UseVisualStyleBackColor = true;
            this.btnDirFileEntityBOSelect.Click += new System.EventHandler(this.BtnDirFileEntityBoSelectButtonClick);
            // 
            // btnDirFileEntitySelect
            // 
            this.btnDirFileEntitySelect.Image = ((System.Drawing.Image)(resources.GetObject("btnDirFileEntitySelect.Image")));
            this.btnDirFileEntitySelect.Location = new System.Drawing.Point(622, 26);
            this.btnDirFileEntitySelect.Name = "btnDirFileEntitySelect";
            this.btnDirFileEntitySelect.Size = new System.Drawing.Size(30, 23);
            this.btnDirFileEntitySelect.TabIndex = 2;
            this.btnDirFileEntitySelect.UseVisualStyleBackColor = true;
            this.btnDirFileEntitySelect.Click += new System.EventHandler(this.BtnDirFileEntitySelectButtonClick);
            // 
            // lblNamespaceEntityBO
            // 
            this.lblNamespaceEntityBO.AutoSize = true;
            this.lblNamespaceEntityBO.Location = new System.Drawing.Point(21, 136);
            this.lblNamespaceEntityBO.Name = "lblNamespaceEntityBO";
            this.lblNamespaceEntityBO.Size = new System.Drawing.Size(117, 13);
            this.lblNamespaceEntityBO.TabIndex = 11;
            this.lblNamespaceEntityBO.Text = "Namespace (EntityBO):";
            // 
            // lblNamespaceEntity
            // 
            this.lblNamespaceEntity.AutoSize = true;
            this.lblNamespaceEntity.Location = new System.Drawing.Point(36, 110);
            this.lblNamespaceEntity.Name = "lblNamespaceEntity";
            this.lblNamespaceEntity.Size = new System.Drawing.Size(102, 13);
            this.lblNamespaceEntity.TabIndex = 9;
            this.lblNamespaceEntity.Text = "Namespace (Entity):";
            // 
            // txtNamespaceEntityBO
            // 
            this.txtNamespaceEntityBO.Location = new System.Drawing.Point(139, 132);
            this.txtNamespaceEntityBO.Name = "txtNamespaceEntityBO";
            this.txtNamespaceEntityBO.Size = new System.Drawing.Size(477, 20);
            this.txtNamespaceEntityBO.TabIndex = 12;
            // 
            // txtNamespaceEntity
            // 
            this.txtNamespaceEntity.Location = new System.Drawing.Point(139, 106);
            this.txtNamespaceEntity.Name = "txtNamespaceEntity";
            this.txtNamespaceEntity.Size = new System.Drawing.Size(477, 20);
            this.txtNamespaceEntity.TabIndex = 10;
            // 
            // statusStripFooter
            // 
            this.statusStripFooter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStripFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripLabelStatus});
            this.statusStripFooter.Location = new System.Drawing.Point(0, 711);
            this.statusStripFooter.Name = "statusStripFooter";
            this.statusStripFooter.Size = new System.Drawing.Size(1173, 22);
            this.statusStripFooter.TabIndex = 1;
            this.statusStripFooter.Text = "statusStrip1";
            // 
            // stripLabelStatus
            // 
            this.stripLabelStatus.AutoToolTip = true;
            this.stripLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stripLabelStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.stripLabelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stripLabelStatus.ForeColor = System.Drawing.Color.Red;
            this.stripLabelStatus.Name = "stripLabelStatus";
            this.stripLabelStatus.Size = new System.Drawing.Size(1158, 17);
            this.stripLabelStatus.Spring = true;
            this.stripLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 733);
            this.Controls.Add(this.statusStripFooter);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1189, 726);
            this.Name = "StartForm";
            this.Text = "KPCodeGen 1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartFormFormClosing);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grbTables.ResumeLayout(false);
            this.grbTables.PerformLayout();
            this.tabConfiguration.ResumeLayout(false);
            this.tabComponentView.ResumeLayout(false);
            this.tabComponentForm.ResumeLayout(false);
            this.grbActions.ResumeLayout(false);
            this.tabCodeGenerate.ResumeLayout(false);
            this.tabTableDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).EndInit();
            this.tabEntityCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEntityCode)).EndInit();
            this.tabEntityBOCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEntityBOCode)).EndInit();
            this.tabFormViewASPX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewASPXCode)).EndInit();
            this.tabFormViewDesigner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewDESIGNERCode)).EndInit();
            this.tabFormViewCS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormViewCSCode)).EndInit();
            this.tabFormASPX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormASPXCode)).EndInit();
            this.tabFormDesigner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormDESIGNERCode)).EndInit();
            this.tabFormCS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCSCode)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.grbConnection.ResumeLayout(false);
            this.grbConnection.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.grbOthersConfig.ResumeLayout(false);
            this.grbOthersConfig.PerformLayout();
            this.grbConfigAssembly.ResumeLayout(false);
            this.grbConfigAssembly.PerformLayout();
            this.statusStripFooter.ResumeLayout(false);
            this.statusStripFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnStr;
        private System.Windows.Forms.Button btnConnectDatabase;
        private System.Windows.Forms.DataGridView gridViewDetails;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private TabControl tabMain;
        private TabPage tabBasic;
        private TabPage tabSettings;
        private GroupBox grbConnection;
        private GroupBox grbTables;
        private ComboBox cmbSchemaOwners;
        private ListBox lstTable;
        private TextBox txtTableFilter;
        private ComboBox cmbConnectionName;
        private Button btnConnectionConfig;
        private SplitContainer splitContainer;
        private TabControl tabCodeGenerate;
        private TabPage tabTableDetail;
        private TabPage tabEntityCode;
        private TabPage tabEntityBOCode;
        private FastColoredTextBoxNS.FastColoredTextBox txtEntityCode;
        private FastColoredTextBoxNS.FastColoredTextBox txtEntityBOCode;
        private StatusStrip statusStripFooter;
        private ToolStripStatusLabel stripLabelStatus;
        private TabPage tabFormViewASPX;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormViewASPXCode;
        private TabPage tabFormASPX;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormASPXCode;
        private GroupBox grbConfigAssembly;
        private Button btnDirFileFormsSelect;
        private Label lblDirFileForms;
        private TextBox txtDirFileForms;
        private Label lblDirFileEntityBO;
        private Label lblDirFileEntity;
        private TextBox txtDirFileEntityBO;
        private TextBox txtDirFileEntity;
        private Button btnDirFileEntityBOSelect;
        private Button btnDirFileEntitySelect;
        private Label lblNamespaceEntityBO;
        private Label lblNamespaceEntity;
        private TextBox txtNamespaceEntityBO;
        private TextBox txtNamespaceEntity;
        private GroupBox grbOthersConfig;
        private ComboBox cmbGlobalizingLanguage;
        private Label lblGlobalization;
        private DataGridViewTextBoxColumn col_ColumnName;
        private DataGridViewTextBoxColumn col_ColumnDataType;
        private DataGridViewComboBoxColumn col_cSharpType;
        private DataGridViewCheckBoxColumn col_IsPrimaryKey;
        private DataGridViewCheckBoxColumn col_IsForeignKey;
        private DataGridViewCheckBoxColumn col_IsNullable;
        private DataGridViewCheckBoxColumn col_IsIdentity;
        private DataGridViewCheckBoxColumn col_IsUniqueKey;
        private DataGridViewTextBoxColumn col_ConstraintName;
        private DataGridViewTextBoxColumn col_ForeignKeyTableName;
        private DataGridViewTextBoxColumn col_ForeignKeyColumnName;
        private DataGridViewTextBoxColumn col_DataLength;
        private DataGridViewTextBoxColumn col_MappedDataType;
        private DataGridViewTextBoxColumn col_DataSize;
        private DataGridViewTextBoxColumn col_DataPrecision;
        private DataGridViewTextBoxColumn col_DataScale;
        private DataGridViewTextBoxColumn col_DisplayName;
        private DataGridViewComboBoxColumn col_ColumnTypeView;
        private DataGridViewComboBoxColumn col_ColumnTypeForm;
        private Label lblNamespaceWebProject;
        private TextBox txtNamespaceWebProject;
        private Label lblEntityName;
        private TextBox txtEntityName;
        private TabPage tabFormViewDesigner;
        private TabPage tabFormViewCS;
        private TabPage tabFormDesigner;
        private TabPage tabFormCS;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormViewDESIGNERCode;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormViewCSCode;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormDESIGNERCode;
        private FastColoredTextBoxNS.FastColoredTextBox txtFormCSCode;
        private GroupBox grbActions;
        private Button btnGenerateCode;
        private Label lblSequenceTable;
        private ComboBox cmbSequencesOracle;
        private TabControl tabConfiguration;
        private TabPage tabComponentView;
        private PropertyGrid propertyView;
        private TabPage tabComponentForm;
        private PropertyGrid propertyForm;
    }
}

