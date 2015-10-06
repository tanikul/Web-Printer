using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebPrinter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Owner", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "AddCustomer",
                url: "AddCustomer",
                defaults: new { controller = "Customer", action = "Index" }
            );

            routes.MapRoute(
                name: "ManageAccount",
                url: "ManageAccount",
                defaults: new { controller = "Owner", action = "ManageAccount" }
            );

            routes.MapRoute(
                name: "ManagePaper",
                url: "ManagePaper",
                defaults: new { controller = "StockPaper", action = "Index" }
            );

            routes.MapRoute(
                name: "ManagePrinter",
                url: "ManagePrinter",
                defaults: new { controller = "Printer", action = "Index" }
            );

            routes.MapRoute(
                name: "ManageFinishing",
                url: "ManageFinishing",
                defaults: new { controller = "Finishing", action = "Index" }
            );

            routes.MapRoute(
                 name: "PriceCalculate",
                 url: "PriceCalculate",
                 defaults: new { controller = "Home", action = "SearchResult" }
             );

        }
    }
}
