namespace DataAccess.Models.Catalogs;

public class Catalog
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public List<CatalogValue> Values { get; set; } = [];
}