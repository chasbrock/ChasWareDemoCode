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
        private List<MemberInfo> _members;
        private Type _target;

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

        /// <summary>
        ///     flatten fields of siblings, then write to .cs file structure
        /// </summary>
        /// <returns></returns>
        public string CreateDTO(Type target)
        {
            AnalyseType(target);
            StringBuilder dto = new StringBuilder();
            dto.AppendLine("using System;");
            dto.AppendLine("using System.Collections.Generic;");
            dto.AppendLine();
            dto.AppendLine($"namespace {_target.Namespace}.DTO.{_target.Name}DTO;");
            dto.AppendLine("{");

            // ReSharper disable once SwitchStatementMissingSomeCases (we are not interested in other member types)
            foreach (MemberInfo info in _members)
            {
                switch (info.MemberType)
                {
                    case MemberTypes.Field:
                        FieldInfo fieldInfo = (FieldInfo) info;
                        string readOnly = fieldInfo.IsInitOnly ? "readonly" : string.Empty;
                        dto.AppendLine($"  public {readOnly} {fieldInfo.FieldType.Name} {info.Name};");
                        break;

                    case MemberTypes.Property:
                        PropertyInfo propertyInfo = (PropertyInfo) info;
                        dto.Append($"  public {propertyInfo.PropertyType.Name} {info.Name} {{");
                        if (propertyInfo.CanRead)
                        {
                            dto.Append(" get; ");
                        }

                        if (propertyInfo.CanWrite)
                        {
                            dto.Append(" set; ");
                        }

                        dto.AppendLine("};");
                        break;
                    default:
                        continue;
                }
            }

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
            sb.AppendLine($"export class {_target.Name} {{");
            foreach (MemberInfo info in _members)
            {
                sb.AppendLine($"  {ToCamelCase(info.Name)}: {ConvertDataTypeToTS(info)};");
            }

            sb.AppendLine("}");
            return sb.ToString();
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
        public void PrintToFile(Type type, string content, string path, string extension)
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(RootPath, path));
            if (!directory.Exists)
            {
                _log.AppendLine($"Creating directory {directory.FullName}");
                directory.Create();
            }

            string outputPath = Path.ChangeExtension(Path.Combine(directory.Name, type.Name), extension);
            File.WriteAllText(outputPath, content);
        }

        #endregion

        #region other methods

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
                       .Where(mi => mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property).ToList();
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

        private void AnalyseFields(Type target, bool checkExisting)
        {
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
                        if (ExportedTypes.Contains(fieldInfo.FieldType))
                        {
                            AnalyseFields(fieldInfo.FieldType, true);
                            continue;
                        }

                        break;

                    case MemberTypes.Property:
                        // short cut field
                        PropertyInfo propertyInfo = (PropertyInfo) info;

                        // see if this is a sibling object 
                        if (ExportedTypes.Contains(propertyInfo.PropertyType))
                        {
                            AnalyseFields(propertyInfo.PropertyType, true);
                            continue;
                        }

                        break;
                    default:
                        continue;
                }
                _members.Add(info);
            }
        }

        /// <summary>
        ///     Analyse the given type, in readiness to export to file format(s)
        /// </summary>
        /// <param name="target"></param>
        private void AnalyseType(Type target)
        {
            if (_target != target)
            {
                _target = target;
                _members = new List<MemberInfo>();
                _existingMembers = GetInterfaceMembers(target);
                AnalyseFields(target, false);
            }
        }


        private string ConvertDataTypeToTS(MemberInfo mi)
        {
            Type t = mi is PropertyInfo info ? info.PropertyType : ((FieldInfo) mi).FieldType;
            return ConvertToTsType(t);
        }

        private string ConvertToTsType(Type t)
        {
            if (t.IsPrimitive)
            {
                if (t == typeof(bool))
                {
                    return "boolean";
                }

                if (t == typeof(char))
                {
                    return "string";
                }

                return "number";
            }

            if (t == typeof(decimal))
            {
                return "number";
            }

            if (t == typeof(string))
            {
                return "string";
            }

            if (t.IsArray)
            {
                Type at = t.GetElementType();
                return ConvertToTsType(at) + "[]";
            }

            if (typeof(IEnumerable).IsAssignableFrom(t))
            {
                Type collectionType = t.GetGenericArguments()[0]; // all my enumerables are typed, so there is a generic argument
                return ConvertToTsType(collectionType) + "[]";
            }

            if (Nullable.GetUnderlyingType(t) != null)
            {
                return ConvertToTsType(Nullable.GetUnderlyingType(t));
            }

            if (t.IsEnum)
            {
                return "number";
            }

            if (ExportedTypes.Contains(t))
            {
                return t.Name;
            }

            return "any";
        }

        #endregion

        #region nested classes

        /// <inheritdoc />
        /// <summary>
        ///     Attribute used to tell t4 transformation to export class
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, Inherited = false)]
        public class Transform : Attribute
        {
        }

        #endregion
    }
}