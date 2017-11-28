using AutoMapper;
using Chatbot.BusinessLayer.Interfaces;
using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.DataAccessLayer.Interfaces;
using Chatbot.DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Chatbot.BusinessLayer
{
    public class PluginConfigurationLogic : IPluginConfigurationLogic
    {
        private readonly IRepository<DataAccessLayer.Entities.PluginConfiguration> pluginConfigurationRepository;
        private readonly IMapper mapper;

        public PluginConfigurationLogic(IRepository<DataAccessLayer.Entities.PluginConfiguration> pluginConfigurationRepository, IMapper mapper)
        {
            this.pluginConfigurationRepository = pluginConfigurationRepository;
            this.mapper = mapper;
        }

        public void AddPluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationRepository.Create(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
        }

        public void DeletePluginConfiguration(string name, string key)
        {
            pluginConfigurationRepository.Delete(i => i.Name == name && i.Key == key);
        }

        public void DeletePluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationRepository.Delete(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations()
        {
            return mapper.Map<IEnumerable<PluginConfiguration>>(pluginConfigurationRepository.Read(i => true));
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations(IPlugin plugin)
        {
            return mapper.Map<IEnumerable<PluginConfiguration>>(pluginConfigurationRepository.Read(i => i.Name == plugin.Name));
        }

        public PluginConfiguration GetPluginConfiguration(IPlugin plugin, string key)
        {
            return GetPluginConfiguration(plugin.Name, key);
        }

        public PluginConfiguration GetPluginConfiguration(string name, string key)
        {
            return mapper.Map<PluginConfiguration>(pluginConfigurationRepository.Read(i => i.Name == name && i.Key == key).Single());
        }

        public void SavePluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationRepository.Update(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
        }
    }
}
