using System;
using System.Collections.Generic;
using ChasWare.Common.Utils.Transformation;

namespace ChasWare.Common.Tests.Utils.Transform
{
    [Transformer.Transform]
    public class TestChild
    {
        #region Entity Framework Mapping

        public string ChildName { get; set; }
        public bool IsMale { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }

    [Transformer.Transform]
    public class TestAddress
    {
        #region Entity Framework Mapping

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }

    [Transformer.Transform]
    public class TestParent
    {
        #region Entity Framework Mapping

        public int? Id { get; set; }
        public double[] Values;
        public string ParentName { get; set; }

        [Transformer.Transform(Conflate = true)]
        public TestAddress HomeAddress { get; set; }

        [Transformer.Transform(Conflate = false)]
        public TestAddress WorkAddress { get; set; }

        public List<TestChild> Children { get; set; }
        public DateTime TimeStamp { get; set; }

        [Transformer.Transform(Ignore = true)]
        public TestAddress PrivateAddress { get; set; }

        public readonly int ReadOnly1;
        public int ReadOnly { get; } 

        #endregion
    }
}