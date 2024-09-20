using HR.Core;
using Microsoft.EntityFrameworkCore;

namespace HR.Data;
public class HRDbContext : DbContext
{
    public HRDbContext(DbContextOptions<HRDbContext> options)
        : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
}
