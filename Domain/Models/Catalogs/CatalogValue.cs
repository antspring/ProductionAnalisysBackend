namespace Domain.Models.Catalogs;

public class CatalogValue
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
    public int CatalogId { get; set; }
    public Catalog Catalog { get; set; } = null!;
}