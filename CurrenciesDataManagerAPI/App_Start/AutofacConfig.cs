using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CurrenciesDataAccess.Repositories;
using CurrenciesDataManagerLibrary.Entities;
using CurrenciesDataManagerLibrary.Processor;
using CurrenciesDataManagerLibrary.Repositories;

namespace CurrenciesDataManagerAPI.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));            
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterType<CurrenciesDb>()
               .AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<DataProcessor>()
               .As<IDataProcessor>()
               .InstancePerLifetimeScope();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DatesRangeRepository>()
               .As<IDatesRangeRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<CurrenciesListRepository>()
               .As<ICurrenciesListRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<CurrenciesRateRepository>()
                  .As<ICurrenciesRateRepository>()
                  .InstancePerLifetimeScope();         

            Container = builder.Build();
            return Container;
        }
    }
}