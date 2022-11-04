using Autofac;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Repositories;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Infrastructure.UnitOfWorks;

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


            /*
               Must be Binding ApplicationDbContext as AsSelf()..ApplicationDbContext Class have parametterized constructor
            -------------------------------------------------------------------------------------------------------------------------------------------
               Basically the main reason is when we create migrations we use --context ApplicationDbContext in migration command , 
               not Use IApplicationDbContext interface , so that Command Cannot resolve this interface . so need ApplicationDbContext AsSelf() Binding.
             */
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


            builder.RegisterType<TimeService>().As<ITimeService>()
                .InstancePerLifetimeScope();


            builder.RegisterType<DataUtility>().As<IDataUtility>()
                .InstancePerLifetimeScope();




            base.Load(builder);
        }
    }
}
