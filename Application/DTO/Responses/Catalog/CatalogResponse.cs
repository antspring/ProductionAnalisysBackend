namespace Application.DTO.Responses;

public class CatalogResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public List<string> Values { get; set; } = [];

    public CatalogResponse(Domain.Models.Catalogs.Catalog catalog)
    {
        Id = catalog.Id;
        Title = catalog.Title;
        Values = catalog.Values.Select(v => v.Value).ToList();
    }
}