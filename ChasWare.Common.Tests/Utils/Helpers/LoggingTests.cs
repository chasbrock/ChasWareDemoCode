// --------------------------------------------------------------------------------------------------------------------
// <copyright file=LoggingTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using ChasWare.Common.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    [TestClass]
    public class LoggingTests
    {
        [TestMethod]
        public void Initilise()
        {
            try
            {
                Logging.Initialise();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}