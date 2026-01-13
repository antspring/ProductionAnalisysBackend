using Application.DTO.Requests.CatalogValue;
using Application.UnitOfWork;

namespace Application.Services.CatalogValue;

public class CatalogValueService
{
    private IUnitOfWork _uow { get; set; }

    public CatalogValueService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<Domain.Models.Catalogs.CatalogValue?> GetAsync(int catalogId, int id)
    {
        return _uow.CatalogValues.GetAsync(catalogId, id);
    }

    public async Task<Domain.Models.Catalogs.CatalogValue?> CreateAsync(int catalogId, string value)
    {
        var catalogValue = new Domain.Models.Catalogs.CatalogValue(catalogId, value);

        catalogValue = await _uow.CatalogValues.CreateAsync(catalogValue);
        await _uow.SaveChangesAsync();

        return catalogValue;
    }

    public async Task<Domain.Models.Catalogs.CatalogValue?> UpdateAsync(
        CatalogValueUpdateRequest catalogValueUpdateRequest)
    {
        var catalogValue =
            await _uow.CatalogValues.GetAsync(catalogValueUpdateRequest.CatalogId, catalogValueUpdateRequest.Id);

        if (catalogValue is null)
            return null;

        catalogValue.ChangeValue(catalogValueUpdateRequest.Value);

        await _uow.SaveChangesAsync();
        return catalogValue;
    }

    public async Task RemoveAsync(int catalogId, int id)
    {
        await _uow.CatalogValues.RemoveAsync(catalogId, id);
        await _uow.SaveChangesAsync();
    }
}