using AutoMapper;
using eVault.Domain.Constants;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eVault.Application.Mediator.Roles
{
    public record AddUserRoleCommand(Role Role) : IRequest<Result<Role>>;

    internal class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, Result<Role>>
    {
        private readonly RoleManager<Infrastructure.Entities.Role> _roleManager;

        private readonly IMapper _mapper;

        public AddUserRoleCommandHandler(RoleManager<Infrastructure.Entities.Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Result<Role>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var roleToBeAdded = _mapper.Map<Infrastructure.Entities.Role>(request.Role);

            if (await _roleManager.RoleExistsAsync(roleToBeAdded.Name))
                return Result<Role>.Conflict(ApplicationResources.GetResourceExistsString(nameof(IdentityRole)));

            var result = await _roleManager.CreateAsync(roleToBeAdded);

            return result.Succeeded ?
                Result<Role>.Success(_mapper.Map<Role>(roleToBeAdded)) :
                Result<Role>.Failure(result.Errors.First()?.Description);
        }
    }
}
