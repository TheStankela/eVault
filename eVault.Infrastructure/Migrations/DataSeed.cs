using eVault.Domain.Enums;
using eVault.Domain.Extensions;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVault.Infrastructure.Migrations
{
    public class DataSeed
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var seedingSection = configuration.GetSection("DatabaseSeeding");

            var useSeeding = ValidateSeedingSection(seedingSection);

            if (!useSeeding)
                return;

            //Seeding
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.EnsureCreatedAsync();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                await EnableUserAndRoleConstraints(context, true);

                var eUserRoles = new List<eUserRole>() { eUserRole.Administrator, eUserRole.User, eUserRole.Finance, eUserRole.Manager };

                var roles = eUserRoles.Select(role => role.GetDescription()).ToArray();
                foreach (var role in roles)
                {
                    if (!string.IsNullOrEmpty(role) && !await roleManager.RoleExistsAsync(role))
                    {
                        var roleResult = await roleManager.CreateAsync(new Role() { Name = role, IsActive = true });

                        if (!roleResult.Succeeded)
                            throw new Exception($"Failed to create role {role}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }

                var user = new User
                {
                    FirstName = "System",
                    LastName = "Admin",
                    Email = seedingSection["SystemEmail"],
                    EmailConfirmed = true,
                    IsActive = true
                };

                var userDb = await userManager.FindByNameAsync(user.UserName);

                if (userDb == null)
                {
                    var password = seedingSection["DefaultAdminPassword"];

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                        userDb = await context.Users.Where(_ => _.UserName == user.UserName).SingleOrDefaultAsync();
                    else
                        throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                if (!await context.UserRoles.AnyAsync(_ => _.UserId == userDb.Id))
                {
                    var roleAssignResult = await AssignRoles(userManager, userDb.Email, roles);

                    if (!roleAssignResult.Succeeded)
                        throw new Exception($"Error while assigning roles to user.");
                }

                if (roles.Any())
                    await context.Roles
                        .Where(_ => roles.Contains(_.Name))
                        .ExecuteUpdateAsync(_ => _.SetProperty(_ => _.CreatedById, userDb.Id));

                if(userDb != null)
                    await context.Users
                        .Where(_ => _.Id == userDb.Id)
                        .ExecuteUpdateAsync(user => user
                            .SetProperty(u => u.CreatedById, userDb.Id)
                            .SetProperty(u => u.CreatedOn, DateTime.UtcNow)
                            .SetProperty(u => u.UpdatedById, userDb.Id)
                            .SetProperty(u => u.UpdatedOn, DateTime.UtcNow));

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error while saving seed changes: ", ex);
            }
            finally
            {
                await EnableUserAndRoleConstraints(context, false);
            }

            await transaction.CommitAsync();

            Console.WriteLine("Data seed successful!");
        }

        private static async Task<IdentityResult> AssignRoles(UserManager<User> userManager, string email, string[] roles)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null && roles.Any())
                return await userManager.AddToRolesAsync(user, roles);

            throw new Exception($"User with email {email} not found");
        }

        private static async Task EnableUserAndRoleConstraints(ApplicationDbContext context, bool enable)
        {
            if (enable)
            {
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetRoles NOCHECK CONSTRAINT FK_AspNetRoles_AspNetUsers_CreatedById");
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetRoles NOCHECK CONSTRAINT FK_AspNetRoles_AspNetUsers_UpdatedById");

                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetUsers NOCHECK CONSTRAINT FK_AspNetUsers_AspNetUsers_CreatedById");
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetUsers NOCHECK CONSTRAINT FK_AspNetUsers_AspNetUsers_UpdatedById");
            }
            else
            {
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetRoles WITH CHECK CHECK CONSTRAINT FK_AspNetRoles_AspNetUsers_CreatedById");
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetRoles WITH CHECK CHECK CONSTRAINT FK_AspNetRoles_AspNetUsers_CreatedById");

                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetUsers WITH CHECK CHECK CONSTRAINT FK_AspNetUsers_AspNetUsers_UpdatedById");
                await context.Database.ExecuteSqlRawAsync("ALTER TABLE AspNetUsers WITH CHECK CHECK CONSTRAINT FK_AspNetUsers_AspNetUsers_UpdatedById");
            }
        }

        private static bool ValidateSeedingSection(IConfigurationSection section)
        {
            if (string.IsNullOrEmpty(section["SystemEmail"]))
                throw new Exception($"System email cannot be null or empty.");

            if (string.IsNullOrEmpty(section["DefaultAdminPassword"]))
                throw new Exception($"Default admin password hash cannot be null or empty.");

            var useSeeding = section["UseDatabaseSeeding"];

            if (string.IsNullOrEmpty(useSeeding))
                throw new Exception($"UseDatabaseSeeding cannot be null or empty.");

            return string.Equals(useSeeding.ToLower(), "true");
        }
    }
}
