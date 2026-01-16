using Infrastructure;
using ProductionAnalisysAPI;
using ProductionAnalisysAPI.Endpoints;
using Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddDependencyInjectionServices();

var app = builder.Build();

await SeederManager.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapCustomIdentityApi<ApplicationUser>();

app.MapPasswordGenerateEndpoints();
app.MapUserEndpoints();
app.MapCatalogEndpoints();
app.MapHourlyByTactTimeEndpoints();

app.UseHttpsRedirection();

app.Run();