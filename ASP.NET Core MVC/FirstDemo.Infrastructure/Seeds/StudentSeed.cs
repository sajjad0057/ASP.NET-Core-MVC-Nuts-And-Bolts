using FirstDemo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Seeds
{
    internal class StudentSeed
    {
        internal Student[] Students
        {
            get
            {
                return new Student[]
                {
                    new Student { Id = Guid.NewGuid(),Name="Student 1",Address = "Dhaka",Cgpa = 3.0 },
                    new Student { Id = Guid.NewGuid(),Name="Student 2",Address = "Rangpur",Cgpa = 3.50 },
                    new Student { Id = Guid.NewGuid(),Name="Student 3",Address = "Dhaka",Cgpa = 3.70 },
                    new Student { Id = Guid.NewGuid(),Name="Student 4",Address = "Natore",Cgpa = 3.50 },
                };
            }
        } 
    }
}
