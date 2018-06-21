using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChasWare.Common.Utils.Transformation
{
    /// <summary>
    ///     tools to transform C# classes to TypeScript, DTO etx
    /// </summary>
    public class Transformer
    {
        #region Constants and fields 

        private readonly StringBuilder _log = new StringBuilder();
        private List<MemberInfo> _existingMembers;
        private List<Tuple<MemberInfo, MemberInfo>> _members;
        private Type _poco;

        #endregion

        #region Constructors

        /// <summary>
        ///     convert data classes to others
        /// </summary>
        /// <param name="assemblyName">name of assembly holding classes</param>
        /// <param name="rootPath">root path to output to</param>
        public Transformer(string assemblyName, string rootPath)
        {
            RootPath = rootPath;
            _log.AppendLine($"Generating code from {assemblyName} to {rootPath}");
            ExportedTypes = GetExportedTypes(assemblyName);
        }

        #endregion

        #region public properties

        /// <summary>
        ///     lists all the types that were found for the assembly
        /// </summary>
        public IEnumerable<Type> ExportedTypes { get; }

        /// <summary>
        ///     directory where files are to be written to
        /// </summary>
        public string RootPath { get; }

        #endregion

        #region public methods

        public static string DisplayWarning()
        {
            return @"
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison to sync any changes.
";
        }

        /// <summary>
        ///     flatten fields of siblings, then write to .cs file structure
        /// </summary>
        /// <returns></returns>
        public string CreateDTO(Type poco)
        {
            AnalyseType(poco);
            StringBuilder dto = new StringBuilder();
            dto.AppendLine(DisplayWarning());
            dto.AppendLine("using System;");
            dto.AppendLine("using System.Collections.Generic;");
            dto.AppendLine();
            dto.AppendLine($"namespace {_poco.Namespace}.DTO");
            dto.AppendLine("{");
            dto.AppendLine($"  public class {_poco.Name}DTO");
            dto.AppendLine("{");

            // ReSharper disable once SwitchStatementMissingSomeCases (we are not interested in other member types)
            foreach (MemberInfo info in _members.Select(t => t.Item1))
            {
                switch (info.MemberType)
                {
                    case MemberTypes.Field:
                        FieldInfo fieldInfo = (FieldInfo) info;
                        dto.AppendLine($"    public {FormatType(fieldInfo.FieldType)} {info.Name};");
                        break;

                    case MemberTypes.Property:
                        PropertyInfo propertyInfo = (PropertyInfo) info;
                        dto.AppendLine($"    public {FormatType(propertyInfo.PropertyType)} {info.Name} {{ get; set;}}");
                        break;

                    default:
                        continue;
                }
            }

            dto.AppendLine("  }");
            dto.AppendLine("}");
            return dto.ToString();
        }


        /// <summary>
        ///     write type to Ts class format
        /// </summary>
        /// <returns></returns>
        public string CreateTS(Type target)
        {
            AnalyseType(target);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"export class {_poco.Name} {{");
            foreach (MemberInfo info in _members.Select(t => t.Item1))
            {
                sb.AppendLine($"  {ToCamelCase(info.Name)}: {ConvertDataTypeToTS(info)};");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        ///     build class to map between POCO and DTO
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public string CreateTX(Type poco)
        {
            AnalyseType(poco);
            StringBuilder tx = new StringBuilder();
            tx.AppendLine(DisplayWarning());
            tx.AppendLine("using System;");
            tx.AppendLine("using System.Collections.Generic;");
            tx.AppendLine($"using {poco.Namespace};");
            tx.AppendLine($"using {poco.Namespace}.DTO;");

            tx.AppendLine();
            tx.AppendLine($"namespace {_poco.Namespace}.TX");
            tx.AppendLine("{");
            tx.AppendLine($" public static class {poco.Name}TX");
            tx.AppendLine("  {");

            //write method to read data from DTO
            CreateReadFromDTO(tx);

            // write method to write data to DTO
            CreateWriteToDTO(tx);

            tx.AppendLine("  }");
            tx.AppendLine("}");
            return tx.ToString();
        }


        public string ExtractEnums<T>() // Enums<>, since Enum<> is not allowed.
        {
            Type t = typeof(T);
            StringBuilder sb = new StringBuilder();
            int[] values = (int[]) Enum.GetValues(t);
            sb.AppendLine("var " + t.Name + " = {");
            foreach (int val in values)
            {
                string name = Enum.GetName(typeof(T), val);
                sb.AppendLine($"  {name}: {val}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        ///     write out file to named path, using type name
        /// </summary>
        /// <param name="type">type to export</param>
        /// <param name="content">the transforned text</param>
        /// <param name="path">path to export to(appends to target path)</param>
        /// <param name="extension">extension of produced file</param>
        /// <returns>filename used to print file</returns>
        public string PrintToFile(Type type, string content, string path, string extension)
        {
            try
            {
                string directory = Path.Combine(RootPath, path);

                if (!Directory.Exists(directory))
                {
                    _log.AppendLine($"Creating directory {directory}");
                    Directory.CreateDirectory(directory);
                }

                string outputPath = Path.ChangeExtension(Path.Combine(directory, type.Name), extension);
                File.WriteAllText(outputPath, content);
                return outputPath;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        #region other methods

        private static Type GetDataType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo) member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo) member).PropertyType;
                default:
                    return null;
            }
        }

        /// <summary>
        ///     get all classes in assembly that have the ExportToTs attribute
        /// </summary>
        /// <param name="assemblyName">name of assembly to check</param>
        /// <returns>list of types, or empty list</returns>
        private static IEnumerable<Type> GetExportedTypes(string assemblyName)
        {
            Assembly loaded = null;
            foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = ass.GetName().Name;
                if (name == assemblyName)
                {
                    loaded = ass;
                }
            }

            if (loaded != null)
            {
                loaded = Assembly.Load(assemblyName);
            }

            return loaded?.GetTypes().Where(t => t.GetCustomAttributes<Transform>().Any()).ToList();
        }

        private static List<MemberInfo> GetInterfaceMembers(Type type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                       .Where(mi => (mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property) &&
                                    !mi.GetCustomAttributes<Transform>().Any(a => a.Ignore) &&
                                    !IsReadonlyMember(mi)).ToList();
        }

        private static bool IsConflated(MemberInfo info)
        {
            return info.GetCustomAttributes<Transform>().Any(a => a.Conflate);
        }

        private static bool IsReadonlyMember(MemberInfo m)
        {
            return m.MemberType == MemberTypes.Field && ((FieldInfo) m).IsInitOnly ||
                   m.MemberType == MemberTypes.Property && !((PropertyInfo) m).CanWrite;
        }


        private static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (s.Length < 2)
            {
                return s.ToLowerInvariant();
            }

            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        private MemberInfo AnalyseFields(Type target, bool checkExisting)
        {
            // source is used where we are flattening sibling objects
            MemberInfo sibling = target;

            // scan through properties and fields of target type
            // if this is the main class then do not check for existing fields
            foreach (MemberInfo info in GetInterfaceMembers(target))
            {
                // do we already have this field (or is it part of parent
                // and we are going to deal with it later)?
                if (checkExisting && _existingMembers.Any(mi => mi.Name == info.Name))
                {
                    _log.AppendLine($"  field '{info.Name}' ignored as it already exists;");
                    continue;
                }

                // ReSharper disable once SwitchStatementMissingSomeCases (we are not interested in other member types)
                switch (info.MemberType)
                {
                    case MemberTypes.Field:
                        // short cut field
                        FieldInfo fieldInfo = (FieldInfo) info;

                        // see if this is a sibling object 
                        if (ExportedTypes.Contains(fieldInfo.FieldType) && IsConflated(info))
                        {
                            sibling = AnalyseFields(fieldInfo.FieldType, true);
                            continue;
                        }

                        break;

                    case MemberTypes.Property:
                        // short cut field
                        PropertyInfo propertyInfo = (PropertyInfo) info;

                        // see if this is a sibling object 
                        if (ExportedTypes.Contains(propertyInfo.PropertyType) && IsConflated(info))
                        {
                            sibling = AnalyseFields(propertyInfo.PropertyType, true);
                            continue;
                        }

                        break;
                    default:
                        continue;
                }

                _members.Add(new Tuple<MemberInfo, MemberInfo>(info, sibling));
            }

            return sibling;
        }

        /// <summary>
        ///     Analyse the given type, in readiness to export to file format(s)
        /// </summary>
        /// <param name="target"></param>
        private void AnalyseType(Type target)
        {
            if (_poco != target)
            {
                _poco = target;
                _members = new List<Tuple<MemberInfo, MemberInfo>>();
                _existingMembers = GetInterfaceMembers(target);
                AnalyseFields(target, false);
            }
        }

        private string ConvertDataTypeToTS(MemberInfo mi)
        {
            Type t = mi is PropertyInfo info ? info.PropertyType : ((FieldInfo) mi).FieldType;
            return ConvertToTsType(t);
        }

        private string ConvertToDTOType(Type type)
        {
            if (ExportedTypes.Contains(type))
            {
                return type.Name + "DTO";
            }

            return type.Name;
        }

        private string ConvertToTsType(Type t)
        {
            while (true)
            {
                if (t != null && t.IsPrimitive)
                {
                    if (t == typeof(bool))
                    {
                        return "boolean";
                    }

                    return t == typeof(char) ? "string" : "number";
                }

                if (t == typeof(decimal))
                {
                    return "number";
                }

                if (t == typeof(string))
                {
                    return "string";
                }

                if (t != null && t.IsArray)
                {
                    Type at = t.GetElementType();
                    return ConvertToTsType(at) + "[]";
                }

                if (typeof(IEnumerable).IsAssignableFrom(t))
                {
                    if (t != null)
                    {
                        Type collectionType = t.GetGenericArguments()[0]; // all my enumerables are typed, so there is a generic argument
                        return ConvertToTsType(collectionType) + "[]";
                    }
                }

                if (t != null && Nullable.GetUnderlyingType(t) != null)
                {
                    t = Nullable.GetUnderlyingType(t);
                    continue;
                }

                if (t != null && t.IsEnum)
                {
                    return "number";
                }

                if (t != null)
                {
                    return ExportedTypes.Contains(t) ? t.Name : "any";
                }
            }
        }

        private bool CopyArray(MemberInfo member, MemberInfo sibling, StringBuilder tx)
        {
            Type memberType = GetDataType(member);
            if (memberType.IsArray)
            {
                Type elementType = memberType.GetElementType();
                if (elementType != null)
                {
                    // use TX for exported type to deep copy
                    if (ExportedTypes.Contains(elementType))
                    {
                        tx.AppendLine($"       created.{member.Name} = source.{GetSourceObject(member, sibling)}Select(i => {elementType}TX.WriteToDTO(i)).ToArray();");
                    }
                    else
                    {
                        tx.AppendLine($"       created.{member.Name} = source.{GetSourceObject(member, sibling)}Select().ToArray();");
                    }

                    return true;
                }
            }

            if (memberType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(memberType))
            {
                // use TX for exported type to deep copy
                MemberInfo elementType = memberType.GetGenericArguments()[0];
                if (ExportedTypes.Contains(elementType))
                {
                    tx.AppendLine($"       created.{member.Name} = source.{GetSourceObject(member, sibling)}Select(i => {elementType.Name}TX.WriteToDTO(i)).ToArray();");
                }
                else
                {
                    tx.AppendLine($"       created.{member.Name} = source.{GetSourceObject(member, sibling)}Select().ToArray();");
                }

                return true;
            }

            return false;
        }

        private void CreateReadFromDTO(StringBuilder tx)
        {
            tx.AppendLine($"    public static void ReadFromDTO({_poco.Name}DTO source, {_poco.Name} target)");
            tx.AppendLine("    {");

            foreach (Tuple<MemberInfo, MemberInfo> member in _members.Where(m => !IsReadonlyMember(m.Item1)))
            {
                tx.AppendLine($"       target.{GetSourceObject(member.Item1, member.Item2)}{member.Item1.Name} = source.{member.Item1.Name};");
            }


            tx.AppendLine("    }");
            tx.AppendLine("");
        }

        private void CreateWriteToDTO(StringBuilder tx)
        {
            tx.AppendLine($"    public static {_poco.Name}DTO WriteToDTO({_poco.Name} source)");
            tx.AppendLine("    {");
            tx.AppendLine($"       {_poco.Name}DTO created = new {_poco.Name}DTO();");


            foreach (Tuple<MemberInfo, MemberInfo> member in _members)
            {
                if (CopyArray(member.Item1, member.Item2, tx))
                {
                    continue;
                }

                if (!IsReadonlyMember(member.Item1))
                {
                    tx.AppendLine($"       created.{member.Item1.Name} = source.{GetSourceObject(member.Item1, member.Item2)}{member.Item1.Name};");
                }
            }

            tx.AppendLine("       return created;");
            tx.AppendLine("    }");
            tx.AppendLine("");
        }

        private string FormatType(Type t)
        {
            if (t.IsArray)
            {
                Type elementType = t.GetElementType();
                if (elementType != null)
                {
                    return $"{ConvertToDTOType(elementType)}[]";
                }
            }

            // wrinkle with string.. it thinks it is enumerable, the foolish thing
            if (t != typeof(string) && typeof(IEnumerable).IsAssignableFrom(t))
            {
                string enumerableTypeName = t.Name.Substring(0, t.Name.IndexOf("`", StringComparison.Ordinal));
                return $"{enumerableTypeName}<{ConvertToDTOType(t.GetGenericArguments()[0])}>";
            }

            if (Nullable.GetUnderlyingType(t) != null)
            {
                return $"{Nullable.GetUnderlyingType(t)?.Name}?";
            }

            return ConvertToDTOType(t);
        }


        private string GetSourceObject(MemberInfo memberInfo, MemberInfo sibling)
        {
            return memberInfo.DeclaringType != _poco ? $"{sibling.Name}." : "";
        }

        #endregion

        #region nested classes

        /// <inheritdoc />
        /// <summary>
        ///     Attribute used to tell t4 transformation to export class
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
        public class Transform : Attribute
        {
            #region public properties

            /// <summary>
            ///     should this class be exported
            /// </summary>
            public bool Ignore { get; set; } = true;

            /// <summary>
            ///     will copy unmatched fields from siblings
            /// </summary>
            public bool Conflate { get; set; }

            #endregion
        }

        #endregion
    }
}