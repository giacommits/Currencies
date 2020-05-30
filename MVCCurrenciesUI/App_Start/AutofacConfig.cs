using Autofac;
using Autofac.Integration.Mvc;
using CurrenciesLibrary.CurrenciesAPI;
using CurrenciesLibrary.CurrenciesUtilities;
using MVCCurrenciesUI.Controllers.Helpers;
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
            builder.RegisterType<CurrenciesList>().As<ICurrenciesList>().InstancePerLifetimeScope();
            builder.RegisterType<APIHelper>().As<IAPIHelper>().SingleInstance();
            builder.RegisterType<CurrenciesListHelper>().As<ICurrenciesListHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DatesRangeHelper>().As<IDatesRangeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<RateHelper>().As<IRateHelper>().InstancePerLifetimeScope();

            

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}