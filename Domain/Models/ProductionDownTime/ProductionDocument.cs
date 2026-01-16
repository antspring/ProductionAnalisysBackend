namespace Domain.Models.ProductionDownTime;

public class ProductionDocument
{
    public int Id { get; init; }
    public ProductionDocumentType DocumentType { get; init; }
    public List<ProductionDownTime> DownTimes { get; init; } = null!;
}