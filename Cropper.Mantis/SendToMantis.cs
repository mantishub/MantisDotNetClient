//-----------------------------------------------------------------------
// <copyright file="SendToMantis.cs" company="Victor Boctor">
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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;

using Fusion8.Cropper.Extensibility;
using Futureware.MantisConnect;

namespace Cropper.Mantis
{
    public class SentToMantisFormat : IPersistableImageFormat, IConfigurablePlugin
    {
        private MantisConnectOptionsForm configurationForm; 
        private MantisConnectSettings settings;
        private IPersistableOutput _output;
        private const string DESCRIPTION = "Send to Mantis";
        private const string EXTENSION = "jpg";
        private const bool hostInOptions = true;

        public event ImageFormatClickEventHandler ImageFormatClick;

        public void Connect(IPersistableOutput persistableOutput)
        {
            if (persistableOutput == null)
            {
                throw new ArgumentNullException("persistableOutput");
            }

            this._output = persistableOutput;
            this._output.ImageCaptured += new ImageCapturedEventHandler(this.persistableOutput_ImageCaptured);
        }

        public void Disconnect()
        {
            this._output.ImageCaptured -= new ImageCapturedEventHandler(this.persistableOutput_ImageCaptured);
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            ImageFormatEventArgs args1 = new ImageFormatEventArgs();
            args1.ClickedMenuItem = (MenuItem)sender;
            args1.ImageOutputFormat = this;
            this.ImageFormatClick.Invoke(sender, args1);
        }

        private void persistableOutput_ImageCaptured(object sender, ImageCapturedEventArgs e)
        {
            this._output.FetchOutputStream(new StreamHandler(this.SaveImage), e.ImageNames.FullSize, e.FullSizeImage);

            using (ScreenshotDetailsForm form = new ScreenshotDetailsForm(this, this.ProcessImage, e.ImageNames.FullSize))
            {
                form.ShowDialog();
            }
        }

        public void ProcessImage(int issueId, string fullSizePath, string fileName, string note)
        {
            // get file name from the user as input to save on the server.
            try
            {
                Session session = new Session(this.PluginSettings.Url, this.PluginSettings.UserName, this.PluginSettings.Password, null);
                session.Request.IssueAttachmentAdd(issueId, fullSizePath, fileName);

                if (note.Trim().Length > 0)
                {
                    IssueNote issueNote = new IssueNote();
                    issueNote.Text = note;
                    session.Request.IssueNoteAdd(issueId, issueNote);
                }

                string message = "Snapshot successfully uploaded";
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SoapException ex)
            {
                MessageBox.Show(ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetIssueSummary(int issueId)
        {
            try
            {
                Session session = new Session(this.PluginSettings.Url, this.PluginSettings.UserName, this.PluginSettings.Password, null);
                Issue issue = session.Request.IssueGet(issueId);
                if (issue == null)
                {
                    return "<issue doesn't exist>";
                }

                return issue.Summary;
            }
            catch (Exception ex)
            {
                return "<" + ex.Message + ">";
            }
        }

        private void SaveImage(Stream stream, Image image)
        {
            image.Save(stream, ImageFormat.Png);
        }

        public string Description
        {
            get
            {
                return DESCRIPTION;
            }
        }

        public string Extension
        {
            get
            {
                return EXTENSION;
            }
        }

        public IPersistableImageFormat Format
        {
            get
            {
                return this;
            }
        }

        public MenuItem Menu
        {
            get
            {
                MenuItem item1 = new MenuItem();
                item1.RadioCheck = true;
                item1.Text = DESCRIPTION;
                item1.Click += new EventHandler(this.menuItem_Click);
                return item1;
            }
        }

        #region IConfigurablePlugin Members

        public bool HostInOptions
        {
            get
            {
                return hostInOptions;
            }
        }

        public object Settings
        {
            get { return this.PluginSettings; }
            set { this.PluginSettings = value as MantisConnectSettings; }
        }

        public MantisConnectSettings PluginSettings
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = new MantisConnectSettings();
                }

                return this.settings;
            }
            set { this.settings = value; }
        }

        public BaseConfigurationForm ConfigurationForm
        {
            get
            {
                if (this.configurationForm == null)
                {
                    this.configurationForm = new MantisConnectOptionsForm();
                    this.configurationForm.OptionsSaved += new EventHandler(configurationForm_OptionsSaved);
                    this.configurationForm.UserName = this.settings.UserName;
                    this.configurationForm.Password = this.settings.Password;
                    this.configurationForm.Url = this.settings.Url;
                }

                return this.configurationForm;
            }
        }

        void configurationForm_OptionsSaved(object sender, EventArgs e)
        {
            this.settings.UserName = this.configurationForm.UserName;
            this.settings.Password = this.configurationForm.Password;
            this.settings.Url = this.configurationForm.Url;
        }

        #endregion

        public override string ToString()
        {
            return "Send to Mantis [Victor Boctor]";
        }
    }
}
