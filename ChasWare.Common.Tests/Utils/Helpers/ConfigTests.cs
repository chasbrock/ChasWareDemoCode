// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ConfigTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using ChasWare.Common.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    /// <summary>
    ///     The StrFuncs tests.
    /// </summary>
    [TestClass]
    public class ConfigTests
    {
        #region public methods

        /// <summary>
        ///     The config tests.
        /// </summary>
        [TestMethod]
        public void AppSettingsTests()
        {
            Assert.AreEqual(123.456f, Config.AppSetting("float", 0f));
            Assert.AreEqual(0d, Config.AppSetting("double", 0d));
            Assert.AreEqual(100, Config.AppSetting("int", 0));
            Assert.AreEqual(true, Config.AppSetting("bool", false));
            Assert.AreEqual(true, Config.AppSetting("yes", true));
            Assert.AreEqual("see ya", Config.AppSetting("bye", "see ya"));
            Assert.IsTrue(Config.IsAppSettingAvailable("bool"));
            Assert.IsFalse(Config.IsAppSettingAvailable("yes"));
        }

        #endregion
    }
}