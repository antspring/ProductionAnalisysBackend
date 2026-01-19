using Application.Repositories.Interfaces;
using Domain.Models.ProductionDownTime;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class DownTimeRepository : IDownTimeRepository
{
    private ApplicationDbContext _dbContext { get; set; }

    public DownTimeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<ProductionDownTime>> GetAllAsync()
    {
        return _dbContext.ProductionDownTime
            .Include(pt => pt.ReasonGroup)
            .Include(pt => pt.Reason)
            .Include(pt => pt.Responsible)
            .ToListAsync();
    }

    public Task<List<ProductionDownTime>> GetByDocumentIdAsync(int documentId)
    {
        return _dbContext.ProductionDownTime
            .Where(pt => pt.DocumentId == documentId)
            .Include(pt => pt.ReasonGroup)
            .Include(pt => pt.Reason)
            .Include(pt => pt.Responsible)
            .ToListAsync();
    }

    public async Task<ProductionDownTime> CreateAsync(ProductionDownTime downTime)
    {
        await _dbContext.ProductionDownTime.AddAsync(downTime);

        return downTime;
    }
}