using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.LessThanPerHour;

public class LessThanPerHourView
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public int OperationNameId { get; set; }
    public CatalogValue OperationName { get; set; } = null!;

    public TimeOnly StartTimePlan { get; set; }
    public TimeOnly StartTimeFact { get; set; }
    public TimeOnly EndTimePlan { get; set; }
    public TimeOnly EndTimeFact { get; set; }

    public double Plan { get; set; }
    public double Fact { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;

    public double PlanCumulative { get; set; }
    public double FactCumulative { get; set; }
    public double Deviation { get; set; }
    public double DeviationCumulative { get; set; }
    public string Status { get; set; } = null!;
}