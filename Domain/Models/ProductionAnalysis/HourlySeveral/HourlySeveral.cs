using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.HourlySeveral;

public class HourlySeveral
{
    public int Id { get; init; }

    public int Product1Id { get; set; }
    public CatalogValue Product1 { get; set; } = null!;

    public int Product2Id { get; set; }
    public CatalogValue Product2 { get; set; } = null!;

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public DateOnly Date { get; set; }

    public double CycleTime1 { get; set; }
    public double CycleTime2 { get; set; }

    public double DailyRate1 { get; set; }
    public double DailyRate2 { get; set; }

    public double Fact { get; set; }

    public double Changeover { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;
}