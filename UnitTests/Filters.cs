//-----------------------------------------------------------------------
// <copyright file="Filters.cs" company="Victor Boctor">
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
    /// A test fixture that tests the retrieval of the filters and the issues matching
    /// these filters.
    /// </summary>
    [TestFixture]
    public sealed class Filters : BaseTestFixture
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
        public void GetFiltersForAllProjects()
        {
            try
            {
                Project[] projects = Session.Request.UserGetAccessibleProjects();

                foreach (Project project in projects)
                {
                    Filter[] filters = Session.Request.UserGetFilters(project.Id);

                    foreach (Filter filter in filters)
                    {
                        Session.Request.GetIssues(project.Id, filter.Id, 1, 10);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
