using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace FirstDemo.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, Guid>, ICourseRepository
    {

        public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }


        public (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,
            int pageSize, string searchText, string orderby)
        {
            (IList<Course> data, int total, int totalDisplay) results =
                GetDynamic(x => x.Title.Contains(searchText), orderby, "Topics,CourseStudents", pageIndex, pageSize, true);

            return results;
        }



    }
}
