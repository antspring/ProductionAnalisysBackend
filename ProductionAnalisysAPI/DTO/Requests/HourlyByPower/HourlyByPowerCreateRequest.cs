namespace ProductionAnalisysAPI.DTO.Requests.HourlyByPower;

public class HourlyByPowerCreateRequest
{
    public int NameOfProductId { get; set; }
    public int DepartmentId { get; set; }
    public int PerformerId { get; set; }
    public int ShiftId { get; set; }
    public int WorkHourId { get; set; }
    public double Power { get; set; }
    public double DailyRate { get; set; }
    public double Fact { get; set; }
    public DateOnly Date { get; set; }

    public Domain.Models.ProductionAnalysis.HourlyByPower.HourlyByPower ToModel()
    {
        return new Domain.Models.ProductionAnalysis.HourlyByPower.HourlyByPower(
            NameOfProductId,
            DepartmentId,
            PerformerId,
            ShiftId,
            WorkHourId,
            Power,
            DailyRate,
            Fact,
            Date);
    }
}