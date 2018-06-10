using ChasWare.Common.Utils.Transform;
using System;


namespace ChasWare.Common.Tests.Utils.Transform
{
    [ExportToTs]
    public class TestExportClassA
    {
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }


    [ExportToTs]
    public class TestExportClassB
    {
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }
}
