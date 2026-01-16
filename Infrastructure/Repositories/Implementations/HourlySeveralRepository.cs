using Application.DTO.Requests.HourlySeveral;
using Application.Repositories.Interfaces;
using Domain.Models.ProductionAnalysis.HourlySeveral;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class HourlySeveralRepository : IHourlySeveralRepository
{
    private ApplicationDbContext _dbContext;

    public HourlySeveralRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<HourlySeveralView>> GetAllThroughViewAsync()
    {
        return _dbContext.HourlySeveralViews
            .Include(v => v.Product1)
            .Include(v => v.Product2)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .ToListAsync();
    }

    public Task<HourlySeveralView?> GetByIdThroughViewAsync(int id)
    {
        return _dbContext.HourlySeveralViews
            .Include(v => v.Product1)
            .Include(v => v.Product2)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<HourlySeveral?> GetByIdAsync(int id)
    {
        return _dbContext.HourlySeveral.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<HourlySeveral> CreateAsync(HourlySeveral hourlySeveral)
    {
        await _dbContext.HourlySeveral.AddAsync(hourlySeveral);

        return hourlySeveral;
    }

    public void Update(HourlySeveral hourlySeveral,
        HourlySeveralUpdateRequest request)
    {
        var entry = _dbContext.Entry(hourlySeveral);

        foreach (var prop in typeof(HourlySeveralUpdateRequest).GetProperties())
        {
            var value = prop.GetValue(request);
            if (value is null)
                continue;
            entry.Property(prop.Name).CurrentValue = value;
            entry.Property(prop.Name).IsModified = true;
        }
    }

    public Task RemoveAsync(int id)
    {
        return _dbContext.HourlySeveral.Where(hb => hb.Id == id).ExecuteDeleteAsync();
    }
}