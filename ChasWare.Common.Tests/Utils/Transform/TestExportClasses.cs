using System;
using System.Collections.Generic;

namespace ChasWare.Common.Tests.Utils.Transform
{
    [Common.Utils.Transformation.Transformer.Transform]
    public class TestChild
    {
        #region Entity Framework Mapping

        public string ChildName { get; set; }
        public bool IsMale { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }

    [Common.Utils.Transformation.Transformer.Transform]
    public class TestAddress
    {
        #region Entity Framework Mapping

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }


    [Common.Utils.Transformation.Transformer.Transform]
    public class TestParent
    {
        #region Entity Framework Mapping

        public int Id { get; set; }
        public string ParentName { get; set; }
        public TestAddress HomeAddress { get; set; }
        public List<TestChild> Children { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
    }
}