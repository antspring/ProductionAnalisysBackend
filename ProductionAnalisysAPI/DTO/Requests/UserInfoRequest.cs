namespace ProductionAnalisysAPI.DTO.Requests;

public class UserInfoRequest
{
    public string? NewUserName { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}