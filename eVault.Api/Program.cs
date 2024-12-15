using eVault.Api;
using eVault.Application;
using eVault.Infrastructure;
using eVault.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddDbContext<ApplicationDbContext>(
    opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("eVaultConnection")
));

builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
