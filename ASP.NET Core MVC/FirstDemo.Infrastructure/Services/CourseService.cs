using FirstDemo.Infrastructure.Repositories;
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
        CourseRepository<CourseEO> courseRepository;
        public void CreateCourse(CourseBO course)
        {
            course.SetProperClassStartDate();

            CourseEO courseEO = new CourseEO();

            //// In furure we can done this work using automapper feature . 
            courseEO.Title = course.Name;
            courseEO.Fees = course.Fees;
            courseEO.ClassStartDate = course.ClassStartDate;

            courseRepository.Add(courseEO);
        }
    }
}
