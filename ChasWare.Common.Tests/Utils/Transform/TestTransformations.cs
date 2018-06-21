using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ChasWare.Common.Utils.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChasWare.Common.Tests.Utils.Transform
{
    [TestClass]
    public class TestTransformations
    {
        #region public methods

        /// <summary>
        ///     test we proppertly flatten a parent / child / sibling heirarchy
        /// </summary>
        [TestMethod]
        public void ExportToDTO()
        {
            // create dummy class
            string fileContent = CreateDummyDTO();

            Transformer transformer = new Transformer("ChasWare.Common.Tests", string.Empty);
            Type parentType = transformer.ExportedTypes.Single(t => t == typeof(TestParent));
            Assert.IsNotNull(parentType);

            string exported = transformer.CreateDTO(parentType);
            string a = Regex.Replace(fileContent, @"\s", "");
            string b = Regex.Replace(exported, @"\s", "");
            Assert.IsTrue(string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0);
        }


        /// <summary>
        ///     test that we produce the data we expect
        /// </summary>
        [TestMethod]
        public void ExportToTS()
        {
            // create dummy class
            string fileContent = CreateDummyTS();

            Transformer transformer = new Transformer("ChasWare.Common.Tests", string.Empty);
            IEnumerable<Type> types = transformer.ExportedTypes;
            Type parentType = types.Single(t => t == typeof(TestParent));
            Assert.IsNotNull(parentType);
            string exported = transformer.CreateTS(parentType);
            string a = Regex.Replace(fileContent, @"\s", "");
            string b = Regex.Replace(exported, @"\s", "");
            Assert.IsTrue(string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        ///     test that we produce the data we expect
        /// </summary>
        [TestMethod]
        public void ExportToTX()
        {
            // create dummy class
            string fileContent = CreateDummyTX();

            Transformer transformer = new Transformer("ChasWare.Common.Tests", string.Empty);
            IEnumerable<Type> types = transformer.ExportedTypes;
            Type parentType = types.Single(t => t == typeof(TestParent));
            Assert.IsNotNull(parentType);
            string exported = transformer.CreateTX(parentType);
            string a = Regex.Replace(fileContent, @"\s", "");
            string b = Regex.Replace(exported, @"\s", "");
            Assert.IsTrue(string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        ///     test that we find correct number of classes
        /// </summary>
        [TestMethod]
        public void GetExportedTypes()
        {
            Transformer transformer = new Transformer("ChasWare.Common.Tests", string.Empty);
            IEnumerable<Type> types = transformer.ExportedTypes;
            Assert.IsNotNull(types);
            Assert.AreEqual(3, types.Count());
            Assert.IsTrue(types.Contains(typeof(TestChild)));
            Assert.IsTrue(types.Contains(typeof(TestParent)));
            Assert.IsTrue(types.Contains(typeof(TestAddress)));
        }

        #endregion

        #region other methods

        private static string CreateDummyDTO()
        {
            return @"
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Common.Tests.Utils.Transform.DTO;
{
  public class TestParentDTO;
{
    public Int32? Id { get;  set; };
    public String ParentName { get;  set; };
    public String Line1 { get;  set; };
    public String Line2 { get;  set; };
    public List<TestChild> Children { get;  set; };
    public DateTime TimeStamp { get;  set; };
    public Double[] Values;
  }
}";
        }

        private static string CreateDummyTX()
        {
            return @"
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.

using System;
using System.Collections.Generic;
using ChasWare.Common.Tests;
using ChasWare.Common.Tests.DTO;

namespace ChasWare.Common.Tests.Utils.Transform.TX;
{
 public static class TestParentTX;
  {
    public static void DTO ReadFromDTO(TestParentDTO source, TestParent target)
    {
       target.Id = source.Id;
       target.ParentName = source.ParentName;
       target.TestAddress.Line1 = source.Line1;
       target.TestAddress.Line2 = source.Line2;
       target.Children = source.Children;
       target.TimeStamp = source.TimeStamp;
       target.Values = source.Values;
       return created;
    }

    public static TestParentDTO WriteToDTO(TestParent source)
    {
       TestParentDTO created = new TestParentDTO();
       created.Id = source.Id;
       created.ParentName = source.ParentName;
       created.Line1 = source.TestAddress.Line1;
       created.Line2 = source.TestAddress.Line2;
       created.Children = source.Select(i => TestChildTX.WriteToDTO(i)).ToArray();
       created.TimeStamp = source.TimeStamp;
       created.Values = source.Select().ToArray();
       return created;
    }

  }
}";
        }


        private static string CreateDummyTS()
        {
            return @"export class TestParent {
  id: number;
  parentName: string;
  line1: string;
  line2: string;
  children: TestChild[];
  timeStamp: any;
  values: number[];
}";
        }

        #endregion
    }
}