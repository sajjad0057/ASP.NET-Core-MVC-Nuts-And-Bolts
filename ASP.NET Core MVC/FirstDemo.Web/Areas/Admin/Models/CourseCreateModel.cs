using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        private  ICourseService _courseService;
        private IMapper _mapper;

        //// Must be keep here empty Constructor 

        public CourseCreateModel() : base()
        {

        }

        public CourseCreateModel(ICourseService courseService,IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _courseService = _scope.Resolve<ICourseService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        internal async Task CreateCourse()        //// we use async method here for, program wait here for successfully create Course instance
        {

            CourseBO course = _mapper.Map<CourseBO>(this);  //// If we set here 'this' then having problem when we try to perform unit testing . 

            _courseService.CreateCourse(course);
        }
    }
}
