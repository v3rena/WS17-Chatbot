using AutoMapper;
using System;

namespace Chatbot.Mapping
{
    public class BusinessDataAccessMappingProfile : Profile
    {
        public BusinessDataAccessMappingProfile()
        {
            CreateMap<BusinessLayer.Models.Message, DataAccessLayer.Entities.Message>();
            CreateMap<DataAccessLayer.Entities.Message, BusinessLayer.Models.Message>();

            CreateMap<BusinessLayer.Models.SessionKey, DataAccessLayer.Entities.SessionKey>();
            CreateMap<DataAccessLayer.Entities.SessionKey, BusinessLayer.Models.SessionKey>();

            CreateMap<BusinessLayer.Models.PluginConfiguration, DataAccessLayer.Entities.PluginConfiguration>();
            CreateMap<DataAccessLayer.Entities.PluginConfiguration, BusinessLayer.Models.PluginConfiguration>();
        }
    }
}
