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
using System.Net;
using System.Web.Services.Protocols;

using NUnit.Framework;

namespace Futureware.MantisConnect.UnitTests
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[TestFixture]
	public sealed class SubmitIssueTestCases : BaseTestFixture
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
		public void SubmitIssue()
		{
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Project = new ObjectRef( projectId );
			issue.Summary = GetRandomSummary();
			issue.Description = GetRandomDescription();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

			SubmitAndDelete( issue );
		}

		[Test]
		[ExpectedException( typeof( SoapException ) )]
		public void SubmitIssueNoProjectId()
		{
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Summary = GetRandomSummary();
			issue.Description = GetRandomDescription();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

			SubmitAndDelete( issue );
		}

		[ExpectedException( typeof( SoapException ) )]
		public void SubmitIssueNoSummary()
		{
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Project = new ObjectRef( projectId );
			issue.Description = GetRandomDescription();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

			SubmitAndDelete( issue );
		}

		[Test]
		[ExpectedException( typeof( ArgumentOutOfRangeException ) )]
		public void SubmitIssueNoDescription()
		{
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Project = new ObjectRef( projectId );
			issue.Summary = GetRandomSummary();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

			SubmitAndDelete( issue );
		}

		[Test]
		public void SubmitIssueNoCategory()
		{
			int projectId = FirstProjectId;

			string cfg;
			Session.Request.ConfigGet( "mc_error_when_category_not_found", out cfg );

			bool errorWhenCategoryNotFound = cfg == "1";

			try
			{
				Issue issue = new Issue();
				issue.Project = new ObjectRef( projectId );
				issue.Summary = GetRandomSummary();
				issue.Description = GetRandomDescription();

				SubmitAndDelete( issue );
			}
			catch( SoapException )
			{
				if ( !errorWhenCategoryNotFound )
					throw;

				return;
			}

			if ( errorWhenCategoryNotFound )
				Assert.Fail( "No category specified, no default category, and no exception raised." );
		}

		[Test]
		public void SubmitIssueAndCheckIt()
		{
			const string StepsToRepro = "steps";
			const string AdditionalInfo = "additional";
			int projectId = FirstProjectId;

			Issue issue = new Issue();
			issue.Project = new ObjectRef( projectId );
			issue.Summary = GetRandomSummary();
			issue.Description = GetRandomDescription();
			issue.Category = new ObjectRef( GetFirstCategory( projectId ) );
			issue.StepsToReproduce = StepsToRepro;
			issue.AdditionalInformation = AdditionalInfo;

			int issueId = Session.Request.IssueAdd( issue );

			try
			{
				Issue issueRet = Session.Request.IssueGet( issueId );

				Assert.AreEqual( issueId, issueRet.Id );
				Assert.AreEqual( issue.Project.Id, issueRet.Project.Id );
				Assert.AreEqual( issue.Summary, issueRet.Summary );
				Assert.AreEqual( issue.Description, issueRet.Description );
				Assert.AreEqual( issue.Category.Name, issueRet.Category.Name );
				Assert.AreEqual( Session.Username, issueRet.ReportedBy.Name );
				Assert.IsTrue( issueRet.Severity.Id != 0 );
				Assert.IsTrue( issueRet.Priority.Id != 0 );
				Assert.IsTrue( issueRet.Reproducibility.Id != 0 );
				Assert.IsTrue( issueRet.Projection.Id != 0 );
				Assert.IsTrue( issueRet.Eta.Id != 0 );
				Assert.AreEqual(StepsToRepro, issueRet.StepsToReproduce);
				Assert.AreEqual(AdditionalInfo, issueRet.AdditionalInformation);
				Assert.IsTrue(issueRet.Platform.Length == 0);
				Assert.IsTrue( issueRet.Os.Length == 0 );
				Assert.IsTrue( issueRet.OsBuild.Length == 0 );
				Assert.IsTrue( issueRet.FixedInVersion.Length == 0 );
				Assert.AreEqual( 0, issueRet.SponsorshipTotal );
				Assert.AreEqual( 0, issueRet.Attachments.Length );
			}
			finally
			{
				Session.Request.IssueDelete( issueId );
			}
		}

        [Test]
        public void SubmitIssueWithNotesAndCheckIt()
        {
            int projectId = FirstProjectId;

            string note1Text = "This is the text for the first note";
            string note2Text = "This is the text for the second note";

            IssueNote note1 = new IssueNote();
            note1.Text = note1Text;

            IssueNote note2 = new IssueNote();
            note2.Text = note2Text;

            Issue issue = new Issue();
            issue.Project = new ObjectRef( projectId );
            issue.Summary = GetRandomSummary();
            issue.Description = GetRandomDescription();
            issue.Category = new ObjectRef( GetFirstCategory( projectId ) );
            issue.Notes = new IssueNote[] { note1, note2 };

            int issueId = Session.Request.IssueAdd( issue );

            try
            {
                Issue issueRet = Session.Request.IssueGet( issueId );

                Assert.IsNotNull( issueRet );
                Assert.IsNotNull( issueRet.Notes );
                Assert.AreEqual( 2, issueRet.Notes.Length );
                Assert.AreEqual( note1Text, issueRet.Notes[0].Text );
                Assert.AreEqual( note2Text, issueRet.Notes[1].Text );
            }
            finally
            {
                Session.Request.IssueDelete( issueId );
            }
        }

        private void SubmitAndDelete( Issue issue )
		{
			int issueId = Session.Request.IssueAdd( issue );
			if ( issueId > 0 )
			{
				Assert.IsTrue( Session.Request.IssueExists( issueId ) );
				Assert.AreEqual( issueId, Session.Request.IssueGetLastId( issue.Project.Id ) );
				Assert.AreEqual( issueId, Session.Request.IssueGetIdFromSummary( issue.Summary ) );

				Session.Request.IssueDelete( issueId );

				Assert.IsFalse( Session.Request.IssueExists( issueId ) );
				Assert.AreEqual( 0, Session.Request.IssueGetIdFromSummary( issue.Summary ) );
			}
		}
	}
}
