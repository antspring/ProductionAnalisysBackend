using Domain.Models.Catalogs;
using Domain.Models.ProductionAnalysis.LessThanPerHour;
using Domain.Models.ProductionDownTime;

namespace Application.DTO.Responses.LessThanPerHour;

public class LessThanPerHourResponse
{
    public int Id { get; set; }
    public CatalogValue Department { get; set; }
    public CatalogValue Performer { get; set; }
    public DateOnly Date { get; set; }
    public CatalogValue Shift { get; set; }
    public CatalogValue WorkHour { get; set; }
    public CatalogValue OperationName { get; set; }

    public TimeOnly StartTimePlan { get; set; }
    public TimeOnly StartTimeFact { get; set; }
    public TimeOnly EndTimePlan { get; set; }
    public TimeOnly EndTimeFact { get; set; }

    public double Plan { get; set; }
    public double Fact { get; set; }

    public ProductionDocument ProductionDocument { get; set; }

    public double PlanCumulative { get; set; }
    public double FactCumulative { get; set; }
    public double Deviation { get; set; }
    public double DeviationCumulative { get; set; }

    public LessThanPerHourResponse(LessThanPerHourView lessThanPerHourView)
    {
        Id = lessThanPerHourView.Id;
        Department = lessThanPerHourView.Department;
        Performer = lessThanPerHourView.Performer;
        Date = lessThanPerHourView.Date;
        Shift = lessThanPerHourView.Shift;
        WorkHour = lessThanPerHourView.WorkHour;
        OperationName = lessThanPerHourView.OperationName;
        StartTimePlan = lessThanPerHourView.StartTimePlan;
        StartTimeFact = lessThanPerHourView.StartTimeFact;
        EndTimePlan = lessThanPerHourView.EndTimePlan;
        EndTimeFact = lessThanPerHourView.EndTimeFact;
        Plan = lessThanPerHourView.Plan;
        Fact = lessThanPerHourView.Fact;
        ProductionDocument = lessThanPerHourView.ProductionDocument;
        PlanCumulative = lessThanPerHourView.PlanCumulative;
        FactCumulative = lessThanPerHourView.FactCumulative;
        Deviation = lessThanPerHourView.Deviation;
        DeviationCumulative = lessThanPerHourView.DeviationCumulative;
    }
}