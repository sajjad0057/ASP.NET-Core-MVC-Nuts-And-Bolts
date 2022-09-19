using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private  ICourseService _courseService;

        //// Must be keep here empty Constructor 

        public CourseCreateModel() : base()
        {

        }

        public CourseCreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _courseService = _scope.Resolve<ICourseService>();
        }
        internal async Task CreateCourse()        //// we use async method here for, program wait here for successfully create Course instance
        {
            ////later We Create BusinessObject using AutoMapper , now create manually -
            
            Course course = new Course();
            course.Name = Title;
            course.Fees = Fees;
            course.ClassStartDate = ClassStartDate;

            _courseService.CreateCourse(course);
        }
    }
}
