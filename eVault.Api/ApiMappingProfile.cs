﻿using AutoMapper;
using eVault.Api.Models;

namespace eVault.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<NotificationDto, Domain.Models.Notification>();
            CreateMap<NotificationDto, Domain.Models.Notification>().ReverseMap();
        }
    }
}