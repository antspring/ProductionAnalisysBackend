using Domain.Models.ProductionDownTime;

namespace Application.Repositories.Interfaces;

public interface IDownTimeRepository
{
    public Task<List<ProductionDownTime>> GetByDocumentIdAsync(int documentId);
    public Task<ProductionDownTime> CreateAsync(ProductionDownTime downTime);
}