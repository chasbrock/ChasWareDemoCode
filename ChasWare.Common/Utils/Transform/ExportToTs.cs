using System;

namespace ChasWare.Common.Utils.Transform
{
    /// <summary>
    /// Attribute used to tell t4 transformation to export class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExportToTs : Attribute
    {

    }
}
