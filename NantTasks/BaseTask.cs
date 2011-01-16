#region Copyright © 2004 Victor Boctor
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
using System.Globalization;
using System.IO;
using System.Text;

using NAnt.Core;
using NAnt.Core.Types;
using NAnt.Core.Attributes;

using Futureware.MantisConnect;

namespace NAnt.Contrib.Tasks.MantisConnect
{
	/// <summary>
	/// Summary description for BaseTask.
	/// </summary>
	public abstract class BaseTask : Task
	{
        #region Private Instance Fields

        private string url = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private string httpUsername = string.Empty;
        private string httpPassword = string.Empty;
        private Session session;

        #endregion Private Instance Fields

        #region Public Instance Properties
        
        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("url", Required = true)]
        public string Url 
        {
            get { return url; }
            set { url = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("username", Required = false)]
        public string Username 
        {
            get { return username; }
            set { username = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("password", Required = false)]
        public string Password 
        {
            get { return password; }
            set { password = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("httpUsername", Required = false)]
        public string HttpUsername
        {
            get { return httpUsername; }
            set { httpUsername = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        [TaskAttribute("httpPassword", Required = false)]
        public string HttpPassword
        {
            get { return httpPassword; }
            set { httpPassword = value;}
        }

        /// <summary>
        /// A Mantis session that can be used to communicate with Mantis installation.
        /// </summary>
        /// <remarks>
        /// This is valid after <see cref="Connect"/> is called.
        /// </remarks>
        protected Session Session
        {
            get { return session; }
        }

        /// <summary>
        /// Connects to Mantis installation and creates a session which can be accessed through the
        /// <see cref="Session"/> property.
        /// </summary>
        protected void Connect()
        {
            session = new Session( Url, Username, Password, null );
            session.Connect();
        }

        #endregion Public Instance Properties
    }
}
