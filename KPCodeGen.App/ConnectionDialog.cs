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
using KPCodeGen.Core.Util;
using KPCodeGen.Components;
using Microsoft.Data.ConnectionUI;
using System;
using System.Windows.Forms;
using KPCodeGen.Enumerator;

namespace KPCodeGen
{
    /// <summary>
    /// <para>Authors: Juliano Tiago Rinaldi and 
    /// Tiago Antonio Jacobi</para>
    /// </summary>
    public partial class ConnectionDialog : Form
    {
        private Connection _connection;

        public Connection Connection
        {
            get { return _connection; }
            set { _connection = value; BindData(); }
        }

        public ConnectionDialog()
        {
            InitializeComponent();

            Load += OnConnectionDialogLoad;

            PopulateServerTypes();

            serverTypeComboBox.SelectedIndexChanged += OnServerTypeSelectedIndexChanged;
        }

        private void OnConnectionDialogLoad(object sender, EventArgs e)
        {
            // If no connection has been passed in create a new one
            if (Connection == null)
            {
                Connection = CreateNewConnection();
            }
        }

        private void OnDeleteButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void OnAddButtonClick(object sender, EventArgs e)
        {
            Connection = CreateNewConnection();
        }

        private void OnServerTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (serverTypeComboBox.SelectedIndex == -1)
            {
                // Nothing selected
                return;
            }

            // Set a default connection string if user changes server type.
            var serverType = (DatabaseServerType)serverTypeComboBox.SelectedItem;
            Connection.Name = nameTextBox.Text;
            Connection.Type = serverType;
            Connection.ConnectionString = GetDefaultConnectionStringForServerType(serverType);
            BindData();
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            CaptureConnection();
        }

        private Connection CreateNewConnection(DatabaseServerType serverType = DatabaseServerType.SqlServer)
        {
            // Default connection settings.
            var connectionString = GetDefaultConnectionStringForServerType(serverType);

            return new Connection
                       {
                           Id = Guid.NewGuid(),
                           Name = "New Connection",
                           ConnectionString = connectionString,
                           Type = serverType
                       };
        }

        private string GetDefaultConnectionStringForServerType(DatabaseServerType serverType)
        {
            switch (serverType)
            {
                case DatabaseServerType.Oracle:
                    return StringConstants.ORACLE_CONN_STR_TEMPLATE;
                case DatabaseServerType.SqlServer:
                    return StringConstants.SQL_CONN_STR_TEMPLATE;
                case DatabaseServerType.MySQL:
                    return StringConstants.MYSQL_CONN_STR_TEMPLATE;
                case DatabaseServerType.SQLite:
                    return StringConstants.SQLITE_CONN_STR_TEMPLATE;
                case DatabaseServerType.Sybase:
                    return StringConstants.SYBASE_CONN_STR_TEMPLATE;
                case DatabaseServerType.Ingres:
                    return StringConstants.INGRES_CONN_STR_TEMPLATE;
                case DatabaseServerType.CUBRID:
                    return StringConstants.CUBRID_CONN_STR_TEMPLATE;
                default:
                    return StringConstants.POSTGRESQL_CONN_STR_TEMPLATE;
            }
        }

        private void BindData()
        {
            serverTypeComboBox.SelectedIndexChanged -= OnServerTypeSelectedIndexChanged;

            nameTextBox.Text = Connection.Name;
            serverTypeComboBox.SelectedItem = Connection.Type;
            connectionStringTextBox.Text = Connection.ConnectionString;

            serverTypeComboBox.SelectedIndexChanged += OnServerTypeSelectedIndexChanged;
        }

        private void CaptureConnection()
        {
            Connection.Name = nameTextBox.Text;
            Connection.Type = (DatabaseServerType)serverTypeComboBox.SelectedItem;
            Connection.ConnectionString = connectionStringTextBox.Text;
        }

        private void PopulateServerTypes()
        {
            serverTypeComboBox.DataSource = Enum.GetValues(typeof(DatabaseServerType));
            serverTypeComboBox.SelectedIndex = 0;
        }

        private void OnConnectionStringButtonClick(object sender, EventArgs e)
        {
            var dialogResult = DialogResult.Cancel;
            var connectionString = string.Empty;
            var dcd = new DataConnectionDialog();

            try
            {
                var dcs = new DataConnectionConfiguration(null);
                dcs.LoadConfiguration(dcd, Connection.Type);

                CaptureConnection();
                if (Connection.ConnectionString != GetDefaultConnectionStringForServerType(Connection.Type))
                {
                    dcd.ConnectionString = Connection.ConnectionString;
                }

                dialogResult = DataConnectionDialog.Show(dcd);
                connectionString = dcd.ConnectionString;

            }
            catch (ArgumentException)
            {
                dcd.ConnectionString = string.Empty;
                dialogResult = DataConnectionDialog.Show(dcd);
            }
            finally
            {
                if (dialogResult == DialogResult.OK)
                {
                    Connection.ConnectionString = connectionString;
                    BindData();
                }
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
