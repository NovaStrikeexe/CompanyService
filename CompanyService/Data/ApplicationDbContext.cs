using CompanyService.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().ToTable("Companies");
        modelBuilder.Entity<Department>().ToTable("Departments");
        modelBuilder.Entity<Employee>().ToTable("Employees");
    }
}
