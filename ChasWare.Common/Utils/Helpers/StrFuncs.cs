// --------------------------------------------------------------------------------------------------------------------
// <copyright file=StrFuncs.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;

namespace ChasWare.Common.Utils.Helpers
{
    /// <summary>
    ///     The str funcs.
    /// </summary>
    public static class StrFuncs
    {
        #region public methods

        /// <summary>
        ///     The contains.
        /// </summary>
        /// <param name="value">
        ///     The string to search.
        /// </param>
        /// <param name="toFind">
        ///     The string to look for.
        /// </param>
        /// <returns>
        ///     returns true if found <see cref="bool" />.
        /// </returns>
        public static bool CaselessContains(this string value, string toFind)
        {
            if (ReferenceEquals(value, toFind))
            {
                return true;
            }

            if (value is null || toFind is null)
            {
                return false;
            }

            return value.IndexOf(toFind, StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>
        ///     see if string is one of the following
        /// </summary>
        /// <param name="text">
        ///     text to search for
        /// </param>
        /// <param name="choices">
        ///     list of items to search for
        /// </param>
        /// <returns>
        ///     true if found
        /// </returns>
        public static bool IsOneOf(this string text, params string[] choices)
        {
            if (text == null || choices is null || choices.Length == 0)
            {
                return false;
            }

            return choices.Any(choice => choice.CaselessContains(text));
        }

        /// <summary>
        ///     check to see if text is a substring of any of values
        /// </summary>
        /// <param name="text">
        ///     text to search for
        /// </param>
        /// <param name="choices">
        ///     targets to search
        /// </param>
        /// <returns>
        ///     true if found
        /// </returns>
        public static bool IsSubstringOfAny(this string text, params string[] choices)
        {
            if (text == null || choices is null || choices.Length == 0)
            {
                return false;
            }

            text = text.Trim();
            return choices.Any(val => val.IndexOf(text, StringComparison.OrdinalIgnoreCase) > 0);
        }

        /// <summary>
        ///     build string out of sub strings delimited with space
        /// </summary>
        /// <param name="vals">
        ///     text to search for
        /// </param>
        /// <returns>
        ///     packed string
        /// </returns>
        public static string PackOutStrings(params object[] vals)
        {
            if (vals is null || vals.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (string val in vals)
            {
                if (!string.IsNullOrWhiteSpace(val))
                {
                    result.Append(val);
                    result.Append(" ");
                }
            }

            return result.ToString().Trim();
        }

        /// <summary>
        ///     Pad and wrap string (ideal for logging)
        /// </summary>
        /// <param name="width">minimum width of string desired</param>
        /// <param name="wrapping">string to put at each end of string</param>
        /// <param name="values">values to put in string, seperated by padding </param>
        /// <returns></returns>
        public static string PadAndWrap(int width, string wrapping, params string[] values)
        {
            StringBuilder result = new StringBuilder();
            result.Append(wrapping);
            result.Append(" ");
            result.Append(PackOutStrings(values));

            int remaining = width - wrapping.Length - 1 - result.Length;
            if (remaining > 0)
            {
                result.Append(string.Empty.PadRight(remaining, ' '));
            }

            result.Append(" ");
            result.Append(wrapping);
            return result.ToString();
        }


        /// <summary>
        ///     get substring form string for everything before first instance of delim
        /// </summary>
        /// <param name="text">
        ///     text to search
        /// </param>
        /// <param name="delim">
        ///     the delimiter
        /// </param>
        /// <returns>
        ///     substring or whole if delim not found)
        /// </returns>
        public static string SubstringUpTo(this string text, char delim)
        {
            if (text == null)
            {
                return null;
            }

            int found = text.IndexOf(delim);
            return found > -1 ? text.Substring(0, found) : text;
        }

        #endregion
    }
}