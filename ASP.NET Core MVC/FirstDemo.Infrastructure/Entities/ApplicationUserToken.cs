using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Entities
{
    internal class ApplicationUserToken : IdentityUserToken<Guid>
    {
    }
}
