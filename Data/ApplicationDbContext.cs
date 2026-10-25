using EmployeeDepartmentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartmentManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();



            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "HR" },
                new Department { Id = 3, Name = "Finance" },
                new Department { Id = 4, Name = "Marketing" }
            );



            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Ahmed Ali",
                    Email = "ahmed@gmail.com",
                    Salary = 8000,
                    DepartmentId = 1
                },

                new Employee
                {
                    Id = 2,
                    Name = "Sara Mohamed",
                    Email = "sara@gmail.com",
                    Salary = 9000,
                    DepartmentId = 2
                }
            );
        }
    }
}