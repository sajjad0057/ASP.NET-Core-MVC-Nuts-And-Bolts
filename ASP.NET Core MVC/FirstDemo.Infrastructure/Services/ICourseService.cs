using FirstDemo.Infrastructure.BusinessObjects;

namespace FirstDemo.Infrastructure.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
        void EditCourse(Course courseBO);
        Course GetCourse(Guid id);
        (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, int pageSize, string searchText, string orderby);

        IList<Course> GetCourses();
        Course GetCourse(string name);
        Course GetCourses(Guid id);
        void DeleteCourse(Guid id);
    }
}