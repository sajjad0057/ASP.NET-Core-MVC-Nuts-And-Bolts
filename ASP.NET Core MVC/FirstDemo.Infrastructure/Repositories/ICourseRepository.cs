using FirstDemo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Repositories
{
    public interface ICourseRepository : IRepository<Course,Guid>
    {
        (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,int pageSize, string searchText, string orderby);

    }
}
