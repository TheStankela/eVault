using AutoMapper;
using eVault.Domain.Interfaces.Service;
using eVault.Domain.Models;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Users
{
    public record GetCurrentUserQuery : IRequest<User>;

    internal class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, User>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserStore _userStore;
        public GetCurrentUserHandler(ApplicationDbContext dbContext, IMapper mapper, IUserStore userStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userStore = userStore;
        }

        public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _userStore.CurrentUserId;
            
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(_ => _.Id == currentUserId);

            return _mapper.Map<User>(userDb);
        }
    }
}
