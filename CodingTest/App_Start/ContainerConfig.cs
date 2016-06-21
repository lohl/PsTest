using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CodingTest.Controllers;
using CodingTest.Models;
using CodingTest.Repositories;

namespace CodingTest.App_Start
{
    public class ContainerConfig
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            RegisterMVCComponents(builder);
            builder.RegisterType<DataContext>().AsImplementedInterfaces().AsSelf();
            RegisterOtherComponents(builder);
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

        private static void RegisterOtherComponents(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
    
            builder.RegisterType<MagRepository>().As<IMagRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RepositoryFactory>().As<IRepositoryFactory>().InstancePerLifetimeScope();
        }

        private static void RegisterMVCComponents(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();
        }
    }
}