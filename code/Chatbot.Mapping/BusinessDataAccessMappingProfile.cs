using AutoMapper;
using System;

namespace Chatbot.Mapping
{
    public class BusinessDataAccessMappingProfile : Profile
    {
        public BusinessDataAccessMappingProfile()
        {
            CreateMap<BusinessLayer.Models.Message, DataAccessLayer.Entities.Message>()
                .ForMember(destination => destination.Id, opt => opt.Ignore());
            CreateMap<DataAccessLayer.Entities.Message, BusinessLayer.Models.Message>()
                .ForSourceMember(source => source.Id, opt => opt.Ignore());

            CreateMap<BusinessLayer.Models.SessionKey, DataAccessLayer.Entities.SessionKey>()
                .ForMember(destination => destination.Id, opt => opt.Ignore());
            CreateMap<DataAccessLayer.Entities.SessionKey, BusinessLayer.Models.SessionKey>()
                .ForSourceMember(source => source.Id, opt => opt.Ignore());

            CreateMap<BusinessLayer.Models.PluginConfiguration, DataAccessLayer.Entities.PluginConfiguration>()
                .ForMember(destination => destination.Id, opt => opt.Ignore());
            CreateMap<DataAccessLayer.Entities.PluginConfiguration, BusinessLayer.Models.PluginConfiguration>()
                .ForSourceMember(source => source.Id, opt => opt.Ignore());
        }
    }
}
