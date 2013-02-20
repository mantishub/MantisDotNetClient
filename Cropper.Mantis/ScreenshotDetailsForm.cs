//-----------------------------------------------------------------------
// <copyright file="ScreenshotDetailsForm.cs" company="Victor Boctor">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cropper.Mantis
{
    public partial class ScreenshotDetailsForm : Form
    {
        private string filePath;
        private ProcessImageHandler processImageHandler;
        private SentToMantisFormat sendToMantis;
        private string issueIdBeingFetched;

        public delegate void ProcessImageHandler(int issueId, string filePath, string fileName, string note);

        /// <summary>
        /// The constructor for the screenshot details form.
        /// </summary>
        /// <param name="filePath">The full file path of the screenshot.</param>
        public ScreenshotDetailsForm(SentToMantisFormat sendToMantis, ProcessImageHandler processImageHandler, string filePath)
        {
            if (sendToMantis == null)
            {
                throw new ArgumentNullException("sendToMantis");
            }

            if (processImageHandler == null)
            {
                throw new ArgumentNullException("processImageHandler");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            this.processImageHandler = processImageHandler;
            this.filePath = filePath;
            this.sendToMantis = sendToMantis;

            InitializeComponent();
        }

        /// <summary>
        /// The file name dot extension that will appear on the View Issue page next to the screen capture.
        /// </summary>
        private string FileName
        {
            get
            {
                return this.fileNameTextBox.Text + ".jpg";
            }
        }

        /// <summary>
        /// The id of the issue that the screen capture will be attached to.
        /// </summary>
        private int IssueId
        {
            get
            {
                return int.Parse(this.issueIdTextBox.Text.Trim());
            }
        }

        /// <summary>
        /// The note to add along with attaching the screen capture.  If empty, then no note will be added.
        /// </summary>
        private string Note
        {
            get
            {
                return this.noteTextBox.Text.Trim();
            }
        }

        /// <summary>
        /// Triggered when the issue id text box is modified.  It enables/disables controls as appropriate.
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void issueIdTextBox_TextChanged(object sender, EventArgs e)
        {
            this.EnableControls();

            if (String.IsNullOrEmpty(this.issueIdBeingFetched))
            {
                this.issueIdBeingFetched = this.issueIdTextBox.Text;
                this.getIssueBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Triggered when the file name is modified.  It enables/disables controls as appropriate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.EnableControls();
        }

        /// <summary>
        /// A helper method that is called from events to enable/disable controls based on the data
        /// entered by the user.
        /// </summary>
        private void EnableControls()
        {
            int issueId;
            this.uploadToMantisButton.Enabled = this.fileNameTextBox.Text.Trim().Length > 0 &&
                this.issueIdTextBox.Text.Trim().Length > 0 &&
                Int32.TryParse(this.issueIdTextBox.Text, out issueId);
        }

        private void uploadToMantisButton_Click(object sender, EventArgs e)
        {
            this.processImageHandler(this.IssueId, this.filePath, this.FileName, this.Note);
        }

        private void getIssueBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.issueSummaryTextBox.Text = "Retrieving issue summary...";

            int issueId;
            if (Int32.TryParse(this.issueIdBeingFetched, out issueId))
            {
                e.Result = this.sendToMantis.GetIssueSummary(issueId);
            }
            else
            {
                e.Result = "<invalid issue id>";
            }
        }

        private void getIssueBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.issueSummaryTextBox.Text = (string)e.Result;

            string temp = this.issueIdBeingFetched;
            this.issueIdBeingFetched = null;

            if (!String.Equals(this.issueIdTextBox.Text, temp, StringComparison.OrdinalIgnoreCase))
            {
                this.issueIdTextBox_TextChanged(this, null);
            }
        }
    }
}