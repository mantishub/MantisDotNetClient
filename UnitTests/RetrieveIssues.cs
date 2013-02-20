//-----------------------------------------------------------------------
// <copyright file="RetrieveIssues.cs" company="Victor Boctor">
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
using System.Web.Services.Protocols;

using NUnit.Framework;

namespace Futureware.MantisConnect.UnitTests
{
	/// <summary>
	/// A test fixture to test the error scenarios for retrieving an issue. 
	/// The case for retrieving an existing issue is covered in other test
	/// cases.
	/// </summary>
	[TestFixture]
	public sealed class RetrieveIssuesTestCases : BaseTestFixture
	{
		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			Connect();
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void RetrieveIssueWithIdZero()
		{
			Session.Request.IssueGet( 0 );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void RetrieveIssueWithIdNegative()
		{
			Session.Request.IssueGet( -1 );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void IssueExistsWithIdZero()
		{
			Session.Request.IssueExists( 0 );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void GetIssueIdFromEmptySummary()
		{
			Session.Request.IssueGetIdFromSummary( string.Empty );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void GetIssueIdFromBlankSummary()
		{
			Session.Request.IssueGetIdFromSummary( "   " );
		}

		[Test]
		[ExpectedException( typeof( ArgumentNullException ) )]
		public void GetIssueIdFromNullSummary()
		{
			Session.Request.IssueGetIdFromSummary( null );
		}

		[Test]
		public void GetIssueIdFromNonExistingSummary()
		{
			int issueId = Session.Request.IssueGetIdFromSummary( Guid.NewGuid().ToString() );
			Assert.AreEqual( 0, issueId );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void IssueExistsWithIdNegative()
		{
			Session.Request.IssueExists( -1 );
		}

		[Test]
		[ExpectedException( typeof( SoapException ) )]
		public void RetrieveIssueThatDoesNotExist()
		{
			int id = 5000;
			while ( Session.Request.IssueExists( id ) )
				id += 1000;
	
			Session.Request.IssueGet( id );
		}
	}
}
