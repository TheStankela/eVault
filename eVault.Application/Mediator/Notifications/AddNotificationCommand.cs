using AutoMapper;
using eVault.Infrastructure.Context;
using eVault.Infrastructure.Entities;
using MediatR;

namespace eVault.Application.Mediator.Notifications
{
    public record AddNotificationCommand(Domain.Models.Notification Notification) : IRequest<bool>;

    internal class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddNotificationCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            var dbNotification = _mapper.Map<Notification>(request.Notification);

            await _dbContext.AddAsync(dbNotification, cancellationToken);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
