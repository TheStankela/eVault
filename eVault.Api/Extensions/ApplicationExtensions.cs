using eVault.Infrastructure.Migrations;

namespace eVault.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseDatabaseSeeding(this IApplicationBuilder app, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(app);

            var useSeeding = configuration.GetSection("DatabaseSeeding")["UseDatabaseSeeding"];
            if (!string.IsNullOrEmpty(useSeeding) && useSeeding.ToLower() == "true")
                Task.Run(async () =>
                {
                    await DataSeed.Initialize(app.ApplicationServices);
                });
            
            return app;
        }
    }
}
