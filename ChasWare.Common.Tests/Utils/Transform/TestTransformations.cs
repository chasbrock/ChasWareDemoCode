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
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;

namespace ChasWare.Common.Tests.Utils.Transform.DTO
{
  public class TestParentDTO
{
    public int? Id { get; set;}
    public string ParentName { get; set;}
    public string Line1 { get; set;}
    public string Line2 { get; set;}
    public TestAddressDTO WorkAddress { get; set;}
    public List<TestChildDTO> Children { get; set;}
    public DateTime TimeStamp { get; set;}
    public double[] Values;
  }
}
";
        }

        private static string CreateDummyTS()
        {
            return @"export class TestParent {
  id: number;
  parentName: string;
  line1: string;
  line2: string;
  workAddress: TestAddress;
  children: TestChild[];
  timeStamp: any;
  values: number[];
}";
        }

        private static string CreateDummyTX()
        {
            return @"
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.

using System;
using System.Collections.Generic;
using ChasWare.Common.Tests.Utils.Transform;
using ChasWare.Common.Tests.Utils.Transform.DTO;

namespace ChasWare.Common.Tests.Utils.Transform.TX
{
 public static class TestParentTX
  {
    public static void ReadFromDTO(TestParentDTO source, TestParent target)
    {
       target.Id = source.Id;
       target.ParentName = source.ParentName;
       target.HomeAddress.Line1 = source.Line1;
       target.HomeAddress.Line2 = source.Line2;
       TestAddressTX.WriteToDTO(target.WorkAddress, source.WorkAddress);
       target.Children = source.Children;
       target.TimeStamp = source.TimeStamp;
       target.Values = source.Values;
    }

    public static TestParentDTO WriteToDTO(TestParent source)
    {
       return  new TestParentDTO
       {
         Id = source.Id,
         ParentName = source.ParentName,
         Line1 = source.HomeAddress.Line1,
         Line2 = source.HomeAddress.Line2,
         WorkAddress = TestAddressTX.WriteToDTO(source.WorkAddress),
         Children = source.Select(i => TestChildTX.WriteToDTO(i)).ToArray(),
         TimeStamp = source.TimeStamp,
         Values = source.Select().ToArray(),
       };
    }

  }
}";
        }

        #endregion
    }
}