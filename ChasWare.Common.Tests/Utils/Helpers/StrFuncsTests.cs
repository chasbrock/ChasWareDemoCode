// --------------------------------------------------------------------------------------------------------------------
// <copyright file=StrFuncsTests.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using ChasWare.Common.Utils.Helpers;
using NUnit.Framework;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    /// <summary>
    ///     The StrFuncs tests.
    /// </summary>
    [TestFixture]
    public class StrFuncsTests
    {
        /// <summary>
        ///     Test the contains method
        /// </summary>
        [Test]
        public void ContainsTest()
        {
            const string toFind = "Blah";
            const string atMiddle = "MehBlahPhrmpf";
            const string atEnd = "MehPhrmpfBlah";
            const string atStart = "BlahMehPhrpf";
            const string notThere = "I'mALittleTeaPot";

            Assert.IsTrue(atStart.CaselessContains(toFind));
            Assert.IsTrue(atMiddle.CaselessContains(toFind));
            Assert.IsTrue(atEnd.CaselessContains(toFind));
            Assert.IsTrue(atStart.CaselessContains(toFind.ToUpper()));
            Assert.IsTrue(atStart.ToUpper().CaselessContains(toFind));
            Assert.IsFalse(notThere.Contains(toFind));
            Assert.IsFalse(atMiddle.CaselessContains(null));
            Assert.IsFalse(StrFuncs.CaselessContains(null, toFind));
        }

        /// <summary>
        ///     test StrFuncs.IsOneOf
        /// </summary>
        [Test]
        public void TestIsOneOf()
        {
            string[] choices =
                {
                    "one",
                    "two",
                    "Three"
                };

            Assert.IsTrue("one".IsOneOf(choices));
            Assert.IsTrue("Three".IsOneOf(choices));
            Assert.IsFalse("banananana".IsOneOf(choices));
            Assert.IsFalse("one".IsOneOf(null));
            Assert.IsFalse("one".IsOneOf());
            Assert.IsFalse(StrFuncs.IsOneOf(null, choices));
        }

        /// <summary>
        ///     test StrFuncs.IsSubstringOfAny
        /// </summary>
        [Test]
        public void TestIsSubstringOfAny()
        {
            string[] choices =
                {
                    "twentyone",
                    "TwentyTwo",
                    "Veintitres"
                };

            Assert.IsTrue("one".IsOneOf("twentyone", "TwentyTwo", "Veintitres"));
            Assert.IsTrue("Two".IsOneOf(choices));
            Assert.IsTrue("Veintitres".IsOneOf(choices));
            Assert.IsFalse("banananana".IsOneOf(choices));
            Assert.IsFalse("one".IsOneOf(null));
            Assert.IsFalse("one".IsOneOf());
            Assert.IsFalse(StrFuncs.IsOneOf(null, choices));
        }

        /// <summary>
        ///     test StrFuncs.PackOutStrings
        /// </summary>
        [Test]
        public void TestPackOutStrings()
        {
            string[] values =
                {
                    "Mary",
                    "had",
                    "a",
                    "little",
                    "bit",
                    "on",
                    "the",
                    "side"
                };
            const string message = "Mary had a little bit on the side";
            Assert.AreEqual(StrFuncs.PackOutStrings(values), message);
            Assert.AreNotEqual(StrFuncs.PackOutStrings(values), "Message");
            Assert.AreEqual(StrFuncs.PackOutStrings(null), string.Empty);
            Assert.AreEqual(StrFuncs.PackOutStrings(), string.Empty);
        }

        /// <summary>
        ///     test StrFuncs.PadAndWrap
        /// </summary>
        [Test]
        public void TestPadAndWrap()
        {
            const string boon = "Boon";
            const string doggle = "Doggle";
            const string wrapping = "|||";
            string expected = $"{wrapping} {boon} {doggle}              {wrapping}";
            string created = StrFuncs.PadAndWrap(expected.Length, wrapping, boon, doggle);
            Assert.AreEqual(expected, created);
        }

        /// <summary>
        ///     test StrFuncs.SubstringUpTo
        /// </summary>
        [Test]
        public void TestSubstringUpTo()
        {
            const string source = "The_Staunton:Lick";
            Assert.AreEqual(source.SubstringUpTo('_'), "The");
            Assert.AreEqual(source.SubstringUpTo(':'), "The_Staunton");
            Assert.AreEqual(source.SubstringUpTo('*'), source);
            Assert.AreEqual(source.SubstringUpTo('\0'), source);
            Assert.AreEqual(StrFuncs.SubstringUpTo(null, '*'), null);
        }
    }
}