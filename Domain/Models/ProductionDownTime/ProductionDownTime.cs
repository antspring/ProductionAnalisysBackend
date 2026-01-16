using Domain.Models.Catalogs;

namespace Domain.Models.ProductionDownTime;

public class ProductionDownTime
{
    public int Id { get; init; }
    public int DocumentId { get; set; }
    public ProductionDocument Document { get; set; } = null!;

    public int ResponsibleId { get; set; }
    public CatalogValue Responsible { get; set; } = null!;

    public int ReasonGroupId { get; set; }
    public CatalogValue ReasonGroup { get; set; } = null!;

    public int ReasonId { get; set; }
    public CatalogValue Reason { get; set; } = null!;
    
    public int DownTime { get; set; }
    public string ActionTake { get; set; } = null!;
}