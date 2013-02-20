//-----------------------------------------------------------------------
// <copyright file="ProjectCategories.cs" company="Victor Boctor">
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
    /// 
    /// </summary>
    [TestFixture]
    public sealed class ProjectCategories : BaseTestFixture
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
        public void ProjectGetCategoriesForInvalidProject()
        {
            Session.Request.ProjectGetCategories( -50 );
        }

        [Test]
        [ExpectedException( typeof( SoapException ) )]
        public void ProjectGetCategoriesForNonExistingProject()
        {
            Session.Request.ProjectGetCategories( 99999 );
        }

        [Test]
        public void ProjectGetCategoriesForFirstProject()
        {
            int projectId = FirstProjectId;
            if ( projectId == 0 )
                Assert.Ignore();

            Session.Request.ProjectGetCategories( projectId );
        }
    }
}
