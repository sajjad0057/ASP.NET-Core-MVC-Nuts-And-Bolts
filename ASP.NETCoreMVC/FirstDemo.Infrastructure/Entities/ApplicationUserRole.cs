using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Infrastructure.Entities
{
    public class ApplicationUserRole
        : IdentityUserRole<Guid>
    {
       
    }
}
