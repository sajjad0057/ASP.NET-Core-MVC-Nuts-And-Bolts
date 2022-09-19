using FirstDemo.Infrastructure.BusinessObjects;

namespace FirstDemo.Infrastructure.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
        (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, int pageSize, string searchText, string orderby);
    }
}