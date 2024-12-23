using AutoMapper;
using eVault.Domain.Constants;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Notifications
{
    public record AddNotificationCommand(Domain.Models.Notification Notification) : IRequest<Result<Notification>>;

    internal class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, Result<Notification>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddNotificationCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<Notification>> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationExists = await _dbContext.Notifications.FirstOrDefaultAsync(_ => _.Id == request.Notification.Id);

            if (notificationExists != null)
                return Result<Notification>.Conflict(ApplicationResources.GetResourceExistsString(nameof(Notification)));

            var dbNotification = _mapper.Map<Infrastructure.Entities.Notification>(request.Notification);

            await _dbContext.AddAsync(dbNotification, cancellationToken);

            await _dbContext.SaveChangesAsync();
            
            return dbNotification.Id != Guid.Empty ? Result<Notification>.Success(_mapper.Map<Notification>(dbNotification)) : Result<Notification>.Failure(ApplicationResources.ErrorSavingChanges) ;
        }
    }
}
