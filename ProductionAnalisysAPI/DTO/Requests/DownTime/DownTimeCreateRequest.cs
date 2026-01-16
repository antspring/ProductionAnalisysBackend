using Domain.Models.ProductionDownTime;

namespace ProductionAnalisysAPI.DTO.Requests.DownTime;

public class DownTimeCreateRequest
{
    public int DocumentId { get; set; }
    public int ResponsibleId { get; set; }
    public int ReasonGroupId { get; set; }
    public int ReasonId { get; set; }
    public string ActionTake { get; set; } = null!;
    public int DownTime { get; set; }

    public ProductionDownTime ToModel()
    {
        return new ProductionDownTime
        {
            DocumentId = DocumentId,
            ResponsibleId = ResponsibleId,
            ReasonGroupId = ReasonGroupId,
            ReasonId = ReasonId,
            ActionTake = ActionTake,
            DownTime = DownTime
        };
    }
}