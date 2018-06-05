// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Logging.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using Common.Logging;
using eSpares.Levity;

namespace ChasWare.Common.Utils.Helpers
{
    /// <summary>
    ///     commonly used logging methods
    /// </summary>
    public static class Logging
    {
        #region public methods

        public static ILog Initialise()
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("");
            log.Info(string.Empty.PadRight(60, '-'));
            log.Info(StrFuncs.PadAndWrap(60, "--", "Session started at", DateTime.Now.ToLongTimeString()));
            log.Info(StrFuncs.PadAndWrap(60, "--", "Current version", ApplicationAssemblyUtility.GetApplicationVersionNumber()));
            log.Info(string.Empty.PadRight(60, '-'));
            log.Info("");
            return log;
        }

        #endregion
    }
}