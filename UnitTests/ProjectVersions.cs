#region Copyright © 2004-2005 Victor Boctor
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
    /// 
    /// </summary>
    [TestFixture]
    public sealed class ProjectVersions : BaseTestFixture
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

        #region Request.ProjectGetVersions()
        [Test]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void ProjectGetVersionsForInvalidProject()
        {
            Session.Request.ProjectGetVersions( -50 );
        }

        [Test]
        [ExpectedException( typeof( SoapException ) )]
        public void ProjectGetVersionsForNonExistingProject()
        {
            Session.Request.ProjectGetVersions( 99999 );
        }

        [Test]
        public void ProjectGetVersionsForFirstProject()
        {
            int projectId = FirstProjectId;
            if ( projectId == 0 )
                Assert.Ignore();

            Session.Request.ProjectGetVersions( projectId );
        }
        #endregion

        #region Request.ProjectGetReleasedVersions()
        [Test]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void ProjectGetVersionsReleasedForInvalidProject()
        {
            Session.Request.ProjectGetVersionsReleased( -50 );
        }

        [Test]
        [ExpectedException( typeof( SoapException ) )]
        public void ProjectGetVersionsReleasedForNonExistingProject()
        {
            Session.Request.ProjectGetVersionsReleased( 99999 );
        }

        [Test]
        public void ProjectGetVersionsReleasedForFirstProject()
        {
            int projectId = FirstProjectId;
            if ( projectId == 0 )
                Assert.Ignore();

            Session.Request.ProjectGetVersionsReleased( projectId );
        }
        #endregion

        #region Request.ProjectGetUnreleasedVersions()
        [Test]
        [ExpectedException( typeof( ArgumentOutOfRangeException ) )]
        public void ProjectGetVersionsUnreleasedForInvalidProject()
        {
            Session.Request.ProjectGetVersionsUnreleased( -50 );
        }

        [Test]
        [ExpectedException( typeof( SoapException ) )]
        public void ProjectGetVersionsUnreleasedForNonExistingProject()
        {
            Session.Request.ProjectGetVersionsUnreleased( 99999 );
        }

        [Test]
        public void ProjectGetVersionsUnreleasedForFirstProject()
        {
            int projectId = FirstProjectId;
            if ( projectId == 0 )
                Assert.Ignore();

            Session.Request.ProjectGetVersionsUnreleased( projectId );
        }
        #endregion

        #region Request.ProjectGetVersions()
        [Test]
        public void ProjectVersionAddNotReleased()
        {
            ProjectVersionAddTestCase(false);
        }

        [Test]
        public void ProjectVersionAddReleased()
        {
            ProjectVersionAddTestCase(true);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ProjectVersionAddWithId()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Id = 1000;
            Session.Request.ProjectVersionAdd(versionToAdd);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProjectVersionAddWithNullName()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Name = null;
            Session.Request.ProjectVersionAdd(versionToAdd);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProjectVersionAddWithNullDescription()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Description = null;
            Session.Request.ProjectVersionAdd(versionToAdd);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get a project version by id or null if not found.
        /// </summary>
        /// <param name="projectVersionId">The version id</param>
        /// <returns></returns>
        private ProjectVersion GetProjectVersionById(int projectVersionId)
        {
            ProjectVersion[] versions = Session.Request.ProjectGetVersions(this.FirstProjectId);

            foreach (ProjectVersion version in versions)
            {
                if (version.Id == projectVersionId)
                {
                    return version;
                }
            }

            return null;
        }

        private void ProjectVersionAddTestCase(bool released)
        {
            const string VersionName = "VersionName";
            const string VersionDescription = "VersionDescription";
            DateTime now = DateTime.Today;  // Use today to avoid approximation errors

            ProjectVersion version = new ProjectVersion();
            version.ProjectId = this.FirstProjectId;
            version.Name = VersionName;
            version.Description = VersionDescription;
            version.DateOrder = now;
            version.IsReleased = released;

            int versionId = Session.Request.ProjectVersionAdd(version);

            try
            {
                // Check that the version id is set.
                Assert.AreEqual(versionId, version.Id);
                Assert.AreNotEqual(versionId, 0);

                ProjectVersion versionAdded = GetProjectVersionById(versionId);

                Assert.AreEqual(versionId, versionAdded.Id);
                Assert.AreEqual(VersionName, versionAdded.Name);
                Assert.AreEqual(VersionDescription, versionAdded.Description);
                Assert.AreEqual(now, versionAdded.DateOrder);
                Assert.AreEqual(released, versionAdded.IsReleased);
            }
            finally
            {
                Session.Request.ProjectVersionDelete(versionId);
            }
        }

        private ProjectVersion CreateVersionToAdd()
        {
            const string VersionName = "VersionName";
            const string VersionDescription = "VersionDescription";

            ProjectVersion version = new ProjectVersion();
            version.ProjectId = this.FirstProjectId;
            version.Name = VersionName;
            version.Description = VersionDescription;
            version.DateOrder = DateTime.Now;
            version.IsReleased = false;

            return version;
        }
        #endregion
    }
}
