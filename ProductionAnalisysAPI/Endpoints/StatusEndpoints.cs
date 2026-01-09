using DataAccess.Repositories.Interfaces;

namespace ProductionAnalisysAPI.Endpoints;

public static class StatusEndpoints
{
    public static void MapStatusEndpoints(this WebApplication app)
    {
        var statusEndpoints = app.MapGroup("/status");

        statusEndpoints.MapGet("/", async (IStatusRepository statusRepository) => await statusRepository.GetAllAsync());
    }
}