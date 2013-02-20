//-----------------------------------------------------------------------
// <copyright file="Attachments.cs" company="Victor Boctor">
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
    /// Summary description for Class1.
    /// </summary>
    [TestFixture]
    public sealed class AttachmentTestCases : BaseTestFixture
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
        public void AddBinaryAttachment()
        {
            int projectId = FirstProjectId;

            Issue issue = new Issue();
            issue.Project = new ObjectRef(projectId);
            issue.Summary = GetRandomSummary();
            issue.Description = GetRandomDescription();
            issue.Category = new ObjectRef(GetFirstCategory(projectId));

            int issueId = this.SubmitIssue(issue);

            Issue retrievedIssueBeforeAdd = Session.Request.IssueGet(issueId);
            Assert.AreEqual(0, retrievedIssueBeforeAdd.Attachments.Length);
            
            byte[] attachment = CreateBinaryAttachment(1024);
            byte[] base64 = Base64Encode(attachment);

            int attachmentId = Session.Request.IssueAttachmentAdd(issueId, "blah.mpg", "video/mpeg", base64);

            byte[] retrievedAttachment = Session.Request.IssueAttachmentGet(attachmentId);

            Assert.IsNotNull(retrievedAttachment);
            Assert.AreEqual(attachment.Length, retrievedAttachment.Length);

            for (int i = 0; i < attachment.Length; ++i)
            {
                Assert.AreEqual(attachment[i], retrievedAttachment[i]);
            }

            Issue retrievedIssueAfterAdd = Session.Request.IssueGet(issueId);

            Assert.AreEqual(1, retrievedIssueAfterAdd.Attachments.Length);

            Session.Request.IssueAttachmentDelete(attachmentId);

            Issue retrievedIssueAfterDelete = Session.Request.IssueGet(issueId);
            Assert.AreEqual(0, retrievedIssueAfterDelete.Attachments.Length);

            Session.Request.IssueDelete(issueId);
        }

        [Test]
        public void AddTextAttachment()
        {
            int projectId = FirstProjectId;

            Issue issue = new Issue();
            issue.Project = new ObjectRef(projectId);
            issue.Summary = GetRandomSummary();
            issue.Description = GetRandomDescription();
            issue.Category = new ObjectRef(GetFirstCategory(projectId));

            int issueId = this.SubmitIssue(issue);
            byte[] attachment = CreateTextAttachment(1024);
            byte[] base64 = Base64Encode(attachment);

            int attachmentId = Session.Request.IssueAttachmentAdd(issueId, "sample.txt", "text/plain", base64);

            byte[] retrievedAttachment = Session.Request.IssueAttachmentGet(attachmentId);

            Assert.IsNotNull(retrievedAttachment);
            Assert.AreEqual(attachment.Length, retrievedAttachment.Length);

            for (int i = 0; i < attachment.Length; ++i)
            {
                Assert.AreEqual(attachment[i], retrievedAttachment[i]);
            }

            Session.Request.IssueDelete(issueId);
        }

        private static byte[] Base64Encode(byte[] attachment)
        {
            string base64String = Convert.ToBase64String(attachment);
            return System.Text.UTF8Encoding.ASCII.GetBytes(base64String);
        }

        private static byte[] CreateTextAttachment(int size)
        {
            byte[] bytes = new byte[size];

            for (int i = 0; i < size; ++i)
            {
                bytes[i] = (byte)(i % 32 + 65);
            }

            return bytes;
        }

        private static byte[] CreateBinaryAttachment(int size)
        {
            byte[] bytes = new byte[size];

            for (int i = 0; i < size; ++i)
            {
                bytes[i] = (byte)(i % 255);
            }

            return bytes;
        }

        private int SubmitIssue(Issue issue)
        {
            int issueId = Session.Request.IssueAdd(issue);
            if (issueId > 0)
            {
                Assert.IsTrue(Session.Request.IssueExists(issueId));
                Assert.AreEqual(issueId, Session.Request.IssueGetLastId(issue.Project.Id));
                Assert.AreEqual(issueId, Session.Request.IssueGetIdFromSummary(issue.Summary));
            }

            return issueId;
        }

        private void DeleteIssue(int issueId)
        {
            Session.Request.IssueDelete(issueId);
            Assert.IsFalse(Session.Request.IssueExists(issueId));
        }
    }
}
