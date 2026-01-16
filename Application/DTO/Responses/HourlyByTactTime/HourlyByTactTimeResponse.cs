using Domain.Models.Catalogs;
using Domain.Models.ProductionAnalysis.HourlyByTactTime;
using Domain.Models.ProductionDownTime;

namespace Application.DTO.Responses.HourlyByTactTime;

public class HourlyByTactTimeResponse
{
    public int Id { get; set; }

    public CatalogValue NameOfProduct { get; set; }

    public CatalogValue Department { get; set; }

    public CatalogValue Performer { get; set; }

    public CatalogValue Shift { get; set; }

    public CatalogValue WorkHour { get; set; }

    public double TactTime { get; set; }
    public double DailyRate { get; set; }
    public double Fact { get; set; }
    public DateOnly Date { get; set; }
    public ProductionDocument ProductionDocument { get; set; }
    public double Plan { get; set; }
    public double PlanCumulative { get; set; }
    public double FactCumulative { get; set; }
    public double Deviation { get; set; }
    public double DeviationCumulative { get; set; }
    public double TotalFact { get; set; }
    public double TotalPlan { get; set; }

    public HourlyByTactTimeResponse(HourlyByTactTimeView hourlyByTactTimeView)
    {
        Id = hourlyByTactTimeView.Id;
        NameOfProduct = hourlyByTactTimeView.NameOfProduct;
        Department = hourlyByTactTimeView.Department;
        Performer = hourlyByTactTimeView.Performer;
        Shift = hourlyByTactTimeView.Shift;
        WorkHour = hourlyByTactTimeView.WorkHour;
        TactTime = hourlyByTactTimeView.TactTime;
        DailyRate = hourlyByTactTimeView.DailyRate;
        Fact = hourlyByTactTimeView.Fact;
        Date = hourlyByTactTimeView.Date;
        ProductionDocument = hourlyByTactTimeView.ProductionDocument;
        Plan = hourlyByTactTimeView.Plan;
        PlanCumulative = hourlyByTactTimeView.PlanCumulative;
        FactCumulative = hourlyByTactTimeView.FactCumulative;
        Deviation = hourlyByTactTimeView.Deviation;
        DeviationCumulative = hourlyByTactTimeView.DeviationCumulative;
        TotalFact = hourlyByTactTimeView.TotalFact;
        TotalPlan = hourlyByTactTimeView.TotalPlan;
    }
}