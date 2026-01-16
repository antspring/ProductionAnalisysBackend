using Application.DTO.Requests.HourlyByPower;
using Domain.Models.ProductionAnalysis.HourlyByPower;

namespace Application.Repositories.Interfaces;

public interface IHourlyByPowerRepository
{
    public Task<List<HourlyByPowerView>> GetAllThroughViewAsync();
    public Task<HourlyByPowerView?> GetByIdThroughViewAsync(int id);
    public Task<HourlyByPower?> GetByIdAsync(int id);
    public Task<HourlyByPower> CreateAsync(HourlyByPower hourlyByPower);

    public void Update(HourlyByPower hourlyByPower,
        HourlyByPowerUpdateRequest request);

    public Task RemoveAsync(int id);
}