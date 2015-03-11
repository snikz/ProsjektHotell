using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotellWebMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "", new { controller = "Authentication", action = "Login" });
            routes.MapRoute("Logout", "logout", new { controller = "Authentication", action = "Logout" });
            routes.MapRoute("Booking", "booking", new { controller = "Booking", action = "Index" });
            
        }
    }
}
