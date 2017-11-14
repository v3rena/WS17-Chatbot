using AutoMapper;
using System;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Message, Models.Message>()
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(destination => destination.IsUserMessage, opt => opt.Ignore());
            CreateMap<Models.Message, DTOs.Message>()
                .ForSourceMember(source => source.Id, opt => opt.Ignore())
                .ForSourceMember(source => source.IsUserMessage, opt => opt.Ignore());

            CreateMap<DTOs.SessionKey, Models.SessionKey>()
                .ForMember(destination => destination.Id, opt => opt.Ignore())
                .ForMember(i => i.Key, opt => opt.MapFrom(k => Guid.Parse(k.Key)));
            CreateMap<Models.SessionKey, DTOs.SessionKey>()
                .ForSourceMember(source => source.Id, opt => opt.Ignore())
                .ForMember(i => i.Key, opt => opt.MapFrom(k => k.Key.ToString()));
        }
    }
}
