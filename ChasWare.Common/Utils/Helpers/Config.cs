// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Config.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using ChasWare.Common.Utils.Properties;

namespace ChasWare.Common.Utils.Helpers
{
    /// <summary>
    ///     simple type conversion (not nullable safe at moment)
    /// </summary>
    public static class Config
    {
        #region public methods

        /// <summary>
        ///     get setting from file returning value of given type
        /// </summary>
        /// <typeparam name="T">
        ///     type to return
        /// </typeparam>
        /// <param name="key">
        ///     the key in app.config
        /// </param>
        /// <param name="defaultValue">
        ///     default value
        /// </param>
        /// <returns>
        ///     value if found / default otherwise
        /// </returns>
        public static T AppSetting<T>([NotNull] string key, T defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return (T) Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        ///     get setting from file returning string
        /// </summary>
        /// <param name="key">
        ///     the key in app.config
        /// </param>
        /// <param name="defaultValue">
        ///     default value
        /// </param>
        /// <returns>
        ///     value if found / default otherwise
        /// </returns>
        public static string AppSetting([NotNull] string key, string defaultValue = "")
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        /// <summary>
        ///     seee if setting is present
        /// </summary>
        /// <param name="key">
        ///     the key to look for
        /// </param>
        /// <returns>
        ///     true if found
        /// </returns>
        public static bool IsAppSettingAvailable([NotNull] string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            return !string.IsNullOrWhiteSpace(value);
        }

        #endregion
    }
}