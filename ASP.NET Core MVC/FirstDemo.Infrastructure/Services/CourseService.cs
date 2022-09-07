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
    }
}
