// --------------------------------------------------------------------------------------------------------------------
// <copyright file=WebApiConfig.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

namespace DataService
{
    public static class WebApiConfig
    {
        #region public methods

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }

        #endregion
    }
}