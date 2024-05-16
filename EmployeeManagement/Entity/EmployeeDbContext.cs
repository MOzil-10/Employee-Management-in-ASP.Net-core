using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Entity
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options) 
        {
        
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
