using Autofac;

namespace FirstDemo.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterType<CourseModel>().AsSelf();
            //builder.RegisterType<CourseCreateModel>().AsSelf();
            //builder.RegisterType<CourseEditModel>().AsSelf();
            //builder.RegisterType<CourseListModel>().AsSelf();

            base.Load(builder);
        }
    }
}
