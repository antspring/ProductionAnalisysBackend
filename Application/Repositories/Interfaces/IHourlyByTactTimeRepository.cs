using Application.DTO.Requests.HourlyByTactTime;
using Domain.Models.ProductionAnalysis.HourlyByTactTime;

namespace Application.Repositories.Interfaces;

public interface IHourlyByTactTimeRepository
{
    public Task<List<HourlyByTactTimeView>> GetAllThroughViewAsync();
    public Task<HourlyByTactTimeView?> GetByIdThroughViewAsync(int id);
    public Task<HourlyByTactTime?> GetByIdAsync(int id);
    public Task<HourlyByTactTime> CreateAsync(HourlyByTactTime hourlyByTactTime);

    public void Update(HourlyByTactTime hourlyByTactTime,
        HourlyByTactTimeUpdateRequest request);

    public Task RemoveAsync(int id);
}