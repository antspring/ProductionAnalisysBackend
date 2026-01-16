namespace Application.DTO.Requests.HourlyByPower;

public class HourlyByPowerUpdateRequest
{
    public int? NameOfProductId { get; set; }
    public int? DepartmentId { get; set; }
    public int? PerformerId { get; set; }
    public int? ShiftId { get; set; }
    public int? WorkHourId { get; set; }
    public double? Power { get; set; }
    public double? DailyRate { get; set; }
    public double? Fact { get; set; }
    public DateOnly? Date { get; set; }
}