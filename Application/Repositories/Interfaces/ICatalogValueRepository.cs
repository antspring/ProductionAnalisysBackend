using Domain.Models.Catalogs;

namespace Application.Repositories.Interfaces;

public interface ICatalogValueRepository
{
    public Task<CatalogValue?> GetAsync(int catalogId, int id);
    public Task<CatalogValue?> CreateAsync(CatalogValue catalogValue);
    public Task RemoveAsync(int catalogId, int id);
}