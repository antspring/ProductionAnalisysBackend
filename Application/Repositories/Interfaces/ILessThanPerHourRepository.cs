using Application.DTO.Requests.LessThanPerHour;
using Domain.Models.ProductionAnalysis.LessThanPerHour;

namespace Application.Repositories.Interfaces;

public interface ILessThanPerHourRepository
{
    public Task<List<LessThanPerHourView>> GetAllThroughViewAsync();
    public Task<LessThanPerHourView?> GetByIdThroughViewAsync(int id);
    public Task<LessThanPerHour?> GetByIdAsync(int id);
    public Task<LessThanPerHour> CreateAsync(LessThanPerHour hourlySeveral);

    public void Update(LessThanPerHour hourlySeveral,
        LessThanPerHourUpdateRequest request);

    public Task RemoveAsync(int id);
}