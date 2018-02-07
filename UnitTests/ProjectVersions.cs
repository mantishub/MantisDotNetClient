//-----------------------------------------------------------------------
// <copyright file="ProjectVersions.cs" company="Victor Boctor">
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
    public sealed class ProjectVersions : BaseTestFixture
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

        #region Request.ProjectGetVersions()

        [Test]
        public void ProjectGetVersionsForInvalidProject()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            Session.Request.ProjectGetVersions(-50));
        }

        [Test]
        public void ProjectGetVersionsForNonExistingProject()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            Session.Request.ProjectGetVersions(99999));
        }

        [Test]
        public void ProjectGetVersionsForFirstProject()
        {
            int projectId = FirstProjectId;
            if (projectId == 0)
                Assert.Ignore();

            Session.Request.ProjectGetVersions(projectId);
        }

        #endregion Request.ProjectGetVersions()

        #region Request.ProjectGetReleasedVersions()

        [Test]
        public void ProjectGetVersionsReleasedForInvalidProject()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
          Session.Request.ProjectGetVersionsReleased(-50));
        }

        [Test]
        public void ProjectGetVersionsReleasedForNonExistingProject()
        {
            var ex = Assert.Throws<SoapException>(() =>
            Session.Request.ProjectGetVersionsReleased(99999));
        }

        [Test]
        public void ProjectGetVersionsReleasedForFirstProject()
        {
            int projectId = FirstProjectId;
            if (projectId == 0)
                Assert.Ignore();

            Session.Request.ProjectGetVersionsReleased(projectId);
        }

        #endregion Request.ProjectGetReleasedVersions()

        #region Request.ProjectGetUnreleasedVersions()

        [Test]
        public void ProjectGetVersionsUnreleasedForInvalidProject()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            Session.Request.ProjectGetVersionsUnreleased(-50));
        }

        [Test]
        public void ProjectGetVersionsUnreleasedForNonExistingProject()
        {
            var ex = Assert.Throws<SoapException>(() =>
           Session.Request.ProjectGetVersionsUnreleased(99999));
        }

        [Test]
        public void ProjectGetVersionsUnreleasedForFirstProject()
        {
            int projectId = FirstProjectId;
            if (projectId == 0)
                Assert.Ignore();

            Session.Request.ProjectGetVersionsUnreleased(projectId);
        }

        #endregion Request.ProjectGetUnreleasedVersions()

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
        public void ProjectVersionAddWithId()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Id = 1000;
            var ex = Assert.Throws<ArgumentException>(() =>
            Session.Request.ProjectVersionAdd(versionToAdd));
        }

        [Test]
        public void ProjectVersionAddWithNullName()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Name = null;
            var ex = Assert.Throws<ArgumentException>(() =>
            Session.Request.ProjectVersionAdd(versionToAdd));
        }

        [Test]
        public void ProjectVersionAddWithNullDescription()
        {
            ProjectVersion versionToAdd = CreateVersionToAdd();
            versionToAdd.Description = null;
            var ex = Assert.Throws<ArgumentNullException>(() =>
          Session.Request.ProjectVersionAdd(versionToAdd));
        }

        #endregion Request.ProjectGetVersions()

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

        #endregion Private Methods
    }
}