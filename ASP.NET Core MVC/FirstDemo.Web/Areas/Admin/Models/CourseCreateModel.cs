using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        [Required]
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private  ICourseService _courseService;
        private  ILifetimeScope _scope;

        //// Must be keep here empty Constructor 

        public CourseCreateModel()
        {

        }

        public CourseCreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }


        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
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
