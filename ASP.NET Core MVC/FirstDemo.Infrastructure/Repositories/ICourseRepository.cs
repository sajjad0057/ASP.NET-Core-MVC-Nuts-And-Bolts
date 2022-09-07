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

    }
}
