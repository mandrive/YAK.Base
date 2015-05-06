using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TheZtack.Database;
using TheZtack.SearchEngine;

namespace TheZtack
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SetupServiceContainer();
        }

        private void SetupServiceContainer()
        {
            var container = new ServiceContainer();
            container.RegisterControllers(typeof(TheZtack.MvcApplication).Assembly);

            RegisterTypes(container);

            container.EnableMvc();
        }

        private void RegisterTypes(ServiceContainer container)
        {
            container.Register<SearchEngineCore>(new PerContainerLifetime());
            container.Register<DatabaseContext>(new PerRequestLifeTime());
        }
    }
}
