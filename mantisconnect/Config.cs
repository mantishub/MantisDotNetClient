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
using System.Collections;

namespace Futureware.MantisConnect
{
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
        /// Constructor
        /// </summary>
        /// <param name="session">Session to be used to retrieve the configs.</param>
        public Config( Session session )
        {
            this.session = session;
        }

        /// <summary>
        /// Returns a reference to the <see cref="Session"/> instance used to access
        /// MantisConnect webservice.
        /// </summary>
        public Session Session
        {
            get { return session; }
        }

        /// <summary>
        /// Access Level Enumeration
        /// </summary>
        public MantisEnum AccessLevelEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "access_levels", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Priority Enumeration
        /// </summary>
        public MantisEnum PriorityEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "priority", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Severity Enumeration
        /// </summary>
        public MantisEnum SeverityEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "severity", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Reproducibility Enumeration
        /// </summary>
        public MantisEnum ReproducibilityEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "reproducibility", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// View State (eg: private/public) Enumeration
        /// </summary>
        public MantisEnum ViewStateEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "view_state", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Estimated Time of Arrival Enumeration
        /// </summary>
        public MantisEnum EtaEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "eta", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Projection Enumeration
        /// </summary>
        public MantisEnum ProjectionEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "projection", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Resolution Enumeration
        /// </summary>
        public MantisEnum ResolutionEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "resolution", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Status Enumeration
        /// </summary>
        public MantisEnum StatusEnum
        {
            get
            {
                MantisEnum enumeration;
                Get( "status", out enumeration );
                return enumeration;
            }
        }

        /// <summary>
        /// Gets the value associated with a configuration variable of type string.
        /// </summary>
        /// <param name="config">The name of the configuration variable to get.</param>
        /// <param name="str">The output parameter to store the config variable in.  This
        /// is a parameter rather than a return value to allow overloading of this
        /// method for different types.</param>
        public void Get( string config, out string str )
        {
            if ( configs.ContainsKey( config ) )
            {
                str = configs[config] as string;
            }
            else
            {
                session.Request.ConfigGet( config, out str );
                configs[config] = str;
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
        public void Get( string enumerationName, out MantisEnum enumeration )
        {
            string str;

            string configOption = string.Format( "{0}_enum_string", enumerationName );
            Get( configOption, out str );

            enumeration = new MantisEnum( str );
        }

        #region Private
        private readonly Session session;

        /// <summary>
        /// Hashtable of cached configuration options.
        /// </summary>
        private Hashtable configs = new Hashtable();
        #endregion
    }
}
