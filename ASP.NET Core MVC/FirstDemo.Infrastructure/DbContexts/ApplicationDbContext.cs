using FirstDemo.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.DbContexts
{

    /*
    IdentityDbContext class basically provide by default login, logout , or auth features 
    */
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly string _connectingString;
        private readonly string _migrationAssemblyName;


        //// using DbSet<T> generic class  here, T class connect with DbContext class .
        //// Only which classes are set with DbSet<T> class, these classes are can perform CRUD operation in Database
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(string connectingString, string migrationAssemblyName)
        {
            _connectingString = connectingString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        //// DbContextOptionsBuilder  Provides a simple API surface for configuring DbContextOptions.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectingString, m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(optionsBuilder);
        }



    }
}