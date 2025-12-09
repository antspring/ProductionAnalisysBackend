using DataAccess.Models;
using ProductionAnalisysAPI;
using ProductionAnalisysAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddIdentity();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyInjectionServices();

var app = builder.Build();

await SeederManager.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();

app.MapPasswordGenerateEndpoints();

app.UseHttpsRedirection();

app.Run();