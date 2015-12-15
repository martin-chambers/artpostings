using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Code for registering our repository class and DI
            var container = new Container();
            container.Register<IPostingService, PostingService>(Lifestyle.Singleton);
            container.Register<IPostingRepository, PostingRepository>(Lifestyle.Singleton);
            
            container.Verify();

            // this was included in an example I saw - not apparently required for my application; retained for later examination
            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly()); 

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));        

        }
    }
}
