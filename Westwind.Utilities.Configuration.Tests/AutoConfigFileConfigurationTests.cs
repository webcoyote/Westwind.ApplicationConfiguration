﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Westwind.Utilities.Configuration.Tests
{
    /// <summary>
    /// Tests default config file implementation that uses
    /// only base constructor behavior - (config file and section config only)    
    /// </summary>
    [TestClass]
    public class AutoConfigFileConfigurationTests
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void DefaultConstructorInstanceTest()
        {
            var config = new AutoConfigFileConfiguration(null);

            Assert.IsNotNull(config);
            Assert.IsFalse(string.IsNullOrEmpty(config.ApplicationName));            

            string text = File.ReadAllText(TestHelpers.GetTestConfigFilePath());
            Console.WriteLine(text);          
        }

        [TestMethod]
        public void WriteConfigurationTest()
        {
            var config = new AutoConfigFileConfiguration(null);
            config.MaxDisplayListItems = 12;
            config.DebugMode = DebugModes.DeveloperErrorMessage;
            config.ApplicationName = "Changed";
            config.SendAdminEmailConfirmations = true;
            config.Write();
            
            string text = File.ReadAllText(TestHelpers.GetTestConfigFilePath());
            Console.WriteLine(text);

            Assert.IsTrue(text.Contains(@"<add key=""DebugMode"" value=""DeveloperErrorMessage"" />"));
            Assert.IsTrue(text.Contains(@"<add key=""MaxDisplayListItems"" value=""12"" />"));
            Assert.IsTrue(text.Contains(@"<add key=""SendAdminEmailConfirmations"" value=""True"" />"));
        }
    }
}