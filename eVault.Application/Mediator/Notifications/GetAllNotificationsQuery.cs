using AutoMapper;
using eVault.Domain.Models;
using eVault.Domain.ResultWrapper;
using eVault.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eVault.Application.Mediator.Notifications
{
    public record GetAllNotificationsQuery : IRequest<Result<IEnumerable<Notification>>>;

    internal class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, Result<IEnumerable<Notification>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllNotificationsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<Notification>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _dbContext.Notifications.ToListAsync();

            return Result<IEnumerable<Notification>>.Success(_mapper.Map<IEnumerable<Notification>>(notifications));
        }
    }
}
