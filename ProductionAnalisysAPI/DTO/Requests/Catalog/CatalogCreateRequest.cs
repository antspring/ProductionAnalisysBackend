namespace ProductionAnalisysAPI.DTO.Requests.Catalog;

public class CatalogCreateRequest
{
    public required string Title { get; set; }
    public List<string> Values { get; set; } = [];
}