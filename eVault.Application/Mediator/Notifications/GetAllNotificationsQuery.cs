using AutoMapper;
using eVault.Domain.Models;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Notifications
{
    public record GetAllNotificationsQuery : IRequest<IEnumerable<Notification>>;

    internal class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, IEnumerable<Notification>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllNotificationsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _dbContext.Notifications.ToListAsync();
           
            return _mapper.Map<IEnumerable<Notification>>(notifications);
        }
    }
}
