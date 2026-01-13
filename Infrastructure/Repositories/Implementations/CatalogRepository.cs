using Application.Repositories.Interfaces;
using Domain.Models.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CatalogRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Catalog>> GetAllAsync()
    {
        return _dbContext.Catalogs.Include(c => c.Values).ToListAsync();
    }

    public Task<Catalog?> GetWithValuesAsync(int id)
    {
        return _dbContext.Catalogs.Include(c => c.Values).FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Catalog?> GetAsync(int id)
    {
        return _dbContext.Catalogs.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Catalog> CreateAsync(Catalog catalog)
    {
        await _dbContext.Catalogs.AddAsync(catalog);
        return catalog;
    }

    public Task RemoveAsync(int id)
    {
        return _dbContext.Catalogs.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
}