// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Global.asax.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ChasWare.Common.DI;
using ChasWare.Common.Utils.Helpers;
using ChasWare.Data.Classes;
using Common.Logging;
using Spring.Context;

namespace ChasWare.DataService
{
    public class WebApiApplication : HttpApplication
    {
        #region other methods

        protected void Application_Start()
        {
            ILog log = Logging.Initialise();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IApplicationContext ctx = DependencyInjection.InitialiseSpring();

            // trigger context load
            if (!ctx.GetObject<IEnumerable<Person>>().Any())
            {
                log.Error("Where is everybody?");
            }
        }

        #endregion
    }
}