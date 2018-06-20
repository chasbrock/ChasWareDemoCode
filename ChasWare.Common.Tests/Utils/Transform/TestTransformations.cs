using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string fileContent = CreateDummyDTO("ChasWare.Common.Tests.Utils.Transform", nameof(TestParent));

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
            string fileContent = CreateDummyTS(nameof(TestParent));

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

        private static string CreateDummyDTO(string nameSpace, string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            sb.AppendLine($"namespace {nameSpace}.DTO.{typeName}DTO;");
            sb.AppendLine("{");
            sb.AppendLine("  public int Id { get; set; }");
            sb.AppendLine("  public string ParentName { get; set; }");
            sb.AppendLine("  public string Line1 { get; set; }");
            sb.AppendLine("  public string Line2 { get; set; }");
            sb.AppendLine("  public List<TestChildDTO> Children { get; set; }");
            sb.AppendLine("  public DateTime TimeStamp { get; set; }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string CreateDummyTS(string typeName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"export class {typeName} {{");
            sb.AppendLine("  id: number;");
            sb.AppendLine("  parentName: string;");
            sb.AppendLine("  line1: string;");
            sb.AppendLine("  line2: string;");
            sb.AppendLine("  children: TestChild[] ;");
            sb.AppendLine("  timeStamp: any;");
            sb.AppendLine("}");

            return sb.ToString();
        }

        #endregion
    }
}