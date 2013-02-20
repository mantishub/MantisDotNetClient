//-----------------------------------------------------------------------
// <copyright file="IssueGetLastId.cs" company="Victor Boctor">
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
    /// A test fixture that tests the <see cref="Request.IssueGetLastId"/> method.
    /// </summary>
    /// <remarks>
    /// The focus of this fixture is to test the special cases for using this method.  However, the method
    /// is also tested as part of the <see cref="SubmitIssueTestCases"/> fixture in the 
    /// <see cref="SubmitIssueTestCases.SubmitAndDelete"/> method which is used by almost all of the
    /// fixture test cases.
    /// </remarks>
    [TestFixture]
    public sealed class IssueGetLastId : BaseTestFixture
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
        public void IssueGetLastIdForDefaultProject()
        {
            Session.Request.IssueGetLastId( -1 );
        }

        [Test]
        public void IssueGetLastIdForAllProjects()
        {
            Session.Request.IssueGetLastId( 0 );
        }

        [Test]
        public void IssueGetLastIdForFirstProject()
        {
            int projectId = FirstProjectId;
            if ( projectId == 0 )
                Assert.Ignore();

            Session.Request.IssueGetLastId( projectId );
        }

        [Test]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void IssueGetLastIdForInvalidProjectId()
        {
            Session.Request.IssueGetLastId( -50 );
        }

        [Test]
        [ExpectedException( typeof( SoapException ) )]
        public void IssueGetLastIdForNonExistingProject()
        {
            Session.Request.IssueGetLastId( 999999 );
        }
    }
}
