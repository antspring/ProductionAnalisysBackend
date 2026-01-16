using Application.DTO.Requests.HourlyByPower;
using Application.Repositories.Interfaces;
using Domain.Models.ProductionAnalysis.HourlyByPower;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class HourlyByPowerRepository : IHourlyByPowerRepository
{
    private ApplicationDbContext _dbContext;

    public HourlyByPowerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<HourlyByPowerView>> GetAllThroughViewAsync()
    {
        return _dbContext.HourlyByPowerViews
            .Include(v => v.NameOfProduct)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .ToListAsync();
    }

    public Task<HourlyByPowerView?> GetByIdThroughViewAsync(int id)
    {
        return _dbContext.HourlyByPowerViews.Include(v => v.NameOfProduct)
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.ProductionDocument)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<HourlyByPower?> GetByIdAsync(int id)
    {
        return _dbContext.HourlyByPower.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<HourlyByPower> CreateAsync(HourlyByPower hourlyByPower)
    {
        await _dbContext.HourlyByPower.AddAsync(hourlyByPower);

        return hourlyByPower;
    }

    public void Update(HourlyByPower hourlyByPower,
        HourlyByPowerUpdateRequest request)
    {
        var entry = _dbContext.Entry(hourlyByPower);

        foreach (var prop in typeof(HourlyByPowerUpdateRequest).GetProperties())
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
        return _dbContext.HourlyByPower.Where(hb => hb.Id == id).ExecuteDeleteAsync();
    }
}