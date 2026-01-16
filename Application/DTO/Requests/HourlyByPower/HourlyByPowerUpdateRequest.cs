namespace Application.DTO.Requests.HourlyByPower;

public class HourlyByPowerUpdateRequest
{
    public int? NameOfProductId { get; set; }
    public int? DepartmentId { get; set; }
    public int? PerformerId { get; set; }
    public int? ShiftId { get; set; }
    public int? WorkHourId { get; set; }
    public int? Power { get; set; }
    public int? DailyRate { get; set; }
    public int? Fact { get; set; }
    public DateOnly? Date { get; set; }
}