using AutoMapper;
using eVault.Api.Models;
using eVault.Application.Mediator.Notifications;
using eVault.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public NotificationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var res = await _sender.Send(new GetAllNotificationsQuery());

            return Ok(_mapper.Map<IEnumerable<NotificationDto>>(res));
        }
        [HttpPost]
        public async Task<IActionResult> AddNotification(NotificationDto notificationDto)
        {
            var res = await _sender.Send(new AddNotificationCommand(_mapper.Map<Notification>(notificationDto)));

            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNotification(NotificationDto notificationDto)
        {
            var res = await _sender.Send(new UpdateNotificationCommand(_mapper.Map<Notification>(notificationDto)));

            return Ok(res);
        }
    }
}
