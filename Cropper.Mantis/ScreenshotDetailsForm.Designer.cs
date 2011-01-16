namespace Cropper.Mantis
{
    partial class ScreenshotDetailsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.issueIdTextBox = new System.Windows.Forms.TextBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.uploadToMantisButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.issueSummaryTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.getIssueBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Issue #";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Name";
            // 
            // issueIdTextBox
            // 
            this.issueIdTextBox.Location = new System.Drawing.Point(96, 18);
            this.issueIdTextBox.Name = "issueIdTextBox";
            this.issueIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.issueIdTextBox.TabIndex = 1;
            this.issueIdTextBox.TextChanged += new System.EventHandler(this.issueIdTextBox_TextChanged);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(96, 44);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(141, 20);
            this.fileNameTextBox.TabIndex = 3;
            this.fileNameTextBox.TextChanged += new System.EventHandler(this.fileNameTextBox_TextChanged);
            // 
            // uploadToMantisButton
            // 
            this.uploadToMantisButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uploadToMantisButton.Enabled = false;
            this.uploadToMantisButton.Location = new System.Drawing.Point(139, 318);
            this.uploadToMantisButton.Name = "uploadToMantisButton";
            this.uploadToMantisButton.Size = new System.Drawing.Size(121, 23);
            this.uploadToMantisButton.TabIndex = 9;
            this.uploadToMantisButton.Text = "Upload to Mantis";
            this.uploadToMantisButton.UseVisualStyleBackColor = true;
            this.uploadToMantisButton.Click += new System.EventHandler(this.uploadToMantisButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(288, 318);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(110, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = ".jpg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Note (Optional)";
            // 
            // noteTextBox
            // 
            this.noteTextBox.AcceptsReturn = true;
            this.noteTextBox.Location = new System.Drawing.Point(96, 96);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(454, 203);
            this.noteTextBox.TabIndex = 8;
            // 
            // issueSummaryTextBox
            // 
            this.issueSummaryTextBox.Location = new System.Drawing.Point(96, 70);
            this.issueSummaryTextBox.Name = "issueSummaryTextBox";
            this.issueSummaryTextBox.ReadOnly = true;
            this.issueSummaryTextBox.Size = new System.Drawing.Size(454, 20);
            this.issueSummaryTextBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Issue Summary";
            // 
            // getIssueBackgroundWorker
            // 
            this.getIssueBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getIssueBackgroundWorker_DoWork);
            this.getIssueBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getIssueBackgroundWorker_RunWorkerCompleted);
            // 
            // ScreenshotDetailsForm
            // 
            this.AcceptButton = this.uploadToMantisButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(562, 353);
            this.ControlBox = false;
            this.Controls.Add(this.issueSummaryTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.uploadToMantisButton);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.issueIdTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreenshotDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screenshot Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox issueIdTextBox;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button uploadToMantisButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.TextBox issueSummaryTextBox;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker getIssueBackgroundWorker;
    }
}