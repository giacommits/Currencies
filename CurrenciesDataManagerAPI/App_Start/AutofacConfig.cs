using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CurrenciesDataAccess.Models;
using CurrenciesDataAccess.Repositories;
using CurrenciesDataManagerLibrary.Processor;

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

            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ExchangeRateRepository>()
                  .As<IExchangeRateRepository>()
                  .InstancePerLifetimeScope();
            builder.RegisterType<CurrenciesRepository>()
                .As<ICurrenciesRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DatesRangeRepository>()
                .As<DatesRangeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CurrenciesDb>()
                .AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataProcessor>()
                .As<IDataProcessor>()
                .InstancePerLifetimeScope();

           
            Container = builder.Build();

            return Container;
        }


    }
}