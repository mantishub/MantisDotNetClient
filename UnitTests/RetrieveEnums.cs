//-----------------------------------------------------------------------
// <copyright file="RetrieveEnums.cs" company="Victor Boctor">
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
using System.Collections.Generic;
using System.Net;
using System.Web.Services.Protocols;

using NUnit.Framework;

namespace Futureware.MantisConnect.UnitTests
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [TestFixture]
    public sealed class RetrieveEnums : BaseTestFixture
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
        public void StatusEnum()
        {
            MantisEnum statusEnum = Session.Config.StatusEnum;
            Assert.IsTrue(statusEnum.Count > 0);
            WriteEnum("Status", statusEnum);
        }

        [Test]
        public void PriorityEnum()
        {
            MantisEnum priorityEnum = Session.Config.PriorityEnum;
            Assert.IsTrue(priorityEnum.Count > 0);
            WriteEnum("Priority", priorityEnum);
        }

        [Test]
        public void SeverityEnum()
        {
            MantisEnum severityEnum = Session.Config.SeverityEnum;
            Assert.IsTrue(severityEnum.Count > 0);
            WriteEnum("Severity", severityEnum);
        }

        [Test]
        public void ProjectionEnum()
        {
            MantisEnum projectionEnum = Session.Config.ProjectionEnum;
            Assert.IsTrue(projectionEnum.Count > 0);
            WriteEnum("Projection", projectionEnum);
        }

        [Test]
        public void EtaEnum()
        {
            MantisEnum etaEnum = Session.Config.EtaEnum;
            Assert.IsTrue(etaEnum.Count > 0);
            WriteEnum("Eta", etaEnum);
        }

        [Test]
        public void ReproducibilityEnum()
        {
            MantisEnum reproducibilityEnum = Session.Config.ReproducibilityEnum;
            Assert.IsTrue(reproducibilityEnum.Count > 0);
            WriteEnum("Reproducibility", reproducibilityEnum);
        }

        [Test]
        public void AccessLevelEnum()
        {
            MantisEnum accessLevelEnum = Session.Config.AccessLevelEnum;
            Assert.IsTrue(accessLevelEnum.Count > 0);
            WriteEnum("AccessLevel", accessLevelEnum);
        }

        [Test]
        public void ResolutionEnum()
        {
            MantisEnum resolutionEnum = Session.Config.ResolutionEnum;
            Assert.IsTrue(resolutionEnum.Count > 0);
            WriteEnum("Resolution", resolutionEnum);
        }

        [Test]
        public void ViewStateEnum()
        {
            MantisEnum viewStateEnum = Session.Config.ViewStateEnum;
            Assert.IsTrue(viewStateEnum.Count > 0);
            WriteEnum("ViewState", viewStateEnum);
        }

        private void WriteEnum(string enumType, MantisEnum mantisEnum)
        {
            Console.WriteLine();
            Console.WriteLine(enumType);

            ICollection<string> labels = mantisEnum.GetLabels();

            foreach (string label in labels)
            {
                Console.WriteLine(label);
            }
        }
    }
}