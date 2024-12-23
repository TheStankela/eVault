using System.Security.Claims;
using eVault.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace eVault.Infrastructure.Repositories
{
    public class UserStore : IUserStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != null)
            {
                CurrentUserId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            }
        }
        public Guid CurrentUserId { get; set; }
    }

    public class UserStoreFactory : IUserStoreFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UserStoreFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUserStore GetUserStore()
        {
            return _serviceProvider.GetRequiredService<IUserStore>();
        }
    }
}
