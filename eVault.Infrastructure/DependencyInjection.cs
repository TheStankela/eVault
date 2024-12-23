using eVault.Domain.Interfaces.Service;
using eVault.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eVault.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserStore, UserStore>();
            services.AddScoped<IUserStoreFactory, UserStoreFactory>();

            return services;
        }
    }
}
