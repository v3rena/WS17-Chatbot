using AutoMapper;
using System;

namespace Chatbot.Mapping
{
    public class ServiceBusinessMappingProfile : Profile
    {
        public ServiceBusinessMappingProfile()
        {
            CreateMap<ServiceLayer.DTOs.Message, BusinessLayer.Models.Message>()
                .ForMember(destination => destination.IsUserMessage, opt => opt.Ignore());
            CreateMap<BusinessLayer.Models.Message, ServiceLayer.DTOs.Message>()
                .ForSourceMember(source => source.IsUserMessage, opt => opt.Ignore());

            CreateMap<ServiceLayer.DTOs.SessionKey, BusinessLayer.Models.SessionKey>()
                .ForMember(i => i.Key, opt => opt.MapFrom(k => Guid.Parse(k.Key)));
            CreateMap<BusinessLayer.Models.SessionKey, ServiceLayer.DTOs.SessionKey>()
                .ForMember(i => i.Key, opt => opt.MapFrom(k => k.Key.ToString()));
        }
    }
}
