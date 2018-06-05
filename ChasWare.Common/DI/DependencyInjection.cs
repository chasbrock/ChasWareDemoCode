// --------------------------------------------------------------------------------------------------------------------
// <copyright file=DependencyInjection.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Logging;
using Spring.Context;
using Spring.Context.Attributes;
using Spring.Context.Support;

namespace ChasWare.Common.DI
{
    /// <summary>
    ///     Dependency injection.
    /// </summary>
    [Configuration]
    public class DependencyInjection
    {
        #region Constants and fields 

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IApplicationContext _context;

        #endregion

        #region other properties

        protected static IApplicationContext Context => _context ?? (_context = InitialiseSpring());

        #endregion

        #region public methods

        /// <summary>
        ///     initialise spring
        /// </summary>
        /// <returns>the application context</returns>
        public static IApplicationContext InitialiseSpring()
        {
            try
            {
                if (_context != null)
                {
                    return _context;
                }

                List<string> assemblies = GetAssembliesToUse();
                CodeConfigApplicationContext context = new CodeConfigApplicationContext();
                context.ScanWithAssemblyFilter(a =>
                {
                    if (assemblies.Contains(a.GetName().Name))
                    {
                        Logger.Debug($"Assembly scan added =>{a.GetName().Name}");
                        return true;
                    }

                    Logger.Debug($"Assembly scan ignored =>{a.GetName().Name}");
                    return false;
                });
                context.Refresh();
                _context = context;
                return context;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed To intialise spring \n {ex}");
                throw;
            }
        }

        #endregion

        #region other methods

        /// <summary>
        ///     allow us to use non david code for debugging through
        ///     app.config file (add key="UseDummyCode" value="true"/)
        /// </summary>
        /// <returns>list of assemblies to check</returns>
        private static List<string> GetAssembliesToUse()
        {
            return new List<string> {"ChasWare.Common", "ChasWare.Data"};
        }

        #endregion
    }
}