using Autofac;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Repositories;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectingString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectingString, string migrationAssemblyName)
        {
            _connectingString = connectingString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {

            //// InstancePerLifetimeScope() method keeps a single instance for single request .
            //// here pass parameter coz, ApplicationDbContext class constructor received two parameters . 
           
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectingString", _connectingString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectingString", _connectingString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();


            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseRepository>().As<ICourseRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();




            base.Load(builder);
        }
    }
}
