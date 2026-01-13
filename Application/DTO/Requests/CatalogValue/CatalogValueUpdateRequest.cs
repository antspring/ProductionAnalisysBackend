namespace Application.DTO.Requests.CatalogValue;

public class CatalogValueUpdateRequest
{
    public int Id { get; set; }
    public int CatalogId { get; set; }
    public string Value { get; set; } = null!;
}