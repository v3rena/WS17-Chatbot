using AutoMapper;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Interfaces.DTOs.IMessage, Interfaces.Models.IMessage>().ReverseMap();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
