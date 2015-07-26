using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using log4net.Config;
using LightInject;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web;
using Yak.Web.Interfaces;
using Yak.Web.Models;

namespace Yak
{
    public class MvcApplication : HttpApplication
    {
        private static ServiceContainer _serviceContainer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _serviceContainer = new ServiceContainer();
            _serviceContainer.RegisterControllers(typeof(MvcApplication).Assembly);
            _serviceContainer.EnableMvc();
            
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        public void WindowsAuthentication_OnAuthenticate(object sender, WindowsAuthenticationEventArgs e)
        {
            var customPrincipal = User as ICustomPrincipal;
            if (customPrincipal == null && e.Identity != null)
            {
                customPrincipal = new CustomPrincipal(_serviceContainer.GetInstance<IService<User>>(), e.Identity);
                HttpContext.Current.User = customPrincipal;
            }

            if (customPrincipal != null && customPrincipal.DatabaseUser == null)
            {
                var userSrvice = _serviceContainer.GetInstance<IService<User>>();
                var newUser = new User
                {
                    Username = customPrincipal.Identity.Name.Split('\\')[1],
                    // TODO: somehow get user email address from AD
                    Email = "test@test.test"
                };
                
                userSrvice.Add(newUser);
            }
        }
    }
}
