using Autofac;
using AutoMapper;
using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using FirstDemo.Infrastructure.Models;
using FirstDemo.Infrastructure.Services;

namespace FirstDemo.API.Models
{
    public class CourseModel
    {

        private ICourseService? _courseService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Fees { get; set; }
        public DateTime ClassStartDate { get; set; }

        public CourseModel()
        {

        }

        public CourseModel(ICourseService coursService, IMapper mapper)
        {
            _courseService = coursService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _courseService = scope.Resolve<ICourseService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<CourseBO> GetCourses()
        {
            return _courseService?.GetCourses();
        }

        internal void DeleteCourse(Guid id)
        {
            _courseService?.DeleteCourse(id);
        }

        internal void CreateCourse()
        {
            CourseBO course = _mapper.Map<CourseBO>(this);

            _courseService.CreateCourse(course);
        }

        internal void UpdateCourse()
        {
            CourseBO course = _mapper.Map<CourseBO>(this);

            _courseService.EditCourse(course);
        }

        internal CourseBO GetCourse(string name)
        {
            return _courseService.GetCourse(name);
        }

        internal CourseBO GetCourse(Guid id)
        {
            return _courseService.GetCourse(id);
        }

        internal object? GetPagedCourses(DataTablesAjaxRequestModel model)
        {

            var data = _courseService?.GetCourses(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Title", "Fees", "ClassStartDate" }));

            return new
            {
                recordsTotal = data?.total,
                recordsFiltered = data?.totalDisplay,
                data = (from record in data?.records
                        select new string[]
                        {
                                record.Name,
                                record.Fees.ToString(),
                                record.ClassStartDate.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
