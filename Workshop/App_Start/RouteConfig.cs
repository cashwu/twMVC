using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Workshop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var ns = new string[] { "Workshop.Controllers" };

            routes.MapRoute(
                name: "Article",
                url: "{year}-{month}-{day}/{categoryName}/{subject}",
                defaults: new { controller = "Article", action = "SeoDetails" },
                constraints: new { year = @"20\d{2}", month = @"\d{1,2}", day = @"\d{1,2}", categoryName = ".+", subject = ".+" },
                namespaces: ns
            );

            routes.MapRoute(
                name: "Archive_YMD",
                url: "{year}-{month}-{day}/{categoryName}",
                defaults: new { controller = "Article", action = "Archive", categoryName = UrlParameter.Optional },
                constraints: new { year = @"20\d{2}", month = @"\d{1,2}", day = @"\d{1,2}" },
                namespaces: ns
            );
            routes.MapRoute(
                name: "Archive_YM",
                url: "{year}-{month}/{categoryName}",
                defaults: new { controller = "Article", action = "Archive", categoryName = UrlParameter.Optional },
                constraints: new { year = @"20\d{2}", month = @"\d{1,2}" },
                namespaces: ns
            );
            routes.MapRoute(
                name: "Archive_Y",
                url: "{year}/{categoryName}",
                defaults: new { controller = "Article", action = "Archive", categoryName = UrlParameter.Optional },
                constraints: new { year = @"20\d{2}" },
                namespaces: ns
            );

            routes.MapRoute(
                name: "Category",
                url: "Category/{categoryName}",
                defaults: new { controller = "Article", action = "Index", categoryName = UrlParameter.Optional },
                namespaces: ns
            );

            routes.MapRoute(
                name: "Hot",
                url: "Hot",
                defaults: new { controller = "Article", action = "Hot" },
                namespaces: ns
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Workshop.Controllers" }
            );
        }
    }
}