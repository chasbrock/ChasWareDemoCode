using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChasWare.Common.Utils.Transform
{
    /// <summary>
    /// tools to transform C# classes to TypeScript
    /// </summary>
    public static class TsTransformTools
    {
        /// <summary>
        /// write out file to named path, using type name 
        /// </summary>
        /// <param name="type">type to export</param>
        /// <param name="path">path to export to</param>
        public static void PrintToFile(Type type, string content, string path, string extension)
        {
            string outputPath = Path.ChangeExtension(Path.Combine(path, type.Name), extension);
            File.WriteAllText(outputPath, content);
        }

        /// <summary>
        /// get all classes in assembly that have the ExportToTs attribute
        /// </summary>
        /// <param name="assemblyName">name of assembly to check</param>
        /// <returns>list of types, or empty list</returns>
        public static IEnumerable<Type> GetExportedTypes(string assemblyName)
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

            return loaded?.GetTypes().Where(t => t.GetCustomAttributes<ExportToTs>().Any()).ToList();
        }


        /// <summary>
        /// writetype to Ts class format
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ExtractClass(Type t, IEnumerable<Type> knownTypes)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("export class {0} {{\n", t.Name);
            var members = GetInterfaceMembers(t);
            foreach (var mi in members)
            {
                sb.AppendFormat("  {0}: {1};\n", mi.Name, GetMemberTypeName(mi, knownTypes));
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static IEnumerable<MemberInfo> GetInterfaceMembers(Type type)
        {
            return type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(mi => mi.MemberType == MemberTypes.Field || mi.MemberType == MemberTypes.Property);
        }

        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (s.Length < 2) return s.ToLowerInvariant();
            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        public static string GetMemberTypeName(MemberInfo mi, IEnumerable<Type> knownTypes)
        {
            Type t = (mi is PropertyInfo) ? ((PropertyInfo)mi).PropertyType : ((FieldInfo)mi).FieldType;
            return GetTypeName(t, knownTypes);
        }

        public static string GetTypeName(Type t, IEnumerable<Type> knownTypes)
        {
            if (t.IsPrimitive)
            {
                if (t == typeof(bool)) return "boolean";
                if (t == typeof(char)) return "string";
                return "number";
            }
            if (t == typeof(decimal)) return "number";
            if (t == typeof(string)) return "string";
            if (t.IsArray)
            {
                var at = t.GetElementType();
                return GetTypeName(at, knownTypes) + "[]";
            }
            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(t))
            {
                var collectionType = t.GetGenericArguments()[0]; // all my enumerables are typed, so there is a generic argument
                return GetTypeName(collectionType, knownTypes) + "[]";
            }
            if (Nullable.GetUnderlyingType(t) != null)
            {
                return GetTypeName(Nullable.GetUnderlyingType(t), knownTypes);
            }
            if (t.IsEnum) return "number";
            if (knownTypes.Contains(t)) return t.Name;
            return "any";
        }

        public static string ExtractEnums<T>() // Enums<>, since Enum<> is not allowed.
        {
            Type t = typeof(T);
            var sb = new StringBuilder();
            int[] values = (int[])Enum.GetValues(t);
            sb.AppendLine("var " + t.Name + " = {");
            foreach (var val in values)
            {
                var name = Enum.GetName(typeof(T), val);
                sb.AppendFormat("{0}: {1},\n", name, val);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}