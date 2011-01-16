#region Copyright © 2004-2007 Victor Boctor
//
// MantisConnect is copyrighted to Victor Boctor
//
// This program is distributed under the terms and conditions of the GPL
// See LICENSE file for details.
//
// For commercial applications to link with or modify MantisConnect, they require the
// purchase of a MantisConnect commerical license.
//
#endregion

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Net;
using System.Windows.Forms;

using Futureware.MantisConnect;

namespace Futureware.MantisFilters
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class MantisFiltersForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ComboBox filtersComboBox;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button getIssuesButton;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MantisFiltersForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filtersComboBox = new System.Windows.Forms.ComboBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.getIssuesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // filtersComboBox
            // 
            this.filtersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtersComboBox.Location = new System.Drawing.Point(16, 16);
            this.filtersComboBox.Name = "filtersComboBox";
            this.filtersComboBox.Size = new System.Drawing.Size(232, 21);
            this.filtersComboBox.TabIndex = 0;
            this.filtersComboBox.SelectedIndexChanged += new System.EventHandler(this.filtersComboBox_SelectedIndexChanged);
            // 
            // dataGrid1
            // 
            this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(16, 48);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(608, 208);
            this.dataGrid1.TabIndex = 1;
            // 
            // getIssuesButton
            // 
            this.getIssuesButton.Location = new System.Drawing.Point(264, 16);
            this.getIssuesButton.Name = "getIssuesButton";
            this.getIssuesButton.TabIndex = 2;
            this.getIssuesButton.Text = "Get Issues";
            this.getIssuesButton.Click += new System.EventHandler(this.getIssuesButton_Click);
            // 
            // MantisFiltersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(640, 266);
            this.Controls.Add(this.getIssuesButton);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.filtersComboBox);
            this.Name = "MantisFiltersForm";
            this.Text = "MantisConnect Filters";
            this.Load += new System.EventHandler(this.MantisFiltersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            try
            {
                Application.Run(new MantisFiltersForm());
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Webservice Error", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }

        private void MantisFiltersForm_Load(object sender, System.EventArgs e)
        {
            NetworkCredential nc = null;

            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string basicHttpAuthUserName = appSettings["BasicHttpAuthUserName"];
            string basicHttpAuthPassword = appSettings["BasicHttpAuthPassword"];
            if (!String.IsNullOrEmpty(basicHttpAuthUserName) && basicHttpAuthPassword != null)
            {
                nc = new NetworkCredential(basicHttpAuthUserName, basicHttpAuthPassword);
            }

            string mantisConnectUrl = appSettings["MantisConnectUrl"];
            string mantisUserName = appSettings["MantisUserName"];
            string mantisPassword = appSettings["MantisPassword"];

            session = new Session(mantisConnectUrl, mantisUserName, mantisPassword, nc);
            session.Connect();

            populating = true;
            filtersComboBox.DataSource = session.Request.UserGetFilters( 0 );
            filtersComboBox.DisplayMember = "Name";
            filtersComboBox.ValueMember = "Id";
            populating = false;

            UpdateGrid();
        }

        private void filtersComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if ( populating )
                return;

            UpdateGrid();
        }

        private void getIssuesButton_Click(object sender, System.EventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
			if ( filtersComboBox.SelectedValue == null )
			{
				dataGrid1.DataSource = null;
			}
			else
			{
				DataTable table = Request.ArrayToDataTable( typeof( Issue ), session.Request.GetIssues( 0, Convert.ToInt32( filtersComboBox.SelectedValue ), 1, 10 ), "Issues" );
				dataGrid1.DataSource = table;
			}

			dataGrid1.Refresh();
		}

        private Session session;
        private bool populating;
    }
}
