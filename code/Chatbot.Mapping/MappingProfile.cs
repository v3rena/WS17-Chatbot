using AutoMapper;
using System;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Message, Models.Message>();
            CreateMap<DTOs.Message, Models.Message>().ReverseMap();

            CreateMap<DTOs.SessionKey, Models.SessionKey>()
                .ForMember(i => i.Key, opt => opt.MapFrom(k => Guid.Parse(k.Key)));
            CreateMap<Models.SessionKey, DTOs.SessionKey>()
                .ForMember(i => i.Key, opt => opt.MapFrom(k => k.Key.ToString()));
        }
    }
}
