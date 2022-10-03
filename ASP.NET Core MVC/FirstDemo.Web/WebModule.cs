using Autofac;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;

namespace FirstDemo.Web
{
    //// **** Should not be used Model in Dependency Injection , although here we used Model Instance for create a Dependency Injection Examples **** 
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<CourseModel>().As<ICourseModel>();  ///// it's like work as AddTransient<>() ServiceCollections Method

            //builder.RegisterType<CourseModel>().As<ICourseModel>()   //// it's like work as AddSingleton<>() ServiceCollections Method,provide single intance for single request.
            //    .SingleInstance()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<CourseModel>().As<ICourseModel>()   //// it's like work as AddSingleton<>() ServiceCollections Method,provide single intance for all request .
                .SingleInstance();


            //// InstancePerLifetimeScope() method keeps a single instance for single request .


            //builder.RegisterType<CourseModel>().As<ICourseModel>()   //// here class create with IClass Interface .
            //    .InstancePerLifetimeScope();

            builder.RegisterType<CourseModel>().AsSelf();      //// here , instance create with only Class  .


            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<CourseListModel>().AsSelf();
            builder.RegisterType<CourseEditModel>().AsSelf();


            base.Load(builder);
        }
    }
}
