using System.Text.Json.Serialization;

namespace Domain.Models.Catalogs;

public class CatalogValue
{
    public int Id { get; private set; }
    public string Value { get; private set; } = null!;
    [JsonIgnore] public int CatalogId { get; private set; }
    [JsonIgnore] public Catalog Catalog { get; private set; } = null!;

    public CatalogValue()
    {
    }

    public CatalogValue(int catalogId, string value)
    {
        CatalogId = catalogId;
        Value = value;
    }

    public static CatalogValue FromString(string value)
    {
        return new CatalogValue { Value = value };
    }

    public void ChangeValue(string value)
    {
        Value = value;
    }
}