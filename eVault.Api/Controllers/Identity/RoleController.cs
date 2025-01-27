using AutoMapper;
using eVault.Api.Attributes;
using eVault.Api.Models;
using eVault.Application.Mediator.Roles;
using eVault.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eVault.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ISender _sender;

        private readonly IMapper _mapper;

        public RoleController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [AuthorizeRole(eUserRole.Administrator)]
        [HttpPost]
        public Task<IActionResult> AddRole(RoleDto roleDto) =>
            _sender.Send(new AddUserRoleCommand(_mapper.Map<Domain.Models.Role>(roleDto)))
            .Map(role => _mapper.Map<RoleDto>(role))
            .ToObjectResult();
    }
}
