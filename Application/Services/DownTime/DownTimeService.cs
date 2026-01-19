using Application.UnitOfWork;
using Domain.Models.ProductionDownTime;

namespace Application.Services.DownTime;

public class DownTimeService
{
    private IUnitOfWork _uow { get; set; }

    public DownTimeService(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public Task<List<ProductionDownTime>> GetAllAsync()
    {
        return _uow.DownTime.GetAllAsync();
    }

    public Task<List<ProductionDownTime>> GetByDocumentIdAsync(int documentId)
    {
        return _uow.DownTime.GetByDocumentIdAsync(documentId);
    }

    public async Task<ProductionDownTime> CreateAsync(ProductionDownTime downTime)
    {
        downTime = await _uow.DownTime.CreateAsync(downTime);
        await _uow.SaveChangesAsync();

        return downTime;
    }
}