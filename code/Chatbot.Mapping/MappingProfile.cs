using AutoMapper;

namespace Chatbot.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Message, Models.Message>()
                .ForMember(destination => destination.GUID,opt => opt.Ignore())
                .ForMember(destination => destination.Timestamp, opt => opt.Ignore())
                .ForMember(destination => destination.MessageID, opt => opt.Ignore())
                .ForMember(destination => destination.Usermessage, opt => opt.Ignore());
            CreateMap<Models.Message, DTOs.Message>()
                .ForSourceMember(source => source.GUID, opt => opt.Ignore())
                .ForSourceMember(source => source.Timestamp, opt => opt.Ignore())
                .ForSourceMember(source => source.MessageID, opt => opt.Ignore())
                .ForSourceMember(source => source.Usermessage, opt => opt.Ignore());
        }
    }
}
