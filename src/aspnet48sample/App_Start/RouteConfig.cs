using System.Web.Mvc;
using System.Web.Routing;

namespace aspnet48sample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "EnvironmentVariables",
                 url: "env",
                 defaults: new { controller = "Home", action = "EnvironmentVariables" }
            );
            routes.MapRoute(
                 name: "ServerVariables",
                 url: "srv",
                 defaults: new { controller = "Home", action = "ServerVariables" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
