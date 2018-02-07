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
        [SetUp]
        public void TestFixtureSetup()
        {
            Connect();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
        }

        [Test]
        public void RetrieveIssueWithIdZero()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
           Session.Request.IssueGet(0));
        }

        [Test]
        public void RetrieveIssueWithIdNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
          Session.Request.IssueGet(-1));
        }

        [Test]
        public void IssueExistsWithIdZero()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
          Session.Request.IssueExists(0));
        }

        [Test]
        public void GetIssueIdFromEmptySummary()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
          Session.Request.IssueGetIdFromSummary(string.Empty));
        }

        [Test]
        public void GetIssueIdFromBlankSummary()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
          Session.Request.IssueGetIdFromSummary("   "));
        }

        [Test]
        public void GetIssueIdFromNullSummary()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
          Session.Request.IssueGetIdFromSummary(null));
        }

        [Test]
        public void GetIssueIdFromNonExistingSummary()
        {
            int issueId = Session.Request.IssueGetIdFromSummary(Guid.NewGuid().ToString());
            Assert.AreEqual(0, issueId);
        }

        [Test]
        public void IssueExistsWithIdNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            Session.Request.IssueExists(-1));
        }

        [Test]
        public void RetrieveIssueThatDoesNotExist()
        {
            int id = 5000;
            while (Session.Request.IssueExists(id))
                id += 1000;
            var ex = Assert.Throws<SoapException>(() =>
           Session.Request.IssueGet(id));
        }
    }
}