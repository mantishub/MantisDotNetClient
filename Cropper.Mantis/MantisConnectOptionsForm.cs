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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Fusion8.Cropper.Extensibility;

namespace Cropper.Mantis
{
    public partial class MantisConnectOptionsForm : BaseConfigurationForm
    {
        public MantisConnectOptionsForm()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return this.userNameTextBox.Text; }
            set { this.userNameTextBox.Text = value; }
        }

        public string Password
        {
            get { return this.passwordTextBox.Text; }
            set { this.passwordTextBox.Text = value; }
        }

        public string Url
        {
            get { return this.serverTextBox.Text; }
            set { this.serverTextBox.Text = value; }
        }
    }
}