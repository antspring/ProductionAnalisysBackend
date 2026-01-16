using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.HourlyByTactTime;

public class HourlyByTactTimeView
{
    public int Id { get; set; }

    public int NameOfProductId { get; set; }
    public CatalogValue NameOfProduct { get; set; } = null!;

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public double TactTime { get; set; }
    public double DailyRate { get; set; }
    public double Fact { get; set; }
    public DateOnly Date { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;

    public double Plan { get; set; }
    public double PlanCumulative { get; set; }
    public double FactCumulative { get; set; }
    public double Deviation { get; set; }
    public double DeviationCumulative { get; set; }
    public double TotalFact { get; set; }
    public double TotalPlan { get; set; }
}