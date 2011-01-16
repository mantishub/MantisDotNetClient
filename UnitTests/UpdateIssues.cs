#region Copyright © 2005 Victor Boctor
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
    public sealed class UpdateIssuesTestCases : BaseTestFixture
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
        public void UpdateIssue()
        {
            int projectId = FirstProjectId;

            string originalSummary = GetRandomSummary();
            string newSummary = GetRandomSummary();

            string originalDescription = GetRandomDescription();
            string newDescription = GetRandomDescription();

            Issue issue = new Issue();
            issue.Project = new ObjectRef( projectId );
            issue.Summary = originalSummary;
            issue.Description = originalDescription;
            issue.Category = new ObjectRef( GetFirstCategory( projectId ) );

            int issueId = Session.Request.IssueAdd( issue );

            try
            {
                Issue issueToUpdate = Session.Request.IssueGet(issueId);
                issueToUpdate.Summary = newSummary;
                issueToUpdate.Description = newDescription;
                Session.Request.IssueUpdate(issueToUpdate);

                Issue updatedIssue = Session.Request.IssueGet(issueId);
                Assert.AreEqual(newSummary, updatedIssue.Summary);
                Assert.AreEqual(newDescription, updatedIssue.Description);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void UpdateIssueStepsAndAdditional()
        {
            int projectId = FirstProjectId;

            string originalSummary = GetRandomSummary();
            string newSummary = GetRandomSummary();

            string originalDescription = GetRandomDescription();
            string newDescription = GetRandomDescription();

            Issue issue = new Issue();
            issue.Project = new ObjectRef(projectId);
            issue.Summary = originalSummary;
            issue.Description = originalDescription;
            issue.Category = new ObjectRef(GetFirstCategory(projectId));
            issue.AdditionalInformation = "additional";
            issue.StepsToReproduce = "steps";

            int issueId = Session.Request.IssueAdd(issue);

            try
            {
                Issue issueToUpdate = Session.Request.IssueGet(issueId);
                issueToUpdate.Summary = newSummary;
                issueToUpdate.Description = newDescription;
                issueToUpdate.StepsToReproduce = issueToUpdate.StepsToReproduce + "2";
                issueToUpdate.AdditionalInformation = issueToUpdate.AdditionalInformation + "2";
                Session.Request.IssueUpdate(issueToUpdate);

                Issue updatedIssue = Session.Request.IssueGet(issueId);
                Assert.AreEqual(newSummary, updatedIssue.Summary);
                Assert.AreEqual(newDescription, updatedIssue.Description);
                Assert.AreEqual("additional2", updatedIssue.AdditionalInformation);
                Assert.AreEqual("steps2", updatedIssue.StepsToReproduce);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void IssueCheckinNotFixed()
        {
            int projectId = FirstProjectId;

            string originalSummary = GetRandomSummary();
            string newSummary = GetRandomSummary();

            string originalDescription = GetRandomDescription();
            string newDescription = GetRandomDescription();

            Issue issue = new Issue();
            issue.Project = new ObjectRef(projectId);
            issue.Summary = originalSummary;
            issue.Description = originalDescription;
            issue.Category = new ObjectRef(GetFirstCategory(projectId));

            int issueId = Session.Request.IssueAdd(issue);

            try
            {
                Issue issueAfterSubmit = Session.Request.IssueGet(issueId);

                const string PartialFixComment = "This is a partial fix";
                Session.Request.IssueCheckin(issueId, PartialFixComment, false);
                Issue issueAfterFirstCheckin = Session.Request.IssueGet(issueId);
                Assert.AreEqual(issueAfterFirstCheckin.Status.Id, issueAfterSubmit.Status.Id);
                Assert.AreEqual(issueAfterFirstCheckin.Notes.Length, 1);
                Assert.AreEqual(issueAfterFirstCheckin.Notes[0].Text, PartialFixComment);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void IssueCheckinFixed()
        {
            int projectId = FirstProjectId;

            string originalSummary = GetRandomSummary();
            string newSummary = GetRandomSummary();

            string originalDescription = GetRandomDescription();
            string newDescription = GetRandomDescription();

            Issue issue = new Issue();
            issue.Project = new ObjectRef(projectId);
            issue.Summary = originalSummary;
            issue.Description = originalDescription;
            issue.Category = new ObjectRef(GetFirstCategory(projectId));

            int issueId = Session.Request.IssueAdd(issue);

            try
            {
                Issue issueAfterSubmit = Session.Request.IssueGet(issueId);

                const string FullFixComment = "This is a full fix which resolves the issue";
                Session.Request.IssueCheckin(issueId, FullFixComment, true);
                Issue issueAfterSecondCheckin = Session.Request.IssueGet(issueId);
                Assert.AreEqual(issueAfterSecondCheckin.Notes.Length, 1);
                Assert.AreEqual(issueAfterSecondCheckin.Notes[0].Text, FullFixComment);

                // The status may or may not change depending on the Mantis configuration, 
                // hence we are not checking it.
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void IssueUpdateWithNotesNoChanges()
        {
            Issue issue = this.GetRandomIssue();
            int issueId = Session.Request.IssueAdd(issue);

            try
            {
                IssueNote note = this.GetRandomNote();
                Session.Request.IssueNoteAdd(issueId, note);

                Issue issueWithNote = Session.Request.IssueGet(issueId);

                Assert.AreEqual(1, issueWithNote.Notes.Length);

                Session.Request.IssueUpdate(issueWithNote);

                Issue issueWithNoteAfterUpdate = Session.Request.IssueGet(issueId);

                Assert.AreEqual(1, issueWithNoteAfterUpdate.Notes.Length);
                Assert.AreEqual(note.Text, issueWithNoteAfterUpdate.Notes[0].Text);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void IssueUpdateToAddNotes()
        {
            Issue issue = this.GetRandomIssue();
            int issueId = Session.Request.IssueAdd(issue);

            try
            {
                issue = Session.Request.IssueGet(issueId);
 
                Assert.AreEqual(0, issue.Notes.Length);

                IssueNote note = this.GetRandomNote();
                issue.Notes = new IssueNote[1];
                issue.Notes[0] = note;

                Session.Request.IssueUpdate(issue);

                Issue issueWithNote = Session.Request.IssueGet(issueId);

                Assert.AreEqual(1, issueWithNote.Notes.Length);
                Assert.AreEqual(note.Text, issueWithNote.Notes[0].Text);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        [Test]
        public void IssueUpdateToUpdateNotes()
        {
            Issue issue = this.GetRandomIssue();
            int issueId = Session.Request.IssueAdd(issue);

            IssueNote originalNote = this.GetRandomNote();
            int originalNoteId = Session.Request.IssueNoteAdd(issueId, originalNote);

            try
            {
                issue = Session.Request.IssueGet(issueId);

                Assert.AreEqual(1, issue.Notes.Length);
                Assert.AreEqual(originalNoteId, issue.Notes[0].Id);
                Assert.AreEqual(originalNote.Text, issue.Notes[0].Text);

                const string NewNoteText = "new note text";

                issue.Notes[0].Text = NewNoteText;

                Session.Request.IssueUpdate(issue);

                Issue issueWithUpdatedNote = Session.Request.IssueGet(issueId);

                Assert.AreEqual(1, issueWithUpdatedNote.Notes.Length);
                Assert.AreEqual(originalNoteId, issueWithUpdatedNote.Notes[0].Id);
                Assert.AreEqual(NewNoteText, issueWithUpdatedNote.Notes[0].Text);
            }
            finally
            {
                Session.Request.IssueDelete(issueId);
            }
        }

        private Issue GetRandomIssue()
        {
            int projectId = FirstProjectId;

            Issue issue = new Issue();

            issue.Project = new ObjectRef(projectId);
            issue.Summary = this.GetRandomSummary();
            issue.Description = this.GetRandomDescription();
            issue.Category = new ObjectRef(this.GetFirstCategory(projectId));

            return issue;
        }

        private IssueNote GetRandomNote()
        {
            IssueNote note = new IssueNote();
            note.Text = String.Format("NoteText {0}", Guid.NewGuid().ToString());
            return note;
        }
    }
}
