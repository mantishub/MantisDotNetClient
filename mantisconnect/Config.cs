//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Victor Boctor">
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

namespace Futureware.MantisConnect
{
    using System;
    using System.Collections.Generic;

    /// <summary>
	/// Provides access to Mantis configuration.
	/// </summary>
	/// <remarks>
	/// This is useful to provide access to Mantis configuration like enumerations and
	/// any other configs that are not considered private.  Using this class the client
	/// will not be able to retrieve sensitive information from the webservice, like
	/// passwords, database names, host names, ...etc.
	/// 
	/// This class also provides the necessary caching to avoid unnecessary round trips
	/// to the server.  Caching is feasible in this class due to the static nature of
	/// these configs.
	/// </remarks>
    public sealed class Config
    {
        /// <summary>
        /// The session to use in getting configs.
        /// </summary>
        private readonly Session session;

        /// <summary>
        /// Hashtable of cached configuration options.
        /// </summary>
        private IDictionary<string, string> configs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">Session to be used to retrieve the configs.</param>
        public Config(Session session)
        {
            this.session = session;
        }

        /// <summary>
        /// Returns a reference to the <see cref="Session"/> instance used to access
        /// MantisConnect webservice.
        /// </summary>
        public Session Session
        {
            get { return this.session; }
        }

        /// <summary>
        /// Access Level Enumeration
        /// </summary>
        public MantisEnum AccessLevelEnum
        {
            get { return this.Get("access_levels"); }
        }

        /// <summary>
        /// Priority Enumeration
        /// </summary>
        public MantisEnum PriorityEnum
        {
            get { return this.Get("priority"); }
        }

        /// <summary>
        /// Severity Enumeration
        /// </summary>
        public MantisEnum SeverityEnum
        {
            get { return this.Get("severity"); }
        }

        /// <summary>
        /// Reproducibility Enumeration
        /// </summary>
        public MantisEnum ReproducibilityEnum
        {
            get { return this.Get("reproducibility"); }
        }

        /// <summary>
        /// View State (eg: private/public) Enumeration
        /// </summary>
        public MantisEnum ViewStateEnum
        {
            get { return this.Get("view_state"); }
        }

        /// <summary>
        /// Estimated Time of Arrival Enumeration
        /// </summary>
        public MantisEnum EtaEnum
        {
            get { return this.Get("eta"); }
        }

        /// <summary>
        /// Projection Enumeration
        /// </summary>
        public MantisEnum ProjectionEnum
        {
            get { return this.Get("projection"); }
        }

        /// <summary>
        /// Resolution Enumeration
        /// </summary>
        public MantisEnum ResolutionEnum
        {
            get { return this.Get("resolution"); }
        }

        /// <summary>
        /// Status Enumeration
        /// </summary>
        public MantisEnum StatusEnum
        {
            get { return this.Get("status"); }
        }

        /// <summary>
        /// Gets the value associated with a configuration variable of type string.
        /// </summary>
        /// <param name="config">The name of the configuration variable to get.</param>
        /// <param name="str">The output parameter to store the config variable in.  This
        /// is a parameter rather than a return value to allow overloading of this
        /// method for different types.</param>
        public void Get(string config, out string str)
        {
            if (!this.configs.TryGetValue(config, out str))
            {
                session.Request.ConfigGet(config, out str);
                this.configs[config] = str;
            }
        }

        /// <summary>
        /// Get the enumeration with the specified name.
        /// </summary>
        /// <remarks>
        /// See also the properties for the enumerations.
        /// </remarks>
        /// <param name="enumerationName">Enumeration name (eg: status, access_levels, ...etc)</param>
        /// <param name="enumeration">An output parameter to contain the created <see cref="MantisEnum"/> instance.</param>
        public void Get(string enumerationName, out MantisEnum enumeration)
        {
            string str;

            string configOption = string.Format("{0}_enum_string", enumerationName);
            this.Get(configOption, out str);

            enumeration = new MantisEnum(str);
        }

        /// <summary>
        /// Get the enumeration with the specified name.
        /// </summary>
        /// <remarks>
        /// See also the properties for the enumerations.
        /// </remarks>
        /// <param name="enumerationName">Enumeration name (eg: status, access_levels, ...etc)</param>
        /// <returns>MantisEnum</returns>
        public MantisEnum Get(string enumerationName)
        {
            MantisEnum enumeration;
            this.Get(enumerationName, out enumeration);
            return enumeration;
        }
    }
}
