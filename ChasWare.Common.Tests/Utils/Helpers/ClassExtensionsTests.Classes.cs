// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ClassExtensionsTests.Classes.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ChasWare.Common.Tests.Utils.Helpers
{
    /// <summary>
    ///     note - do not remove  Entity Framework Mapping region
    ///     because it stops resharper removeing "unused setters"
    /// </summary>
    public partial class ClassExtensionsTests
    {
        private class ClassA
        {
            #region Constants and fields 

            private readonly int[] _value = new int[5];

            #endregion

            #region public properties

            /// <summary>
            ///     Gets or sets the b.
            /// </summary>
            public ClassB B { get; set; }

            /// <summary>
            ///     Gets or sets the c list.
            /// </summary>
            public List<ClassC> CList { get; set; }

            /// <summary>
            ///     Gets or sets the date time.
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            ///     Gets or sets the int list.
            /// </summary>
            public List<int> IntList { get; set; }

            /// <summary>
            ///     Gets or sets the int val.
            /// </summary>
            public int IntVal { get; set; }

            /// <summary>
            ///     Gets or sets the null date time.
            /// </summary>
            public DateTime? NullDateTime { get; set; }

            /// <summary>
            ///     Gets or sets the null int val.
            /// </summary>
            public int? NullIntVal { get; set; }

            /// <summary>
            ///     Gets the read only int.
            /// </summary>
            public int ReadOnlyInt { get; private set; } = -1;

            /// <summary>
            ///     Gets or sets the string enumerable.
            /// </summary>
            public IEnumerable<string> StringEnumerable { get; set; }

            /// <summary>
            ///     Gets or sets the string int dictionary.
            /// </summary>
            public IDictionary<string, int> StringIntDictionary { get; set; }

            #endregion

            #region indexers

            /// <summary>
            ///     indexer - to throw spanner in works
            /// </summary>
            /// <param name="index">
            ///     The index.
            /// </param>
            /// <returns>
            ///     the value <see cref="int" />.
            /// </returns>
            public int this[int index]
            {
                get => _value[index];
                set => _value[index] = value;
            }

            #endregion

            #region public methods

            /// <summary>
            ///     The change readonly int.
            /// </summary>
            /// <param name="i">
            ///     the new value.
            /// </param>
            public void ChangeReadonlyInt(int i)
            {
                ReadOnlyInt = i;
            }

            #endregion
        }

        private class ClassB
        {
            #region public properties

            /// <summary>
            ///     Gets or sets the class a.
            /// </summary>
            public ClassA ClassA { get; set; }

            /// <summary>
            ///     Gets or sets the owner.
            /// </summary>
            public ClassA Owner { get; set; }

            /// <summary>
            ///     Gets or sets the parent.
            /// </summary>
            public ClassA Parent { get; set; }

            /// <summary>
            ///     Gets or sets the theresa may.
            /// </summary>
            public ClassA TheresaMay { get; set; }

            #endregion
        }

        private class ClassC
        {
            #region Constructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="ClassC" /> class.
            /// </summary>
            public ClassC()
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ClassC" /> class.
            /// </summary>
            /// <param name="i">
            ///     an int value
            /// </param>
            /// <param name="owner">
            ///     The owner.
            /// </param>
            public ClassC(int i, ClassA owner)
            {
                IntVal = i;
                Owner = owner;
            }

            #endregion

            #region public properties

            /// <summary>
            ///     Gets the int val.
            /// </summary>
            public int IntVal { get; }

            /// <summary>
            ///     Gets the owner.
            /// </summary>
            public ClassA Owner { get; }

            #endregion
        }
    }
}