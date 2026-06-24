using System.Reflection;
using CompuTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence;

public class CompuTechDbContext(DbContextOptions<CompuTechDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerLocation> CustomerLocations => Set<CustomerLocation>();
    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Technician> Technicians => Set<Technician>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
