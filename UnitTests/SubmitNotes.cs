//-----------------------------------------------------------------------
// <copyright file="SubmitNotes.cs" company="Victor Boctor">
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
    /// Test cases for submitting issue notes.
    /// </summary>
    [TestFixture]
    public sealed class SubmitIssueNotes : BaseTestFixture
    {
        [SetUp]
        public void TestFixtureSetup()
        {
            Connect();

            Issue issue = CreateIssue();
            issueId = Session.Request.IssueAdd(issue);
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            Session.Request.IssueDelete(issueId);
        }

        [Test]
        public void SubmitNote()
        {
            int issueNoteId = Session.Request.IssueNoteAdd(issueId, GetRandomNote());
            if (issueNoteId > 0)
                Session.Request.IssueNoteDelete(issueNoteId);
        }

        [Test]
        public void SubmitNoteAndCheckIt()
        {
            IssueNote note = GetRandomNote();

            int noteId = Session.Request.IssueNoteAdd(issueId, note);
            try
            {
                Assert.IsTrue(noteId > 0, "added note id must be greater than 0");

                Issue ownerIssue = Session.Request.IssueGet(issueId);

                Assert.IsTrue(ownerIssue.Notes.Length > 0, "There must be at least 1 note associated with the issue.");

                IssueNote retNote = null;

                foreach (IssueNote currentNote in ownerIssue.Notes)
                    if (currentNote.Id == noteId)
                        retNote = currentNote;

                Assert.IsNotNull(note);

                Assert.IsTrue(retNote.Id == noteId, "Note Id must be greater than 0");
                Assert.IsTrue(retNote.Author.Id > 0, "Author id must be greater than 0");
                Assert.AreEqual(retNote.Text, note.Text, "Verify note text");

                TimeSpan ts = DateTime.Now.Date - retNote.DateSubmitted.Date;
                Assert.IsTrue(ts.TotalMinutes <= 1, "Verify date submitted");

                ts = DateTime.Now.Date - retNote.LastModified.Date;
                Assert.IsTrue(ts.TotalMinutes <= 1, "Verify last modified");
            }
            finally
            {
                if (noteId > 0)
                    Session.Request.IssueNoteDelete(noteId);
            }
        }

        [Test]
        public void SubmitNoteEmpty()
        {
            IssueNote note = new IssueNote
            {
                Text = string.Empty
            };
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
         Session.Request.IssueNoteAdd(issueId, note));
        }

        [Test]
        public void SubmitNoteBlank()
        {
            IssueNote note = new IssueNote();
            note.Text = "   ";
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
           Session.Request.IssueNoteAdd(issueId, note));
        }

        [Test]
        public void SubmitNoteNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
           Session.Request.IssueNoteAdd(issueId, null));
        }

        [Test]
        public void SubmitNoteNullText()
        {
            IssueNote note = new IssueNote();
            note.Text = null;
            var ex = Assert.Throws<ArgumentNullException>(() =>
                Session.Request.IssueNoteAdd(issueId, note));
        }

        [Test]
        public void SubmitNoteToInvalidIssueId()
        {
            IssueNote note = new IssueNote();
            note.Text = "   ";
            var ex = Assert.Throws<ArgumentNullException>(() =>
         Session.Request.IssueNoteAdd(-5, note));
        }

        [Test]
        public void SubmitNoteToNonExistingIssue()
        {
            var ex = Assert.Throws<SoapException>(() =>
  Session.Request.IssueNoteAdd(5000, GetRandomNote()));
        }

        private IssueNote GetRandomNote()
        {
            IssueNote note = new IssueNote();
            note.Text = String.Format("NoteText {0}", Guid.NewGuid().ToString());
            return note;
        }

        private int issueId;
    }
}