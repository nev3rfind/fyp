using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NhsImsApp.Data;
using System.Data.Entity;

namespace NhsImsApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // The data directory
            string dataDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

            // Create new instance and pass it Intitialise method
            using (var context = new ApplicationDbContext())
            {
                DbInitializer.Initialize(context);
            }
        }
    }
}
