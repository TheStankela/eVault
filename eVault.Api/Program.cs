using eVault.Api;
using eVault.Api.Extensions;
using eVault.Application;
using eVault.Application.Hubs;
using eVault.Infrastructure;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Entities;
using eVault.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<User>(_ =>
    {
        _.SignIn = new SignInOptions { RequireConfirmedEmail = true };
        _.User = new UserOptions { RequireUniqueEmail = true };
        _.Password = new PasswordOptions { RequiredLength = 8 };
    })
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(
    opts => opts
            .UseSqlServer(builder.Configuration.GetConnectionString("eVaultConnection"))
            .AddInterceptors(new AuditInterceptor())
);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("ApplicationPolicy", _ => _.WithOrigins(builder.Configuration.GetSection("Cors")["Origins"])
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials());
});

var app = builder.Build();

app.UseDatabaseSeeding(builder.Configuration);

app.UseHttpsRedirection();

app.UseCors("ApplicationPolicy");

app.MapIdentityApi<User>();

app.Use(async (context, next) =>
{
    if (context.Request.Query.TryGetValue("access_token", out StringValues token))
        context.Request.Headers.Authorization = $"Bearer {token}";

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<BaseHub>("/hub");

app.Run();
