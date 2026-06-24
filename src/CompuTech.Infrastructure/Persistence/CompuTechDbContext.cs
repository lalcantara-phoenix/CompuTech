using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence;

public class CompuTechDbContext(DbContextOptions<CompuTechDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
