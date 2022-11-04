using AutoMapper;
using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Infrastructure.UnitOfWorks;

using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Services
{
    public class CourseService : ICourseService
    {

        private IApplicationUnitOfWork _applicationUnitOfWork;
        private IMapper _mapper;

        public CourseService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCourse(CourseBO courseBO)
        {
            var count = _applicationUnitOfWork.Courses.GetCount(x => x.Title == courseBO.Name);

            if (count > 0)
            {
                throw new DuplicateException("Course Title Already Exists !");
            }

            courseBO.SetProperClassStartDate();


            //// Using AutoMapper - 

            CourseEO courseEntity = _mapper.Map<CourseEO>(courseBO);  //// In this Overload Using sourse as courseBO and returned courseEntity object mapping CourseBO with CourseEO

            _applicationUnitOfWork.Courses.Add(courseEntity);
            _applicationUnitOfWork.Save();
        }


        public (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
                .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);


            IList<CourseBO> courses = new List<CourseBO>();

            foreach (CourseEO courseEO in results.data)
            {
                courses.Add(_mapper.Map<CourseBO>(courseEO));

            }

            return (results.total, results.totalDisplay, courses);


        }



        public void DeteleCourse(Guid id)
        {
            _applicationUnitOfWork.Courses.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public CourseBO GetCourse(Guid id)
        {
            CourseEO courseEO = _applicationUnitOfWork.Courses.GetById(id);

            CourseBO courseBO = _mapper.Map<CourseBO>(courseEO);

            return courseBO;
        }

        public void EditCourse(CourseBO courseBO)
        {
            var courseEO = _applicationUnitOfWork.Courses.GetById(courseBO.Id);

            if (courseEO != null)
            {
                _mapper.Map(courseBO, courseEO);  //// In this Overload Using sourse as courseBO and and Mapping CourseBO with old CourseEO
                _applicationUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException(" Course Doesn't Exists !");
            }
        }
    }
}
