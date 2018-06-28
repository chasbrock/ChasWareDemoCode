using System;
using System.Collections.Generic;
using System.IO;
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
            File.WriteAllText(@"d:\Logs\ExportToDTO.dto.cs", exported); 
            string a = Regex.Replace(fileContent, @"\s", "");
            string b = Regex.Replace(exported, @"\s", "");
            Assert.IsTrue(string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        ///     test we proppertly flatten a parent / child / sibling heirarchy
        /// </summary>
        [TestMethod]
        public void ExportToController()
        {
            // create dummy class
            string fileContent = CreateDummyDTO();

            Transformer transformer = new Transformer("ChasWare.Common.Tests", string.Empty);
            Type parentType = transformer.ExportedTypes.Single(t => t == typeof(TestParent));
            Assert.IsNotNull(parentType);

            string exported = transformer.CreateController("ChasWare.DataService.Controllers", parentType);
            File.WriteAllText(@"d:\Logs\ExportToController.cs", exported); 
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
            File.WriteAllText(@"d:\Logs\ExportToTS.TS", exported); 
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
            File.WriteAllText(@"d:\Logs\ExportToTX.tx.cs", exported); 
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
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;

namespace ChasWare.Common.Tests.Utils.Transform.DTO
{
    public class TestParentDTO
    {
        public int? Id { get; set;}
        public string ParentName { get; set;}
        public int SpuriousId { get; set;}
        public int AddressId { get; set;}
        public string Line1 { get; set;}
        public string Line2 { get; set;}
        public List<TestAddressDTO> AddressList { get; set;}
        public TestAddressDTO WorkAddress { get; set;}
        public List<TestChildDTO> Children { get; set;}
        public DateTime TimeStamp { get; set;}
        public double[] Values;
    }
}";
        }

        private static string CreateDummyTS()
        {
            return @"export class TestParent {
    id: number;
    parentName: string;
    spuriousId: number;
    addressId: number;
    line1: string;
    line2: string;
    addressList: TestAddress[];
    workAddress: TestAddress;
    children: TestChild[];
    timeStamp: any;
    values: number[];
}";
        }

        private static string CreateDummyTX()
        {
            return @"// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018

using System;
using System.Collections.Generic;
using System.Linq;
using ChasWare.Common.Tests.Utils.Transform;
using ChasWare.Common.Tests.Utils.Transform.DTO;

namespace ChasWare.Common.Tests.Utils.Transform.TX
{
    public static class TestParentTX
    {
        public static void ReadFromDTO(TestParent target, TestParentDTO source)
        {
            target.SpuriousId = source.SpuriousId;
            target.Id = source.Id;
            target.ParentName = source.ParentName;
            target.HomeAddress.AddressId = source.AddressId;
            target.HomeAddress.Line1 = source.Line1;
            target.HomeAddress.Line2 = source.Line2;
            ReadAddressListFromDTO(target.AddressList, source.AddressList);
            TestAddressTX.ReadFromDTO(target.WorkAddress, source.WorkAddress);
            ReadChildrenFromDTO(target.Children, source.Children);
            target.TimeStamp = source.TimeStamp;
            target.Values = source.Values.Select().ToArray(),
        }

        public static ReadAddressListFromDTO(TestAddress target, TestAddressDTO source)
        {
            List<TestAddress> existing = target.AddressList.ToList();
            foreach (TestAddressDTO item in source.AddressList)
            {
                TestAddress found = target.AddressList.FirstOrDefault(t => TestAddressTX.Compare(t, item) == 0);
                if (found != null)
                    TestAddressTX.ReadFromDTO(found, item);
                    existing.Remove(found);
                    continue;
                }
                 target.AddressList.Add(TestAddressTX.ReadFromDTO(new TestAddress(), item));
            }
            foreach(TestAddress deleted in existing)
            {
                target.AddressList.Remove(deleted);
            }
        }

        public static ReadChildrenFromDTO(TestChild target, TestChildDTO source)
        {
            List<TestChild> existing = target.Children.ToList();
            foreach (TestChildDTO item in source.Children)
            {
                TestChild found = target.Children.FirstOrDefault(t => TestChildTX.Compare(t, item) == 0);
                if (found != null)
                    TestChildTX.ReadFromDTO(found, item);
                    existing.Remove(found);
                    continue;
                }
                 target.Children.Add(TestChildTX.ReadFromDTO(new TestChild(), item));
            }
            foreach(TestChild deleted in existing)
            {
                target.Children.Remove(deleted);
            }
        }

        public static TestParentDTO WriteToDTO(TestParent source)
        {
            return new TestParentDTO
                {
                    SpuriousId = source.SpuriousId,
                    Id = source.Id,
                    ParentName = source.ParentName,
                    AddressId = source.HomeAddress.AddressId,
                    Line1 = source.HomeAddress.Line1,
                    Line2 = source.HomeAddress.Line2,
                    AddressList = source.AddressList.Select(TestAddressTX.WriteToDTO).ToArray(),
                    WorkAddress = WorkAddressTX.WriteToDTO(source.WorkAddress),
                    Children = source.Children.Select(TestChildTX.WriteToDTO).ToArray(),
                    TimeStamp = source.TimeStamp,
                    Values = source.Values.Select().ToArray(),
                };
        }

        public static int Compare(TestParentDTO lhs, TestParent rhs)
        {
            return Compare(rhs, lhs) * -1;
        }

        public static int Compare(TestParent lhs, TestParentDTO rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return -1;
            }

            if (ReferenceEquals(rhs, null))
            {
                return 1;
            }

            int result = 0;
            result = lhs.Id.CompareTo(lhs.Id);
            if (result != 0)
            {
                 return result;
            }

            return lhs.SpuriousId.CompareTo(lhs.SpuriousId);
        }

    }
}";
        }

        #endregion
    }
}