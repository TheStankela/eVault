using eVault.Api;
using eVault.Application;
using eVault.Infrastructure;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Entities;
using eVault.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

//Database
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(
    opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("eVaultConnection"))
    .AddInterceptors(new AuditInterceptor())
);

builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(_ => _.WithOrigins("localhost:4200", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials());
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
