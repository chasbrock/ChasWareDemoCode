// --------------------------------------------------------------------------------------------------------------------
// <copyright file=LoggingTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using ChasWare.Common.Utils.Helpers;
using NUnit.Framework;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    [TestFixture]
    public class LoggingTests
    {
        [Test]
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