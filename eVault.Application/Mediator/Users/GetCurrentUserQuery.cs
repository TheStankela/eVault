using AutoMapper;
using eVault.Domain.Interfaces.Service;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Users
{
    public record GetCurrentUserQuery : IRequest<Result<User>>;

    internal class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, Result<User>>
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

        public async Task<Result<User>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _userStore.CurrentUserId;
            
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(_ => _.Id == currentUserId);

            if (userDb == null)
                return Result<User>.Unauthorized(new Error("Please log in to your account.", "Unauthorized"));

            return Result<User>.Success(_mapper.Map<User>(userDb));
        }
    }
}
