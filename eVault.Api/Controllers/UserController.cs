using AutoMapper;
using eVault.Api.Models;
using eVault.Application.Mediator.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public UserController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("current")]
        public Task<IActionResult> GetCurrentUser() =>
            _sender.Send(new GetCurrentUserQuery())
            .ToObjectResult();
    }
}
