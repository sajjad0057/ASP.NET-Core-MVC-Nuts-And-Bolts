using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Repositories;
using FirstDemo.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseBO = FirstDemo.Infrastructure.BusinessObjects.Course;
using CourseEO = FirstDemo.Infrastructure.Entities.Course;

namespace FirstDemo.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private  IApplicationUnitOfWork _applicationUnitOfWork;
        public CourseService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void CreateCourse(CourseBO course)
        {
            course.SetProperClassStartDate();

            CourseEO courseEntity = new CourseEO();

            //// In furure we can done this work using automapper feature . 
            courseEntity.Title = course.Name;
            courseEntity.Fees = course.Fees;
            courseEntity.ClassStartDate = course.ClassStartDate;

            _applicationUnitOfWork.Courses.Add(courseEntity);
            _applicationUnitOfWork.Save();
        }


        public (int total , int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
                .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);


            IList<CourseBO> courses = new List<CourseBO>();
            foreach(CourseEO courseEO in results.data)
            {
                courses.Add(new CourseBO
                {
                    Id = courseEO.Id,
                    Name = courseEO.Title,
                    Fees = courseEO.Fees,
                    ClassStartDate = courseEO.ClassStartDate,
                });

            }

            return (results.total, results.totalDisplay, courses);


        }



        public void DeteleCourse(Guid id)
        {
            _applicationUnitOfWork.Courses.Remove(id);
        }

        public CourseBO GetCourse(Guid id)
        {
            CourseEO courseEO = _applicationUnitOfWork.Courses.GetById(id);

            CourseBO courseBO = new CourseBO();

            courseBO.Id = courseEO.Id;
            courseBO.Name = courseEO.Title;
            courseBO.Fees = courseEO.Fees;
            courseBO.ClassStartDate = courseEO.ClassStartDate;

            return courseBO;
        }

        public void EditCourse(CourseBO courseBO)
        {
            var courseEO = _applicationUnitOfWork.Courses.GetById(courseBO.Id);

            if(courseEO != null)
            {
                courseEO.Title = courseBO.Name;
                courseEO.Fees = courseBO.Fees;
                courseEO.ClassStartDate = courseBO.ClassStartDate;

                _applicationUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException(" Course Doesn't Exists !");
            }
        }
    }
}
