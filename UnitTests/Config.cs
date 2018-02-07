//-----------------------------------------------------------------------
// <copyright file="Config.cs" company="Victor Boctor">
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
    public sealed class Config : BaseTestFixture
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
        public void ConfigGetNull()
        {
            string str;
            var ex = Assert.Throws<ArgumentNullException>(() =>
         Session.Request.ConfigGet(null, out str));
        }

        [Test]
        public void ConfigGetEmpty()
        {
            string str;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
         Session.Request.ConfigGet(string.Empty, out str));
        }

        [Test]
        public void ConfigGetBlank()
        {
            string str;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
         Session.Request.ConfigGet("   ", out str));
        }

        [Test]
        public void ConfigGetUndefined()
        {
            string str;
            var ex = Assert.Throws<SoapException>(() =>
           Session.Request.ConfigGet("UndefinedConfigVariable", out str));
        }

        [Test]
        public void ConfigGetInvalidFormat()
        {
            string str;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        Session.Request.ConfigGet("Config Var With Spaces", out str));
        }

        [Test]
        public void ConfigGetValid()
        {
            string str;
            Session.Request.ConfigGet("administrator_email", out str);
        }

        [Test]
        public void ConfigGetDatabasePassword()
        {
            string str;

            string[] privateConfigs = new string[] {
                "hostname", "port", "db_username", "db_password",
                "password_confirm_hash_magic_string",
                "smtp_host", "smtp_username", "smtp_password"
            };

            bool exception;
            foreach (string config in privateConfigs)
            {
                exception = false;

                try
                {
                    Session.Request.ConfigGet(config, out str);
                }
                catch (SoapException)
                {
                    exception = true;
                }

                if (!exception)
                    Assert.Fail(string.Format("Private config '{0}' is not protected", config));
            }
        }
    }
}