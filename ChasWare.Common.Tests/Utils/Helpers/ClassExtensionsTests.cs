// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ClassExtensionsTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using ChasWare.Common.Utils.Helpers;
using NUnit.Framework;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    /// <summary>
    ///     The class extensions tests.
    /// </summary>
    [TestFixture]
    public partial class ClassExtensionsTests
    {
        /// <summary>
        ///     The deep copy.
        /// </summary>
        [TestCase]
        public void DeepCopy()
        {
            ClassA original = ClassExtensions.DeepCreate<ClassA>();
            original.IntVal = 2;
            original.NullIntVal = 2;
            original.DateTime = DateTime.Today;
            original.NullDateTime = DateTime.MaxValue;
            original.IntList.Add(2);
            ((List<string>) original.StringEnumerable).Add("Two");
            ((Dictionary<string, int>) original.StringIntDictionary).Add("Two", 2);
            original.ChangeReadonlyInt(2);
            original.CList.Add(new ClassC(1, original));
            original.CList.Add(new ClassC(2, original));
            original.CList.Add(new ClassC(3, original));

            ClassA copy = original.DeepCopy();

            Assert.IsNotNull(copy);
            Assert.AreEqual(copy.IntVal, 2);
            Assert.AreEqual(copy.NullIntVal, 2);
            Assert.AreEqual(copy.DateTime, DateTime.Today);
            Assert.AreEqual(copy.NullDateTime, DateTime.MaxValue);
            Assert.IsNotNull(copy.IntList);
            Assert.AreEqual(copy.IntList.Count, 1);
            Assert.AreEqual(copy.IntList[0], 2);

            Assert.IsNotNull(copy.StringEnumerable);
            Assert.AreEqual(copy.StringEnumerable.Count(), 1);
            Assert.AreEqual(copy.StringEnumerable.First(), "Two");

            Assert.IsNotNull(copy.CList);
            Assert.AreEqual(copy.CList.Count, 3);
            Assert.AreEqual(copy.CList[0].IntVal, 1);
            Assert.AreEqual(copy.CList[0].Owner, copy);
            Assert.AreEqual(copy.CList[1].IntVal, 2);
            Assert.AreEqual(copy.CList[1].Owner, copy);
            Assert.AreEqual(copy.CList[2].IntVal, 3);
            Assert.AreEqual(copy.CList[2].Owner, copy);

            Assert.IsNotNull(copy.StringIntDictionary);
            Assert.AreEqual(copy.StringIntDictionary.Count, 1);
            Assert.AreEqual(copy.StringIntDictionary["Two"], 2);

            Assert.AreEqual(copy.ReadOnlyInt, -1);

            Assert.IsNotNull(copy.B);
            Assert.IsNotNull(copy.B.ClassA);
            Assert.AreSame(copy.B.ClassA, copy);
            Assert.IsNotNull(copy.B.Owner);
            Assert.AreSame(copy.B.Owner, copy);
            Assert.IsNotNull(copy.B.Parent);
            Assert.AreSame(copy.B.Parent, copy);
            Assert.IsNull(copy.B.TheresaMay);
        }

        /// <summary>
        ///     The deep create.
        /// </summary>
        [TestCase]
        public void DeepCreate()
        {
            ClassA classA = ClassExtensions.DeepCreate<ClassA>();
            Assert.IsNotNull(classA);
            Assert.AreEqual(classA.IntVal, 0);
            Assert.IsNull(classA.NullIntVal);
            Assert.AreEqual(classA.DateTime, DateTime.MinValue);
            Assert.IsNull(classA.NullDateTime);
            Assert.IsNotNull(classA.IntList);
            Assert.IsNotNull(classA.StringEnumerable);
            Assert.IsNotNull(classA.StringIntDictionary);
            Assert.AreEqual(classA.ReadOnlyInt, -1);

            Assert.IsNotNull(classA.B);
            Assert.IsNotNull(classA.B.ClassA);
            Assert.IsNotNull(classA.B.Owner);
            Assert.IsNotNull(classA.B.Parent);
            Assert.IsNull(classA.B.TheresaMay);
        }
    }
}