using Domain.Models.Catalogs;

namespace Application.Repositories.Interfaces;

public interface ICatalogRepository
{
    public Task<List<Catalog>> GetAllAsync();
    public Task<Catalog?> GetWithValuesAsync(int id);
    public Task<Catalog?> GetAsync(int id);
    public Task<Catalog> CreateAsync(Catalog catalog);
    public Task RemoveAsync(int id);
}