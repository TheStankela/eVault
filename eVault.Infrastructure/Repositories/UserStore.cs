using System.Security.Claims;
using eVault.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace eVault.Infrastructure.Repositories
{
    public class UserStore : IUserStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            CurrentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
        public string CurrentUserId { get; set; }
    }
}
