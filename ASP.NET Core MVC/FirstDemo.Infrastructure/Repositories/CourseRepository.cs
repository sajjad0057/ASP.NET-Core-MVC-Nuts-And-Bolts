using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace FirstDemo.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, Guid>, ICourseRepository
    {

        public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }


        public (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,
            int pageSize,string searchText,string orderby)
        {
            (IList<Course> data, int total, int totalDisplay) results = 
                GetDynamic(x => x.Title.Contains(searchText), orderby, "Topics,CourseStudents", pageIndex, pageSize, true);

            return results;
        }

        

    }
}
