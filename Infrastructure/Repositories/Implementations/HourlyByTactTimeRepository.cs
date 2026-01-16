using Application.DTO.Requests.HourlyByTactTime;
using Application.Repositories.Interfaces;
using Domain.Models.ProductionAnalysis.HourlyByTactTime;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class HourlyByTactTimeRepository : IHourlyByTactTimeRepository
{
    private ApplicationDbContext _dbContext;

    public HourlyByTactTimeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<HourlyByTactTimeView>> GetAllThroughViewAsync()
    {
        return _dbContext.HourlyByTactTimeViews
            .Include(v => v.NameOfProduct)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .ToListAsync();
    }

    public Task<HourlyByTactTimeView?> GetByIdThroughViewAsync(int id)
    {
        return _dbContext.HourlyByTactTimeViews.Include(v => v.NameOfProduct)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<HourlyByTactTime?> GetByIdAsync(int id)
    {
        return _dbContext.HourlyByTactTime.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<HourlyByTactTime> CreateAsync(HourlyByTactTime hourlyByTactTime)
    {
        await _dbContext.HourlyByTactTime.AddAsync(hourlyByTactTime);

        return hourlyByTactTime;
    }

    public void Update(HourlyByTactTime hourlyByTactTime,
        HourlyByTactTimeUpdateRequest request)
    {
        var entry = _dbContext.Entry(hourlyByTactTime);

        foreach (var prop in typeof(HourlyByTactTimeUpdateRequest).GetProperties())
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
        return _dbContext.HourlyByTactTime.Where(hb => hb.Id == id).ExecuteDeleteAsync();
    }
}