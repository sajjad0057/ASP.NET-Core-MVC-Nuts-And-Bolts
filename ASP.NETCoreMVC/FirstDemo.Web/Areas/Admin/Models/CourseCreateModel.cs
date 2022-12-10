using Autofac;
using AutoMapper;
using FirstDemo.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel : BaseModel
    {

        // For Using ServerSide Validations - 
        [Required(ErrorMessage = "Title must be provided"), StringLength(200, ErrorMessage = "Title should be less than 200 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Fees must be provided"), Range(1000, 50000, ErrorMessage = "Fees range must have 1000 to 50000")]
        public double Fees { get; set; }
        [Required(ErrorMessage = "Valid date time must be provided")]
        public DateTime ClassStartDate { get; set; }

        private ICourseService _courseService;
        private IMapper _mapper;

        //// Must be keep here empty Constructor 

        public CourseCreateModel() : base()
        {

        }

        public CourseCreateModel(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);     ////Resolving Dependency in BaseModel class .
                                               
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
