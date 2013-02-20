//-----------------------------------------------------------------------
// <copyright file="BaseTestFixture.cs" company="Victor Boctor">
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

using NUnit.Framework;

namespace Futureware.MantisConnect.UnitTests
{
	/// <summary>
	/// Summary description for BaseTestFixture.
	/// </summary>
	public abstract class BaseTestFixture
	{
		protected string UserName
		{
			get { return "administrator"; }
		}

		protected string Password
		{
			get { return "root"; }
		}

		protected string Url
		{
			get { return "http://127.0.0.1/mantisbt/api/soap/mantisconnect.php"; }
		}

		protected void Connect()
		{
			// why does NUnit run the test fixture setup twice?
			if ( session != null )
				return;

			NetworkCredential nc = null;

			// string basicHttpAuthUserName = ConfigurationSettings.AppSettings["BasicHttpAuthUserName"];
			// string basicHttpAuthPassword = ConfigurationSettings.AppSettings["BasicHttpAuthPassword"];
			// if ( ( basicHttpAuthUserName != null ) && ( basicHttpAuthUserName.Length > 0 ) && ( basicHttpAuthPassword != null ) )
			//	nc = new NetworkCredential( basicHttpAuthUserName, basicHttpAuthPassword );

			session = new Session( Url, UserName, Password, nc );

			session.Connect();
		}

		protected string GetRandomSummary()
		{
			return String.Format( "Summary {0}", Guid.NewGuid().ToString() );
		}

		protected string GetRandomDescription()
		{
			return String.Format( "Description {0}", Guid.NewGuid().ToString() );
		}

		protected Issue CreateIssue()
		{
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Project = new ObjectRef( projectId );
			issue.Summary = GetRandomSummary();
			issue.Description = GetRandomDescription();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

			return issue;
		}

		protected string GetFirstCategory( int projectId )
		{
			string[] categories = Session.Request.ProjectGetCategories( projectId );
			if ( categories.Length == 0 )
				return string.Empty;

			return categories[0];
		}

		protected int FirstProjectId
		{
			get 
			{
				Project[] projects = session.Request.UserGetAccessibleProjects(); 
				if ( projects.Length == 0 )
					return 0;

				return projects[0].Id;
			}
		}

		protected string FirstProjectName
		{
			get 
			{
				Project[] projects = session.Request.UserGetAccessibleProjects(); 
				if ( projects.Length == 0 )
					return string.Empty;

				return projects[0].Name;
			}
		}

		protected Session Session
		{
			get { return session; }
		}

		#region Private Members
		/// <summary>
		/// Session used to communicate with MantisConnect.
		/// </summary>
		private Session session;
		#endregion
	}
}
