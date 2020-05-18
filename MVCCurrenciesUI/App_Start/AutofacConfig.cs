using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCurrenciesUI.App_Start
{
    public class AutofacConfig
    {
        public static void  Configure()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}