using AutoMapper;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Notifications
{
    public record UpdateNotificationCommand(Domain.Models.Notification Notification) : IRequest<bool>;

    internal class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateNotificationCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var dbNotification = await _dbContext.Notifications.FirstOrDefaultAsync(_ => _.Id == request.Notification.Id);

            if (dbNotification is null)
            {
                return false;
            }
            //_mapper.Map<Notification>(request.Notification);

            dbNotification.NotificationText = request.Notification.NotificationText;
            
            _dbContext.Update(dbNotification);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
