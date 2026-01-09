namespace ProductionAnalisysAPI.DTO.Requests;

public class ChangeStatusRequest
{
    public string Email { get; set; }
    public int StatusId { get; set; }
}