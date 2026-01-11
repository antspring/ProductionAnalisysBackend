using Application.UnitOfWork;
using Domain.Models.Catalogs;

namespace Application.Services.Catalog;

public class CatalogService
{
    private readonly IUnitOfWork _uow;

    public CatalogService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<List<Domain.Models.Catalogs.Catalog>> GetAllAsync()
    {
        return _uow.Catalogs.GetAllAsync();
    }

    public Task<Domain.Models.Catalogs.Catalog?> GetWithValuesAsync(int id)
    {
        return _uow.Catalogs.GetWithValuesAsync(id);
    }

    public async Task<Domain.Models.Catalogs.Catalog> CreateAsync(string title, List<string> values)
    {
        var catalog = new Domain.Models.Catalogs.Catalog
        {
            Title = title,
            Values = values.Select(CatalogValue.FromString).ToList()
        };

        catalog = await _uow.Catalogs.CreateAsync(catalog);
        await _uow.SaveChangesAsync();

        return catalog;
    }

    public async Task<Domain.Models.Catalogs.Catalog?> UpdateAsync(int id, string title)
    {
        var catalog = await _uow.Catalogs.GetAsync(id);
        if (catalog is null)
            return null;

        catalog.Title = title;
        await _uow.SaveChangesAsync();

        return catalog;
    }

    public Task RemoveAsync(int id)
    {
        return _uow.Catalogs.RemoveAsync(id);
    }
}