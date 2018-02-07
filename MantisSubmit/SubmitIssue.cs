//-----------------------------------------------------------------------
// <copyright file="SubmitIssue.cs" company="Victor Boctor">
//     Copyright (C) All Rights Reserved
// </copyright>
// <summary>
// MantisConnect is copyrighted to Victor Boctor
//
// This program is distributed under the terms and conditions of the GPL
// See LICENSE file for details.
//
// For commercial applications to link with or modify MantisConnect, they require the
// purchase of a MantisConnect commercial license.
// </summary>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;

using Futureware.MantisConnect;

namespace Futureware.MantisSubmit
{
	/// <summary>
	/// A Windows form that allows submitting of issues in a Mantis installation.
	/// </summary>
	public class SubmitIssueForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox priorityComboBox;
        private System.Windows.Forms.ComboBox severityComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox reproducibilityComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox summaryTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.StatusBarPanel statusBarPanel;
        private Label lblCustomField1;
        private TextBox firstCustomFieldTextBox;
        private TextBox secondCustomFieldTextBox;
        private Label lblCustomField2;
        private OpenFileDialog openFileDialog1;
        private TextBox attachmentTextBox;
        private Label label10;
        private Button browseButton;
        private TreeView treeView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SubmitIssueForm()
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
				if(components != null)
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
            this.summaryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.priorityComboBox = new System.Windows.Forms.ComboBox();
            this.severityComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.reproducibilityComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCustomField1 = new System.Windows.Forms.Label();
            this.firstCustomFieldTextBox = new System.Windows.Forms.TextBox();
            this.secondCustomFieldTextBox = new System.Windows.Forms.TextBox();
            this.lblCustomField2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.attachmentTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // summaryTextBox
            // 
            this.summaryTextBox.Location = new System.Drawing.Point(160, 272);
            this.summaryTextBox.Name = "summaryTextBox";
            this.summaryTextBox.Size = new System.Drawing.Size(472, 20);
            this.summaryTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Summary";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // submitButton
            // 
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.Location = new System.Drawing.Point(300, 614);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 9;
            this.submitButton.Text = "Submit";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(328, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Priority";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // priorityComboBox
            // 
            this.priorityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priorityComboBox.Location = new System.Drawing.Point(456, 163);
            this.priorityComboBox.Name = "priorityComboBox";
            this.priorityComboBox.Size = new System.Drawing.Size(176, 21);
            this.priorityComboBox.TabIndex = 4;
            // 
            // severityComboBox
            // 
            this.severityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.severityComboBox.Location = new System.Drawing.Point(456, 195);
            this.severityComboBox.Name = "severityComboBox";
            this.severityComboBox.Size = new System.Drawing.Size(176, 21);
            this.severityComboBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(328, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Severity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reproducibilityComboBox
            // 
            this.reproducibilityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reproducibilityComboBox.Location = new System.Drawing.Point(456, 227);
            this.reproducibilityComboBox.Name = "reproducibilityComboBox";
            this.reproducibilityComboBox.Size = new System.Drawing.Size(176, 21);
            this.reproducibilityComboBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(328, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Reproducibility";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Description";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(160, 304);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(472, 176);
            this.descriptionTextBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Project";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(32, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(616, 48);
            this.label7.TabIndex = 13;
            this.label7.Text = "MantisConnect Submit";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 654);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(664, 22);
            this.statusBar.TabIndex = 14;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Width = 647;
            // 
            // versionComboBox
            // 
            this.versionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionComboBox.Location = new System.Drawing.Point(456, 99);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(176, 21);
            this.versionComboBox.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(328, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 21);
            this.label8.TabIndex = 16;
            this.label8.Text = "Version";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Location = new System.Drawing.Point(456, 131);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(176, 21);
            this.categoryComboBox.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(328, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "Category";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCustomField1
            // 
            this.lblCustomField1.Location = new System.Drawing.Point(35, 494);
            this.lblCustomField1.Name = "lblCustomField1";
            this.lblCustomField1.Size = new System.Drawing.Size(100, 23);
            this.lblCustomField1.TabIndex = 19;
            this.lblCustomField1.Text = "Custom Field 1";
            this.lblCustomField1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // firstCustomFieldTextBox
            // 
            this.firstCustomFieldTextBox.Enabled = false;
            this.firstCustomFieldTextBox.Location = new System.Drawing.Point(160, 494);
            this.firstCustomFieldTextBox.Name = "firstCustomFieldTextBox";
            this.firstCustomFieldTextBox.Size = new System.Drawing.Size(472, 20);
            this.firstCustomFieldTextBox.TabIndex = 20;
            // 
            // secondCustomFieldTextBox
            // 
            this.secondCustomFieldTextBox.Enabled = false;
            this.secondCustomFieldTextBox.Location = new System.Drawing.Point(160, 526);
            this.secondCustomFieldTextBox.Name = "secondCustomFieldTextBox";
            this.secondCustomFieldTextBox.Size = new System.Drawing.Size(472, 20);
            this.secondCustomFieldTextBox.TabIndex = 22;
            // 
            // lblCustomField2
            // 
            this.lblCustomField2.Location = new System.Drawing.Point(35, 526);
            this.lblCustomField2.Name = "lblCustomField2";
            this.lblCustomField2.Size = new System.Drawing.Size(100, 23);
            this.lblCustomField2.TabIndex = 21;
            this.lblCustomField2.Text = "Custom Field 2";
            this.lblCustomField2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // attachmentTextBox
            // 
            this.attachmentTextBox.Location = new System.Drawing.Point(160, 558);
            this.attachmentTextBox.Name = "attachmentTextBox";
            this.attachmentTextBox.Size = new System.Drawing.Size(436, 20);
            this.attachmentTextBox.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(32, 558);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 23;
            this.label10.Text = "Attachment";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(603, 554);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(29, 23);
            this.browseButton.TabIndex = 25;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(35, 104);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(155, 144);
            this.treeView1.TabIndex = 26;
            // 
            // SubmitIssueForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 676);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.attachmentTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.secondCustomFieldTextBox);
            this.Controls.Add(this.lblCustomField2);
            this.Controls.Add(this.firstCustomFieldTextBox);
            this.Controls.Add(this.lblCustomField1);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.versionComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.summaryTextBox);
            this.Controls.Add(this.reproducibilityComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.severityComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.priorityComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.label1);
            this.Name = "SubmitIssueForm";
            this.Text = "Mantis Connect - Submit Issue";
            this.Load += new System.EventHandler(this.SubmitIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        private void SubmitIssue_Load(object sender, System.EventArgs e)
        {
            try
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
                int i = 0;
                foreach (Project project in session.Request.UserGetAccessibleProjects())
                {
                    TreeNode Node = new TreeNode(project.Name);
                    Node.Tag = project.Id;
                    treeView1.Nodes.Add(Node);
                    if (project.Subprojects.Count > 0)
                    {
                        TreeNode customerNode = new TreeNode(project.Name);
                        customerNode.Tag = project.Id;
                        walkNode(project.Subprojects, ref customerNode);
                        treeView1.Nodes[i].Nodes.Add(customerNode);
                     }
                    if (i == 1)
                        treeView1.SelectedNode=Node;
                    i++;
                }

                this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
                populating = false;

                PopulateProjectDependentFields();

                priorityComboBox.DataSource = session.Config.PriorityEnum.GetLabels();
                severityComboBox.DataSource = session.Config.SeverityEnum.GetLabels();
                reproducibilityComboBox.DataSource = session.Config.ReproducibilityEnum.GetLabels();
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Webservice Error", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }

        public void walkNode(List<Project> projects, ref TreeNode Tn)
        {

          for (int i = 0; i < projects.Count; i++)
            {
                TreeNode Node = new TreeNode(projects[i].Name);
                Node.Tag = projects[i].Id;
                Tn.Nodes.Add (Node);
                if (projects[i].Subprojects.Count > 0)
                    walkNode(projects[i].Subprojects, ref Tn);

            }

                


        }

        /// <summary>
        /// Event handler for clicking the submit button.
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void submitButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                string attachment = this.attachmentTextBox.Text;

                if (attachment.Length > 0 && !File.Exists(attachment))
                {
                    MessageBox.Show(String.Format("File '{0}' doesn't exist", attachment));
                    return;
                }

                statusBar.Panels[0].Text = "Checking if issue already reported...";

                // Check if issue was previously logged in Mantis.
                int issueId = session.Request.IssueGetIdFromSummary( summaryTextBox.Text );
                if ( issueId > 0 )
                {
                    statusBar.Panels[0].Text = string.Format( "'{0}' already reported in issue {1}", summaryTextBox.Text, issueId );
                    return;
                }

                // Create the issue in memory
                Issue issue = new Issue();

                issue.Project = new ObjectRef(treeView1.SelectedNode.Index);
                issue.Priority = new ObjectRef( priorityComboBox.Text );
                issue.Severity = new ObjectRef( severityComboBox.Text );
                issue.Reproducibility = new ObjectRef( reproducibilityComboBox.Text );
                issue.Category = new ObjectRef( categoryComboBox.Text );
                issue.ProductVersion = versionComboBox.Text;
                issue.Summary = summaryTextBox.Text;
                issue.Description = descriptionTextBox.Text;
                issue.ReportedBy = new User();
				issue.ReportedBy.Name = session.Username;

                statusBar.Panels[0].Text = "Submitting issue...";

                int newIssueId = session.Request.IssueAdd( issue );

                statusBar.Panels[0].Text = String.Format("Submitting attachment to issue {0}...", newIssueId);

                if (attachment.Length > 0)
                {
                    session.Request.IssueAttachmentAdd(newIssueId, attachment, null);
                }

                // Submit the issue and show its id in the status bar
                statusBar.Panels[0].Text = string.Format("Issued added as {0}.", newIssueId);

                ResetForm();
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString(), "Webservice Error", MessageBoxButtons.OK, MessageBoxIcon.Stop );
                statusBar.Text = string.Empty;
            }
        }

        /// <summary>
        /// Prepare the form for the next issue.
        /// </summary>
        private void ResetForm()
        {
            summaryTextBox.Clear();
            summaryTextBox.Focus();
            descriptionTextBox.Clear();
        }

        // Handle the After_Select event.
        private void TreeView1_AfterSelect(Object sender, TreeViewEventArgs e)
        {
            TreeView TV = (TreeView)sender;
            treeView1.SelectedNode = TV.SelectedNode;
            PopulateProjectDependentFields();
        }


        /// <summary>
        /// Populates the list of categories and versions based on the currently
        /// selected projects.
        /// </summary>
        private void PopulateProjectDependentFields()
        {
            try
            {
                int projectId = (int)treeView1.SelectedNode.Tag;

                this.lblCustomField1.Text = "Custom Field 1";
                this.lblCustomField2.Text = "Custom Field 2";

				if ( projectId == 0 )
				{
					categoryComboBox.DataSource = null;
					versionComboBox.DataSource = null;
				}
				else
				{
					categoryComboBox.DataSource = session.Request.ProjectGetCategories( projectId );
					categoryComboBox.DisplayMember = "Name";
					versionComboBox.DataSource = session.Request.ProjectGetVersions( projectId );
                    versionComboBox.DisplayMember = "Name";
				}
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Webservice Error", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }

        /// <summary>
        /// Tracks whether the projects combobox is currently being populated or not.
        /// </summary>
        /// <remarks>
        /// If being populated, then selection change event for the current project is
        /// ignored.
        /// </remarks>
        private bool populating = false;

        /// <summary>
        /// Session used to communicate with MantisConnect.
        /// </summary>
        private Session session;

        private void browseButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = this.attachmentTextBox.Text;
            this.openFileDialog1.CheckFileExists = true;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.attachmentTextBox.Text = this.openFileDialog1.FileName;
            }
        }
    }
}
