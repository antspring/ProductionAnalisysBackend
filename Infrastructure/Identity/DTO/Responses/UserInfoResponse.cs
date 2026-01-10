namespace ProductionAnalisysAPI.DTO.Responses;

public class UserInfoResponse
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required IList<string> Roles { get; set; }
}