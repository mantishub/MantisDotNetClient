//-----------------------------------------------------------------------
// <copyright file="Session.cs" company="Victor Boctor">
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
using System.Net;

namespace Futureware.MantisConnect
{
    /// <summary>
    /// Represents a connection session between the webservice client and server.
    /// </summary>
    public sealed class Session
    {
        /// <summary>
        /// Constructs a session given a url, username and password.
        /// </summary>
        /// <param name="url">URL of MantisConnect webservice (eg: http://www.example.com/mantis/)</param>
        /// <param name="username">User name to connect as.</param>
        /// <param name="password">Password for the specified user.</param>
        /// <param name="networkCredential"></param>
        public Session(string url, string username, string password, NetworkCredential networkCredential)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            if (url.IndexOf("mantisconnect.php", StringComparison.OrdinalIgnoreCase) == -1)
            {
                if (!url.EndsWith("/"))
                {
                    url += "/";
                }

                url += "api/soap/mantisconnect.php";
            }

            this.username = username;
            this.password = password;
            this.url = url;
            this.networkCredential = networkCredential;

            config = new Config( this );
            request = new Request( this );
        }

        /// <summary>
        /// Connects to the webservice.
        /// </summary>
        public void Connect()
        {
            // call any method to trigger authentication, if url, username, password
            // are valid, then nothing will happen, otherwise an exception will be raised.
            MantisEnum temp = Config.StatusEnum;
        }

        /// <summary>
        /// User name specified on construction of the session.
        /// </summary>
        public string Username
        {
            get { return username; }
        }

        /// <summary>
        /// User password specified on construction of the session.
        /// </summary>
        public string Password
        {
            get { return password; }
        }

        /// <summary>
        /// MantisConnect webservice URL
        /// </summary>
        /// <remarks>
        /// eg: http://www.example.com/mantis/mantisconnect/mantisconnect.php
        /// </remarks>
        public string Url
        {
            get { return url; }
        }

        /// <summary>
        /// Gets the network credential specified during construction time.
        /// </summary>
        /// <remarks>
        /// This is use to support connecting to Mantis installation that are
        /// protected with basic http authentication.
        /// </remarks>
        public NetworkCredential NetworkCredential
        {
            get { return networkCredential; }
        }

        /// <summary>
        /// Config object to be used to retrieve information about the configuration 
        /// of the Mantis installation.
        /// </summary>
        public Config Config
        {
            get
            {
                return config;
            }
        }

        /// <summary>
        /// Request is a wrapper around the MantisConnect webservice which makes calling
        /// the methods more natural for the rest of the C# code.
        /// </summary>
        public Request Request
        {
            get
            {
                return request;
            }
        }

        #region Private
        private readonly Config config;
        private readonly Request request;
        private readonly string url;
        private readonly string username;
        private readonly string password;
        private readonly NetworkCredential networkCredential;
        #endregion
    }
}
