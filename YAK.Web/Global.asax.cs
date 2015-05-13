using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightInject;
using Yak.Database;
using Yak.SearchEngine;
using Yak.Web;
using Yak.Services.Interfaces;
using Yak.DTO;
using Yak.Services;

namespace Yak
{
    public class MvcApplication : HttpApplication
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
            container.RegisterControllers(typeof(MvcApplication).Assembly);
            container.EnableMvc();
        }
    }
}
