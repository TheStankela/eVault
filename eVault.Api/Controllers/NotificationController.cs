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
       
        [HttpGet]
        public Task<IActionResult> GetAllNotifications() =>
            _sender.Send(new GetAllNotificationsQuery())
            .Map(notifications => _mapper.Map<IEnumerable<NotificationDto>>(notifications))
            .ToObjectResult();

        [Authorize]
        [HttpPost]
        public Task<IActionResult> AddNotification(NotificationDto notificationDto) => 
            _sender.Send(new AddNotificationCommand(_mapper.Map<Notification>(notificationDto)))
            .Map(notifications => _mapper.Map<NotificationDto>(notifications))
            .ToObjectResult();

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotification(NotificationDto notificationDto)
        {
            var res = await _sender.Send(new UpdateNotificationCommand(_mapper.Map<Notification>(notificationDto)));

            return Ok(res);
        }

        [Authorize]
        [HttpDelete("{notificationId}")]
        public Task<IActionResult> DeleteNotification(Guid notificationId) =>
            _sender.Send(new DeleteNotificationCommand(notificationId))
            .ToObjectResult();
    }
}
