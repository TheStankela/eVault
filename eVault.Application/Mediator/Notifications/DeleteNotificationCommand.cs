using AutoMapper;
using eVault.Domain.Constants;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Notifications
{
    public record DeleteNotificationCommand(Guid NotificationId) : IRequest<Result<int>>;

    internal class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Result<int>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteNotificationCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationDb = await _dbContext.Notifications
                .Active()
                .FirstOrDefaultAsync(_ => _.Id == request.NotificationId);

            if (notificationDb is null)
                return Result<int>.Conflict(ApplicationResources.GetResourceNotFoundString(nameof(Notification)));

            _dbContext.Remove(notificationDb);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0 ? Result<int>.Success(result) : Result<int>.Failure(ApplicationResources.ErrorSavingChanges);
        }
    }
}
