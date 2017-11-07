using AutoMapper;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Message, Models.Message>();
            CreateMap<DTOs.Message, Models.Message>().ReverseMap();
        }
    }
}
