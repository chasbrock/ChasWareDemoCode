using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ChasWare.Common.Utils.Helpers;

namespace ChasWare.Common.Utils.Transformation
{
    /// <summary>
    ///     tools to transform C# classes to TypeScript, DTO etx
    /// </summary>
    public class Transformer
    {
        #region Constants and fields 

        /// <summary>
        ///     Warning text displayed at top of files
        /// </summary>
        /// <value></value>
        public const string DisplayWarning = @"
// WARNING: this code is auto generated and should not be modified.
// hint:    if you need to modify it, let it build into a non-project directory
//          then use a text comparison tool to sync any changes.
// (c) Chas.Brock@gmail.com 2018
";

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
            Log.AppendLine($"Generating code from {assemblyName} to {rootPath}");
            ExportedTypes = GetExportedTypes(assemblyName);
        }

        #endregion

        #region public properties

        /// <summary>
        ///     lists all the types that were found for the assembly
        /// </summary>
        public IEnumerable<Type> ExportedTypes { get; }

        /// <summary>
        ///     Gets a log of any problems or issues
        /// </summary>
        public StringBuilder Log { get; } = new StringBuilder();

        /// <summary>
        ///     directory where files are to be written to
        /// </summary>
        public string RootPath { get; }

        #endregion

        #region public methods

        public string CreateController(string nameSpace, Type poco)
        {
            try
            {
                AnalyseType(poco);
                StringBuilder code = GenerateControllerHeader(nameSpace);

                // create actions action
                GenerateDeleteAction(code);
                GenerateGetAction(code);
                GeneratePostAction(code);
                GeneratePutAction(code);

                // destructor etc
                GenerateValueExists(code);
                GenerateControllerCleanup(code);
                return code.ToString();
            }
            catch (Exception ex)
            {
                Log.AppendLine($"Error Creating DTO for {poco.FullName}");
                Log.AppendLine(ex.ToString());
            }

            return null;
        }

        /// <summary>
        ///     flatten fields of siblings, then write to .cs file structure
        /// </summary>
        /// <returns></returns>
        public string CreateDTO(Type poco)
        {
            try
            {
                AnalyseType(poco);
                StringBuilder dto = new StringBuilder();
                dto.AppendLine(DisplayWarning);
                dto.AppendLine("using System;");
                dto.AppendLine("using System.Collections.Generic;");
                dto.AppendLine();
                dto.AppendLine($"namespace {_poco.Namespace}.DTO");
                dto.AppendLine("{");
                dto.IndentLine(1, $"public class {_poco.Name}DTO");
                dto.IndentLine(1, "{");

                // ReSharper disable once SwitchStatementMissingSomeCases (we are not interested in other member types)
                foreach (MemberInfo info in _members.Select(t => t.Item1))
                {
                    switch (info.MemberType)
                    {
                        case MemberTypes.Field:
                            FieldInfo fieldInfo = (FieldInfo) info;
                            dto.IndentLine(2, $"public {FormatType(fieldInfo.FieldType)} {info.Name};");
                            break;

                        case MemberTypes.Property:
                            PropertyInfo propertyInfo = (PropertyInfo) info;
                            dto.IndentLine(2, $"public {FormatType(propertyInfo.PropertyType)} {info.Name} {{ get; set;}}");
                            break;

                        default:
                            continue;
                    }
                }

                dto.IndentLine(1, "}");
                dto.AppendLine("}");
                return dto.ToString();
            }
            catch (Exception ex)
            {
                Log.AppendLine($"Error Creating DTO for {poco.FullName}");
                Log.AppendLine(ex.ToString());
            }

            return null;
        }

        /// <summary>
        ///     write type to Ts class format
        /// </summary>
        /// <returns></returns>
        public string CreateTS(Type poco)
        {
            try
            {
                AnalyseType(poco);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"export class {_poco.Name} {{");
                foreach (MemberInfo info in _members.Select(t => t.Item1))
                {
                    sb.IndentLine(1, $"{ToCamelCase(info.Name)}: {ConvertMemberToTS(info)};");
                }

                sb.AppendLine("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log.AppendLine($"Error Creating TS for {poco.FullName}");
                Log.AppendLine(ex.ToString());
            }

            return null;
        }

        /// <summary>
        ///     build class to map between POCO and DTO
        /// </summary>
        /// <param name="poco"></param>
        /// <returns></returns>
        public string CreateTX(Type poco)
        {
            try
            {
                AnalyseType(poco);
                StringBuilder code = new StringBuilder();
                code.AppendLine(DisplayWarning);
                code.AppendLine("using System;");
                code.AppendLine("using System.Collections.Generic;");
                code.AppendLine("using System.Linq;");
                code.AppendLine($"using {poco.Namespace};");
                code.AppendLine($"using {poco.Namespace}.DTO;");

                code.AppendLine();
                code.AppendLine($"namespace {_poco.Namespace}.TX");
                code.AppendLine("{");
                code.IndentLine(1, $"public static class {poco.Name}TX");
                code.IndentLine(1, "{");

                //write method to read data from DTO
                GenerateReadFromDTO(code);

                // write method to write data to DTO
                GenerateWriteToDTO(code);

                // write methods to look poco in collections
                GenerateCompareMethod(code);

                code.IndentLine(1, "}");
                code.AppendLine("}");
                return code.ToString();
            }
            catch (Exception ex)
            {
                Log.AppendLine($"Error Creating TX for {poco.FullName}");
                Log.AppendLine(ex.ToString());
            }

            return null;
        }

        /// <summary>
        ///     write out file to named path, using type name
        /// </summary>
        /// <param name="type">type to export</param>
        /// <param name="content">the transforned text</param>
        /// <param name="path">path to export to(appends to target path)</param>
        /// <param name="extension">extension of produced file (must include "." because we may want to change name of file) </param>
        /// <returns>filename used to print file</returns>
        public string PrintToFile(Type type, string content, string path, string extension)
        {
            try
            {
                string directory = Path.Combine(RootPath, path);

                if (!Directory.Exists(directory))
                {
                    Log.AppendLine($"Creating directory {directory}");
                    Directory.CreateDirectory(directory);
                }

                string outputPath = Path.Combine(directory, type.Name) + extension;
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

        private static IList<MemberInfo> AnalyseKeyFields(Type type)
        {
            SortedList<int, MemberInfo> keys = new SortedList<int, MemberInfo>();
            foreach (MemberInfo member in type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                                              .Where(mi => mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property))
            {
                // is this a key field?
                if (IsKeyField(member, out int index))
                {
                    keys.Add(index, member);
                }
            }

            return keys.Values;
        }

        private static string CleanTypeName(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return $"{CleanTypeName(Nullable.GetUnderlyingType(type))}?";
            }

            switch (type.Name)
            {
                case "String":
                    return "string";
                case "Boolean":
                    return "bool";
                case "Int16":
                    return "short";
                case "Int32":
                    return "int";
                case "UInt32":
                    return "uint";
                case "Int64":
                    return "long";
                case "UInt64":
                    return "ulong";
                default:
                    if (type.IsPrimitive)
                    {
                        return ToCamelCase(type.Name);
                    }

                    return type.Name;
            }
        }

        private static string ConflateType(MemberInfo sibling)
        {
            return sibling != null ? sibling.Name + "." : "";
        }

        private static void GenerateCopyCollection(StringBuilder code, MemberInfo member)
        {
            MemberInfo elementType = GetDataType(member).GetGenericArguments()[0];

            code.IndentLine(2, $"public static void Read{member.Name}FromDTO(ICollection<{elementType.Name}> target, IEnumerable<{elementType.Name}DTO> source)");
            code.IndentLine(2, "{");
            code.IndentLine(3, $"List<{elementType.Name}> existing = target.ToList();");
            code.IndentLine(3, $"foreach ({elementType.Name}DTO item in source)");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"{elementType.Name} found = target.FirstOrDefault(t => {elementType.Name}TX.Compare(t, item) == 0);");
            code.IndentLine(4, "if (found != null)");
            code.IndentLine(4, "{");
            code.IndentLine(5, $"{elementType.Name}TX.ReadFromDTO(found, item);");
            code.IndentLine(5, "existing.Remove(found);");
            code.IndentLine(5, "continue;");
            code.IndentLine(4, "}");
            code.IndentLine(4, $" target.Add({elementType.Name}TX.ReadFromDTO(new {elementType.Name}(), item));");
            code.IndentLine(3, "}");
            code.IndentLine(3, $"foreach({elementType.Name} deleted in existing)");
            code.IndentLine(3, "{");
            code.IndentLine(4, "target.Remove(deleted);");
            code.IndentLine(3, "}");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

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
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = assembly.GetName().Name;
                if (name == assemblyName)
                {
                    loaded = assembly;
                }
            }

            if (loaded != null)
            {
                loaded = Assembly.Load(assemblyName);
            }

            return loaded?.GetTypes().Where(t => t.GetCustomAttributes<Transform>().Any()).ToList();
        }

        /// <summary>
        ///     extract all non-readonly fields and properties. als check Ignore attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<MemberInfo> GetMembers(IReflect type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                       .Where(mi => (mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property) &&
                                    !mi.GetCustomAttributes<Transform>().Any(a => a.Ignore) &&
                                    !IsReadonlyMember(mi)).ToList();
        }

        /// <summary>
        ///     have we been told to conflate this sibling?
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private static bool IsConflated(MemberInfo member)
        {
            return member.GetCustomAttributes<Transform>().Any(a => a.Conflate);
        }

        private static bool IsKeyField(MemberInfo member, out int index)
        {
            index = 0;
            if (member.GetCustomAttributes<KeyAttribute>().Any())
            {
                ColumnAttribute column = member.GetCustomAttributes<ColumnAttribute>().FirstOrDefault();
                if (column != null)
                {
                    index = column.Order;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     check to see if this member is readonly
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private static bool IsReadonlyMember(MemberInfo member)
        {
            return member.MemberType == MemberTypes.Field && ((FieldInfo) member).IsInitOnly ||
                   member.MemberType == MemberTypes.Property && !((PropertyInfo) member).CanWrite;
        }

        /// <summary>
        ///     convert to TS styled camelcase (lower first letter)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ToCamelCase(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            if (text.Length < 2)
            {
                return text.ToLowerInvariant();
            }

            return char.ToLowerInvariant(text[0]) + text.Substring(1);
        }

        /// <summary>
        ///     read through members and build a list, conflating as appropriate
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sibling"></param>
        private void AnalyseFields(Type target, MemberInfo sibling)
        {
            // scan through properties and fields of target type
            // if this is the main class then do not check for existing fields
            foreach (MemberInfo member in GetMembers(target))
            {
                // do we already have this field (or is it part of parent
                // and we are going to deal with it later)?
                if (sibling != null && _existingMembers.Any(mi => mi.Name == member.Name))
                {
                    Log.AppendLine($"  field '{member.Name}' ignored as it already exists;");
                    continue;
                }

                // ReSharper disable once SwitchStatementMissingSomeCases (we are not interested in other member types)
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        // short cut field
                        FieldInfo fieldInfo = (FieldInfo) member;

                        // see if this is a sibling object 
                        if (ExportedTypes.Contains(fieldInfo.FieldType) && IsConflated(member))
                        {
                            AnalyseFields(fieldInfo.FieldType, member);
                            continue;
                        }

                        break;

                    case MemberTypes.Property:
                        // short cut field
                        PropertyInfo propertyInfo = (PropertyInfo) member;

                        // see if this is a sibling object 
                        if (ExportedTypes.Contains(propertyInfo.PropertyType) && IsConflated(member))
                        {
                            AnalyseFields(propertyInfo.PropertyType, member);
                            continue;
                        }

                        break;
                    default:
                        continue;
                }

                _members.Add(new Tuple<MemberInfo, MemberInfo>(member, sibling));
            }
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
                _existingMembers = GetMembers(target);
                AnalyseFields(target, null);
            }
        }

        /// <summary>
        ///     convert c# member type to TS
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private string ConvertMemberToTS(MemberInfo member)
        {
            Type t = member is PropertyInfo info ? info.PropertyType : ((FieldInfo) member).FieldType;
            return ConvertToTsType(t);
        }

        /// <summary>
        ///     convert existing class reference to equiv DTO for non conflated members
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ConvertToDTOType(Type type)
        {
            if (ExportedTypes.Contains(type))
            {
                return type.Name + "DTO";
            }

            return CleanTypeName(type);
        }

        /// <summary>
        ///     convert c# type to TS
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ConvertToTsType(Type type)
        {
            while (true)
            {
                if (type != null && type.IsPrimitive)
                {
                    if (type == typeof(bool))
                    {
                        return "boolean";
                    }

                    return type == typeof(char) ? "string" : "number";
                }

                if (type == typeof(decimal))
                {
                    return "number";
                }

                if (type == typeof(string))
                {
                    return "string";
                }

                if (type != null && type.IsArray)
                {
                    Type at = type.GetElementType();
                    return ConvertToTsType(at) + "[]";
                }

                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    if (type != null)
                    {
                        Type collectionType = type.GetGenericArguments()[0]; // all my enumerables are typed, so there is a generic argument
                        return ConvertToTsType(collectionType) + "[]";
                    }
                }

                if (type != null && Nullable.GetUnderlyingType(type) != null)
                {
                    type = Nullable.GetUnderlyingType(type);
                    continue;
                }

                if (type != null && type.IsEnum)
                {
                    return "number";
                }

                if (type != null)
                {
                    return ExportedTypes.Contains(type) ? type.Name : "any";
                }
            }
        }

        /// <summary>
        ///     change Type to new Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string FormatType(Type type)
        {
            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                if (elementType != null)
                {
                    return $"{ConvertToDTOType(elementType)}[]";
                }
            }

            // wrinkle with string.. it thinks it is enumerable, the foolish thing
            if (type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type))
            {
                string enumerableTypeName = type.Name.Substring(0, type.Name.IndexOf("`", StringComparison.Ordinal));
                return $"{enumerableTypeName}<{ConvertToDTOType(type.GetGenericArguments()[0])}>";
            }

            return ConvertToDTOType(type);
        }

        /// <summary>
        ///     write methods to compare items
        /// </summary>
        /// <param name="code"></param>
        private void GenerateCompareMethod(StringBuilder code)
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (!keyFields.Any())
            {
                return;
            }

            // add reverse compare
            code.IndentLine(2, $"public static int Compare({_poco.Name}DTO lhs, {_poco.Name} rhs)");
            code.IndentLine(2, "{");
            code.IndentLine(3, "return Compare(rhs, lhs) * -1;");
            code.IndentLine(2, "}");
            code.AppendLine();

            // deal with nulls
            code.IndentLine(2, $"public static int Compare({_poco.Name} lhs, {_poco.Name}DTO rhs)");
            code.IndentLine(2, "{");
            code.IndentLine(3, "if (ReferenceEquals(lhs, null))");
            code.IndentLine(3, "{");
            code.IndentLine(4, "return -1;");
            code.IndentLine(3, "}");
            code.AppendLine();
            code.IndentLine(3, "if (ReferenceEquals(rhs, null))");
            code.IndentLine(3, "{");
            code.IndentLine(4, "return 1;");
            code.IndentLine(3, "}");
            code.AppendLine();
            if (keyFields.Count > 1)
            {
                code.IndentLine(3, "int result = 0;");
            }

            // deal withkeys in order
            for (int i = 0; i < keyFields.Count; i++)
            {
                MemberInfo item = keyFields[i];
                if (i == keyFields.Count - 1)
                {
                    code.IndentLine(3, $"return lhs.{item.Name}.CompareTo(lhs.{item.Name});");
                    continue;
                }

                code.IndentLine(3, $"result = lhs.{item.Name}.CompareTo(lhs.{item.Name});");
                code.IndentLine(3, "if (result != 0)");
                code.IndentLine(3, "{");
                code.IndentLine(4, " return result;");
                code.IndentLine(3, "}");
                code.AppendLine();
            }

            code.IndentLine(2, "}");
            code.AppendLine("");
        }

        private void GenerateControllerCleanup(StringBuilder code)
        {
            code.IndentLine(2, "protected override void Dispose(bool disposing)");
            code.IndentLine(2, "{");
            code.IndentLine(3, "if (disposing)");
            code.IndentLine(3, "{");
            code.IndentLine(4, "DbContext.Dispose();");
            code.IndentLine(3, "}");
            code.IndentLine(3, "base.Dispose(disposing);");
            code.IndentLine(2, "}");
            code.AppendLine();
            code.IndentLine(1, "}");
            code.AppendLine("}");
            code.AppendLine();
        }

        private StringBuilder GenerateControllerHeader(string nameSpace)
        {
            StringBuilder code = new StringBuilder();
            //code.AppendLine(DisplayWarning);

            // includes
            code.AppendLine("using System;");
            code.AppendLine("using System.Data.Entity;");
            code.AppendLine("using System.Data.Entity.Infrastructure;");
            code.AppendLine("using System.Net;");
            code.AppendLine("using System.Linq;");
            code.AppendLine("using System.Reflection;");
            code.AppendLine("using System.Threading.Tasks;");
            code.AppendLine("using System.Web.Http;");
            code.AppendLine("using System.Web.Http.Description;");
            code.AppendLine("using ChasWare.Data;");
            code.AppendLine("using ChasWare.Data.Classes;");
            code.AppendLine("using ChasWare.Data.Classes.DTO;");
            code.AppendLine("using ChasWare.Data.Classes.TX;");
            code.AppendLine("using Common.Logging;");
            code.AppendLine("using System.Collections.Generic;");
            code.AppendLine();

            //class def
            code.AppendLine($"namespace {nameSpace}");
            code.AppendLine("{");
            code.IndentLine(1, $"public class {GetContextName()}Controller : ApiController");
            code.IndentLine(1, "{");
            code.IndentLine(2, "private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);");
            code.IndentLine(2, "private DataContext DbContext { get; }");
            code.AppendLine();

            // constructor
            code.IndentLine(2, $"public {GetContextName()}Controller()");
            code.IndentLine(2, "{");
            code.IndentLine(3, "DbContext = new DataContext();");
            code.IndentLine(3, "DbContext.Configuration.ProxyCreationEnabled = false;");
            code.IndentLine(2, "}");
            code.AppendLine();
            return code;
        }

        private void GenerateDeleteAction(StringBuilder code)
        {
            code.IndentLine(2, $"// DELETE: api/{_poco.Name}/5");
            code.IndentLine(2, $"[ResponseType(typeof({_poco.Name}DTO))]");
            code.IndentLine(2, $"public async Task<IHttpActionResult> Delete{_poco.Name}({GetDefinedParams()})");
            code.IndentLine(2, "{");
            code.IndentLine(3, "try");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"{_poco.Name} found = await DbContext.{GetContextName()}.FindAsync({GetCalledParams()});");
            code.IndentLine(4, "if (found == null)");
            code.IndentLine(4, "{");
            code.IndentLine(5, "return NotFound();");
            code.IndentLine(4, "}");
            code.IndentLine(4, $"DbContext.{GetContextName()}.Remove(found);");
            code.IndentLine(4, "await DbContext.SaveChangesAsync();");
            code.IndentLine(4, "return Ok(found);");
            code.IndentLine(3, "}");
            code.IndentLine(3, "catch(Exception ex)");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"Log.Error(\"{_poco.Name}.Delete: \" + ex);");
            code.IndentLine(4, "throw;");
            code.IndentLine(3, "}");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

        private void GenerateGetAction(StringBuilder code)
        {
            code.IndentLine(2, $"// GET: api/{_poco.Name}/5");
            code.IndentLine(2, $"[ResponseType(typeof({_poco.Name}DTO))]");
            code.IndentLine(2, $"public async Task<IHttpActionResult> Get{_poco.Name}({GetDefinedParams()})");
            code.IndentLine(2, "{");
            code.IndentLine(3, "try");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"{_poco.Name}DTO found = await Task.Run(() => ");
            code.IndentLine(5, "{");
            code.IndentLine(6, $"return {_poco.Name}TX.WriteToDTO(DbContext.{GetContextName()}");

            // add include to overcome lazy loading (when conflating)
            foreach (MemberInfo member in GetMembers(_poco).Where(m => m.GetCustomAttributes<Transform>().Any(a => a.Include)))
            {
                code.IndentLine(7, $".Include(a => a.{member.Name})");
            }

            // add where clause
            code.IndentLine(7, $".AsEnumerable().FirstOrDefault(e => {GetLinqWhere("e")}));");
            code.IndentLine(5, "});");
            code.IndentLine(4, "if (found == null)");
            code.IndentLine(4, "{");
            code.IndentLine(5, "return NotFound();");
            code.IndentLine(4, "}");
            code.IndentLine(4, "return Ok(found);");
            code.IndentLine(3, "}");
            code.IndentLine(3, "catch (Exception ex)");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"Log.Error(\"{_poco.Name}.Get: \" + ex);");
            code.IndentLine(4, "throw;");
            code.IndentLine(3, "}");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

        private void GeneratePostAction(StringBuilder code)
        {
            code.IndentLine(2, $" // POST: api/{GetContextName()}");
            code.IndentLine(2, $"[ResponseType(typeof({_poco.Name}DTO))]");
            code.IndentLine(2, $"public async Task<IHttpActionResult> Post{_poco.Name}({_poco.Name}DTO value)");
            code.IndentLine(2, "{");
            code.IndentLine(3, "try");
            code.IndentLine(3, "{");
            code.IndentLine(4, "if (!ModelState.IsValid)");
            code.IndentLine(4, "{");
            code.IndentLine(5, "return BadRequest(ModelState);");
            code.IndentLine(4, "}");
            code.IndentLine(4, $"DbContext.{GetContextName()}.Add({_poco.Name}TX.ReadFromDTO(new {_poco.Name}(), value));");
            code.IndentLine(4, "await DbContext.SaveChangesAsync();");
            code.IndentLine(4, $"return CreatedAtRoute(\"DefaultApi\", new {{ {GetNewRouting("value")} }}, value);");
            code.IndentLine(3, "}");
            code.IndentLine(3, "catch (Exception ex)");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"Log.Error(\"{_poco.Name}.Post: \" + ex);");
            code.IndentLine(4, "throw;");
            code.IndentLine(3, "}");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

        private void GeneratePutAction(StringBuilder code)
        {
            code.IndentLine(2, $"// PUT: api/{GetContextName()}/5");
            code.IndentLine(2, "[ResponseType(typeof(void))]");
            code.IndentLine(2, $"public async Task<IHttpActionResult> Put{_poco.Name}({GetDefinedParams()}, {_poco.Name}DTO value)");
            code.IndentLine(2, "{");
            code.IndentLine(3, "try");
            code.IndentLine(3, "{");
            code.IndentLine(4, "if (!ModelState.IsValid)");
            code.IndentLine(4, "{");
            code.IndentLine(5, "return BadRequest(ModelState);");
            code.IndentLine(4, "}");
            code.IndentLine(4, $"if ({GetLinqWhereNot("value")})");
            code.IndentLine(4, "{");
            code.IndentLine(5, "return BadRequest();");
            code.IndentLine(4, "}");
            code.IndentLine(4, "DbContext.Entry(value).State = EntityState.Modified;");
            code.IndentLine(4, "try");
            code.IndentLine(4, "{");
            code.IndentLine(5, "await DbContext.SaveChangesAsync();");
            code.IndentLine(4, "}");
            code.IndentLine(4, "catch (DbUpdateConcurrencyException)");
            code.IndentLine(4, "{");
            code.IndentLine(5, $"if (!ValueExists({GetCalledParams()}))");
            code.IndentLine(5, "{");
            code.IndentLine(6, "return NotFound();");
            code.IndentLine(5, "}");
            code.IndentLine(5, "throw;");
            code.IndentLine(4, "}");
            code.IndentLine(4, "return StatusCode(HttpStatusCode.NoContent);");
            code.IndentLine(3, "}");
            code.IndentLine(3, "catch (Exception ex)");
            code.IndentLine(3, "{");
            code.IndentLine(4, $"Log.Error(\"{_poco.Name}.Put: \" + ex);");
            code.IndentLine(4, "throw;");
            code.IndentLine(3, "}");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

        /// <summary>
        ///     read data from DTO back to POCO.
        ///     NOTE: does not manage array / enumerations as yet
        /// </summary>
        /// <param name="code"></param>
        private void GenerateReadFromDTO(StringBuilder code)
        {
            code.IndentLine(2, $"public static {_poco.Name} ReadFromDTO({_poco.Name} target, {_poco.Name}DTO source)");
            code.IndentLine(2, "{");

            foreach (Tuple<MemberInfo, MemberInfo> item in _members)
            {
                MemberInfo member = item.Item1;
                Type memberType = GetDataType(member);
                MemberInfo sibling = item.Item2;

                if (memberType.IsArray && memberType != typeof(string))
                {
                    Type elementType = memberType.GetElementType();
                    if (elementType != null)
                    {
                        // use TX for exported type to deep copy
                        if (ExportedTypes.Contains(elementType))
                        {
                            code.IndentLine(3, $"target.{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select({elementType}TX.WriteToDTO).ToArray(),");
                        }
                        else
                        {
                            code.IndentLine(3, $"target.{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select().ToArray(),");
                        }
                    }
                }
                else if (!memberType.IsArray && memberType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(memberType))
                {
                    code.IndentLine(3, $"Read{member.Name}FromDTO(target.{ConflateType(sibling)}{member.Name}, source.{member.Name});");
                }
                else if (ExportedTypes.Contains(memberType))
                {
                    code.IndentLine(3, $"{memberType.Name}TX.ReadFromDTO(target.{ConflateType(sibling)}{member.Name}, source.{member.Name});");
                }
                else
                {
                    code.IndentLine(3, $"target.{ConflateType(sibling)}{member.Name} = source.{member.Name};");
                }
            }

            code.IndentLine(3, "return target;");
            code.IndentLine(2, "}");
            code.AppendLine();

            // write methods to copy colections
            foreach (Tuple<MemberInfo, MemberInfo> item in _members)
            {
                MemberInfo member = item.Item1;
                Type memberType = GetDataType(member);

                if (!memberType.IsArray && memberType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(memberType))
                {
                    GenerateCopyCollection(code, member);
                }
            }
        }

        private void GenerateValueExists(StringBuilder code)
        {
            code.IndentLine(2, $"private bool ValueExists({GetDefinedParams()})");
            code.IndentLine(2, "{");
            code.IndentLine(3, $"return DbContext.{GetContextName()}.Count(e => {GetLinqWhere("e")}) > 0;");
            code.IndentLine(2, "}");
            code.AppendLine();
        }

        /// <summary>
        ///     copy data from POCO to new DTO and manage conflation
        /// </summary>
        /// <param name="code"></param>
        private void GenerateWriteToDTO(StringBuilder code)
        {
            code.IndentLine(2, $"public static {_poco.Name}DTO WriteToDTO({_poco.Name} source)");
            code.IndentLine(2, "{");
            code.IndentLine(3, $"return new {_poco.Name}DTO");
            code.IndentLine(4, "{");

            foreach (Tuple<MemberInfo, MemberInfo> item in _members)
            {
                MemberInfo member = item.Item1;
                Type memberType = GetDataType(member);
                MemberInfo sibling = item.Item2;

                if (memberType.IsArray)
                {
                    Type elementType = memberType.GetElementType();
                    if (elementType != null)
                    {
                        // use TX for exported type to deep copy
                        if (ExportedTypes.Contains(elementType))
                        {
                            code.IndentLine(5, $"{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select({elementType}TX.WriteToDTO).ToArray(),");
                        }
                        else
                        {
                            code.IndentLine(5, $"{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select().ToArray(),");
                        }

                        continue;
                    }
                }

                if (memberType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(memberType))
                {
                    // use TX for exported type to deep copy
                    MemberInfo elementType = memberType.GetGenericArguments()[0];
                    if (ExportedTypes.Contains(elementType))
                    {
                        code.IndentLine(5, $"{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select({elementType.Name}TX.WriteToDTO).ToArray(),");
                    }
                    else
                    {
                        code.IndentLine(5, $"{member.Name} = source.{ConflateType(sibling)}{member.Name}.Select().ToArray()");
                    }

                    continue;
                }

                if (ExportedTypes.Contains(memberType))
                {
                    code.IndentLine(5, $"{member.Name} = {ConflateType(sibling)}{member.Name}TX.WriteToDTO(source.{member.Name}),");
                }
                else
                {
                    code.IndentLine(5, $"{member.Name} = source.{ConflateType(sibling)}{member.Name},");
                }
            }

            code.IndentLine(4, "};");
            code.IndentLine(2, "}");
            code.AppendLine("");
        }

        private string GetCalledParams()
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (keyFields.Any())
            {
                StringBuilder code = new StringBuilder();
                foreach (MemberInfo keyField in keyFields)
                {
                    if (code.Length > 0)
                    {
                        code.Append(", ");
                    }

                    code.Append($"{ToCamelCase(keyField.Name)}");
                }

                return code.ToString();
            }

            return string.Empty;
        }

        private string GetContextName()
        {
            return _poco.GetCustomAttributes<Transform>().FirstOrDefault(a => !string.IsNullOrEmpty(a.ContextName))?.ContextName ?? _poco.Name + "s";
        }

        private string GetDefinedParams()
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (keyFields.Any())
            {
                StringBuilder code = new StringBuilder();
                foreach (MemberInfo keyField in keyFields)
                {
                    if (code.Length > 0)
                    {
                        code.Append(", ");
                    }

                    code.Append($"{FormatType(GetDataType(keyField))} {ToCamelCase(keyField.Name)}");
                }

                return code.ToString();
            }

            return string.Empty;
        }

        private string GetLinqWhere(string variable)
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (keyFields.Any())
            {
                StringBuilder code = new StringBuilder();
                foreach (MemberInfo keyField in keyFields)
                {
                    if (code.Length > 0)
                    {
                        code.Append(" && ");
                    }

                    code.Append($"{variable}.{keyField.Name} == {ToCamelCase(keyField.Name)}");
                }

                return code.ToString();
            }

            return string.Empty;
        }

        private string GetLinqWhereNot(string variable)
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (keyFields.Any())
            {
                StringBuilder code = new StringBuilder();
                foreach (MemberInfo keyField in keyFields)
                {
                    if (code.Length > 0)
                    {
                        code.Append(" || ");
                    }

                    code.Append($"{variable}.{keyField.Name} != {ToCamelCase(keyField.Name)}");
                }

                return code.ToString();
            }

            return string.Empty;
        }

        private string GetNewRouting(string value)
        {
            IList<MemberInfo> keyFields = AnalyseKeyFields(_poco);
            if (keyFields.Any())
            {
                StringBuilder code = new StringBuilder();
                foreach (MemberInfo keyField in keyFields)
                {
                    if (code.Length > 0)
                    {
                        code.Append(", ");
                    }

                    code.Append($"{keyField.Name} = {value}.{keyField.Name}");
                }

                return code.ToString();
            }

            return string.Empty;
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
            ///     will copy unmatched fields from siblings
            /// </summary>
            public bool Conflate { get; set; }

            /// <summary>
            ///     name of method on datacontext where name may not obvious
            /// </summary>
            public string ContextName { get; set; }

            /// <summary>
            ///     should this class be exported
            /// </summary>
            public bool Ignore { get; set; }

            /// <summary>
            ///     force ef to load child object if lazy loaded
            /// </summary>
            public bool Include { get; set; }

            #endregion
        }

        #endregion
    }
}