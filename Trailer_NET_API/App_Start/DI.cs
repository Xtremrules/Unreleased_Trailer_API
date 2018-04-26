using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Trailer_NET_DL;
using Trailer_NET_DL.Concrete;
using Trailer_NET_DL.Infrastructure;
using Trailer_NET_DL.Repository;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_API
{
    public class DI
    {
        private static IContainer Container;

        public static void Initialize()
        {
            Initialize(RegisterServices(new ContainerBuilder()));
        }

        private static void Initialize(IContainer container)
        {
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<AppDbContext>()
                   .As(typeof(IdentityDbContext<AppUser>))
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
               .As<IDbFactory>()
               .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                   .As(typeof(IEntityBaseRepository<>))
                   .InstancePerRequest();

            //builder.RegisterAssemblyTypes(Assembly.Load("CBT.Data"))
            //    .Where(x => x.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces()
            //    .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}