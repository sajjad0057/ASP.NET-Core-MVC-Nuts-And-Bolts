using Autofac;

namespace FirstDemo.Worker
{
    public class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /*
            builder.RegisterType<CourseModel>().As<ICourseModel>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CourseModel>().AsSelf();
            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<CourseEditModel>().AsSelf();
            builder.RegisterType<CourseListModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            */
            base.Load(builder);
        }
    }
}