using AutoMapper;

namespace eVault.Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Domain.Models.Notification, Infrastructure.Entities.Notification>();
            CreateMap<Domain.Models.Notification, Infrastructure.Entities.Notification>().ReverseMap();
        }
    }
}
