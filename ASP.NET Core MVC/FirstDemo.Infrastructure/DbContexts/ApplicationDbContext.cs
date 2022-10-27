using FirstDemo.Infrastructure.Entities;
using FirstDemo.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.DbContexts
{

    /*
    IdentityDbContext class basically provide by default login, logout , or auth features 
    */
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        private readonly string _connectingString;
        private readonly string _migrationAssemblyName;

        /*

         ## Create implicit RealationShip easly using DbSet<T> here .
         using DbSet<T> generic class  here, T class connect with DbContext class .
         Only which classes are set with DbSet<T> class, these classes are can perform CRUD operation in Database -

        */
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Result> Results { get; set; }

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


        //// Using Fluent API , define Table name and Binding :
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Define Topic Table name as Topics - 
            modelBuilder.Entity<Topic>().ToTable("Topics");

            //// Creating Composite primary key in Pivot table -
            modelBuilder.Entity<CourseRegistration>().HasKey(c => new { c.CourseId, c.StudentId });

            //// Explicitly create some relationShip : 

            modelBuilder.Entity<Course>()
                .HasMany(t => t.Topics)
                .WithOne(c => c.Course)
                .HasForeignKey(x => x.CourseId);


            modelBuilder.Entity<CourseRegistration>()
                .HasOne(c => c.Course)
                .WithMany(cs => cs.CourseStudents)
                .HasForeignKey(x => x.CourseId);


            modelBuilder.Entity<CourseRegistration>()
                .HasOne(s => s.Student)
                .WithMany(sc => sc.StudentCourses)
                .HasForeignKey(x => x.StudentId);



            //// For data Seeding in Student Table 
            
            modelBuilder.Entity<Student>().HasData(new StudentSeed().Students);



            base.OnModelCreating(modelBuilder);
        }


    }
}