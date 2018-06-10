using ChasWare.Common.Utils.Transform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChasWare.Common.Tests.Utils.Transform
{
    [TestClass]
    public class TestTransformations
    {
        /// <summary>
        /// test that we find correct number of classes
        /// </summary>
        [TestMethod]
        public void GetExportedTypes()
        {
            IEnumerable<Type> types = TsTransformTools.GetExportedTypes("ChasWare.Common.Tests");
            Assert.IsNotNull(types);
            Assert.AreEqual(2, types.Count());
            Assert.IsTrue(types.Contains(typeof(TestExportClassA)));
            Assert.IsTrue(types.Contains(typeof(TestExportClassB)));
        }


        /// <summary>
        /// test that we produce the data we expect
        /// </summary>
        [TestMethod]
        public void CompareTestExportClassA()
        {
            const string fileContent = @"export class TestExportClassA {
    StringProperty: string;
    BoolProperty: boolean;
    DateTimeProperty: any;
}";
            IEnumerable<Type> types = TsTransformTools.GetExportedTypes("ChasWare.Common.Tests");
            Type testExportClassA  = types.Single(t => t== typeof(TestExportClassA));
            Assert.IsNotNull(testExportClassA);
            string exported = TsTransformTools.ExtractClass(testExportClassA, types);
            string a = System.Text.RegularExpressions.Regex.Replace(fileContent, @"\s", "");
            string b = System.Text.RegularExpressions.Regex.Replace(exported, @"\s", "");
            Assert.IsTrue(string.Compare(a, b, true) == 0);
        }
    }
}
