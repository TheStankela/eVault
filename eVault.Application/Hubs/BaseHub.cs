using eVault.Domain.Interfaces.Service;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Hubs
{
    public partial class BaseHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IUserStore _userStore;

        public BaseHub(ApplicationDbContext dbContext, IUserStore userStore)
        {
            _dbContext = dbContext;
            _userStore = userStore;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var currentUserId = _userStore.CurrentUserId;

            if (currentUserId == Guid.Empty)
                return base.OnDisconnectedAsync(exception);

            _dbContext.Connections.RemoveRange(_dbContext.Connections.Where(p => p.UserId == currentUserId).ToList());
            _dbContext.SaveChangesAsync();

            return base.OnDisconnectedAsync(exception);
        }

        public async Task Authenticate()
        {
            var currentConnectionId = Context.ConnectionId;

            var currentUserId = _userStore.CurrentUserId;

            var user = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == currentUserId);

            if (user == null)
                return;

            Console.WriteLine("\n" + user.UserName + " logged in" + "\nConnectionId: " + currentConnectionId);

            Connection connection = new Connection
            {
                UserId = user.Id,
                User = new User { UserName =  user.UserName },
                ConnectionId = currentConnectionId,
                ConnectedAt = DateTime.UtcNow,
            };

            await _dbContext.Connections.AddAsync(connection);
            await _dbContext.SaveChangesAsync();

            await Clients.Others.SendAsync("userConnected", connection);
        }

        public async Task ReAuthenticate()
        {
            var currentConnectionId = Context.ConnectionId;

            var currentUserId = _userStore.CurrentUserId;

            var user = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == currentUserId);

            if (user == null)
                return;

            Console.WriteLine("\n" + user.UserName + " logged in" + "\nConnectionId: " + currentConnectionId);

            Connection connection = new Connection
            {
                UserId = user.Id,
                User = new User { UserName = user.UserName },
                ConnectionId = currentConnectionId,
                ConnectedAt = DateTime.UtcNow,
            };

            await _dbContext.Connections.AddAsync(connection);
            await _dbContext.SaveChangesAsync();

            await Clients.Caller.SendAsync("authenticateResponseSuccess", connection);
            await Clients.Others.SendAsync("userOn", connection);
        }

        public void LogOut(Guid userId)
        {
            //Check later - Potential security issue
            _dbContext.Connections.RemoveRange(_dbContext.Connections.Where(p => p.UserId == userId).ToList());
            _dbContext.SaveChanges();
        }
    }
}
