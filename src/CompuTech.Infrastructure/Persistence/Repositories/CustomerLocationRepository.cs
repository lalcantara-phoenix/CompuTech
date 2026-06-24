using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class CustomerLocationRepository : ICustomerLocationRepository
{
    private readonly CompuTechDbContext _context;

    public CustomerLocationRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerLocation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CustomerLocations
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<CustomerLocation>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return await _context.CustomerLocations
            .Where(l => l.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CustomerLocation location, CancellationToken cancellationToken = default)
    {
        await _context.CustomerLocations.AddAsync(location, cancellationToken);
    }

    public void Update(CustomerLocation location)
    {
        _context.CustomerLocations.Update(location);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.CustomerLocations
            .AnyAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
