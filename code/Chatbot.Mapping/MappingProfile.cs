using AutoMapper;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Interfaces.DTOs.IMessage, Interfaces.Models.IMessage>().As<Models.Message>();
            CreateMap<Interfaces.DTOs.IMessage, Interfaces.Models.IMessage>().ReverseMap().As<DTOs.Message>();
        }
    }
}
