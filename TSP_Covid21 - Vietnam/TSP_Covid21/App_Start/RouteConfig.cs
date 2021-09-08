using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TSP_Covid21
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Api1",
                url: "apiA/{action}/{id}",
                defaults: new { controller = "ApiAccount", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Api",
                url: "apiP/{action}/{id}",
                defaults: new { controller = "ApiProduct", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Covid21", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
