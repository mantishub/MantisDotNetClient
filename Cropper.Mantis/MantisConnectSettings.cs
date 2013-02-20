//-----------------------------------------------------------------------
// <copyright file="MantisConnectSettings.cs" company="Victor Boctor">
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
using System.Text;

namespace Cropper.Mantis
{
    public class MantisConnectSettings
    {
        private string url = "http://localhost:8008/mantisbt/mc/mantisconnect.php";
        private string userName = "administrator";
        private string password = "root";

        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nUrl:" + this.Url);
            sb.Append("\nUserName:" + this.UserName);
            sb.Append("\nPassword:" + this.Password);
            return sb.ToString();
        }
    }
}
