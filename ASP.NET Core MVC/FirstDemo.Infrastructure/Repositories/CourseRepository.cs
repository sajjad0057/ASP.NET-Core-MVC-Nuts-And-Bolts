using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course, Guid>,ICourseRepository
    {
        public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
