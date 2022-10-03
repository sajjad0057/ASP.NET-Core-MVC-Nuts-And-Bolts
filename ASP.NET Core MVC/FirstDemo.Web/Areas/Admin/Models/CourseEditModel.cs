using Autofac;
using FirstDemo.Infrastructure.Services;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseEditModel : BaseModel
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private ICourseService _courseService;


        public CourseEditModel():base()
        {

        }


        public CourseEditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }   

        public void LoadData(Guid id)
        {
           var course = _courseService.GetCourse(id);

            if(course != null)
            {

                Id = course.Id;
                Title = course.Name;
                Fees = course.Fees;
                ClassStartDate = course.ClassStartDate;

            }
        }


        public void EditCourse()
        {
            CourseBO courseBO = new CourseBO();
            courseBO.Id = Id;
            courseBO.Name = Title;
            courseBO.Fees = Fees;
            courseBO.ClassStartDate = ClassStartDate;

            _courseService.EditCourse(courseBO);
        }
        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _courseService = _scope.Resolve<ICourseService>();
        }



    }
}
