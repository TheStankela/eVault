using AutoMapper;
using eVault.Domain.Interfaces.Service;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.MenuItems
{
    public record GetAllMenuItemsQuery : IRequest<Result<IEnumerable<MenuItem>>>;

    internal class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuItemsQuery, Result<IEnumerable<MenuItem>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserStore _userStore;
        public GetAllMenuItemsQueryHandler(ApplicationDbContext dbContext, IMapper mapper, IUserStore userStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userStore = userStore;
        }
        public async Task<Result<IEnumerable<MenuItem>>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _dbContext.UserRoles
                .Where(_ => _.UserId == _userStore.CurrentUserId)
                .Select(_ => _.RoleId)
                .ToListAsync();

            if (userRoles == null || !userRoles.Any())
                return Result<IEnumerable<MenuItem>>.Unauthorized();

            var menuItems = await _dbContext.MenuItems
                .Where(_ => userRoles.Contains(_.Id))
                .Include(m => m.Items)
                .Include(m => m.Parent)
                .ToListAsync();

            return Result<IEnumerable<MenuItem>>.Success(_mapper.Map<IEnumerable<MenuItem>>(menuItems));
        }
    }
}
