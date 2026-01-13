using Application.Repositories.Interfaces;
using Domain.Models.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class CatalogValueRepository : ICatalogValueRepository
{
    private ApplicationDbContext _dbContext;

    public CatalogValueRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<CatalogValue?> GetAsync(int catalogId, int id)
    {
        return _dbContext.Catalogs.Where(c => c.Id == catalogId).SelectMany(c => c.Values)
            .FirstOrDefaultAsync(cv => cv.Id == id);
    }

    public async Task<CatalogValue?> CreateAsync(CatalogValue catalogValue)
    {
        var catalog = await _dbContext.Catalogs.FirstOrDefaultAsync(c => c.Id == catalogValue.CatalogId);

        if (catalog is null)
            return null;

        catalog.Values.Add(catalogValue);

        return catalogValue;
    }

    public async Task RemoveAsync(int catalogId, int id)
    {
        var catalog = await _dbContext.Catalogs.Where(c => c.Id == catalogId).Select(c => new
        {
            Catalog = c,
            Value = c.Values.FirstOrDefault(v => v.Id == id)
        }).FirstOrDefaultAsync();

        if (catalog?.Value is null)
            return;

        catalog.Catalog.Values.Remove(catalog.Value);
    }
}