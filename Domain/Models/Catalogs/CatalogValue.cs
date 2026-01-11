using System.Text.Json.Serialization;

namespace Domain.Models.Catalogs;

public class CatalogValue
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
    [JsonIgnore] public int CatalogId { get; set; }
    [JsonIgnore] public Catalog Catalog { get; set; } = null!;

    public static CatalogValue FromString(string value)
    {
        return new CatalogValue { Value = value };
    }
}