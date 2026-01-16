using Application.DTO.Requests.HourlySeveral;
using Domain.Models.ProductionAnalysis.HourlySeveral;

namespace Application.Repositories.Interfaces;

public interface IHourlySeveralRepository
{
    public Task<List<HourlySeveralView>> GetAllThroughViewAsync();
    public Task<HourlySeveralView?> GetByIdThroughViewAsync(int id);
    public Task<HourlySeveral?> GetByIdAsync(int id);
    public Task<HourlySeveral> CreateAsync(HourlySeveral hourlySeveral);

    public void Update(HourlySeveral hourlySeveral,
        HourlySeveralUpdateRequest request);

    public Task RemoveAsync(int id);
}