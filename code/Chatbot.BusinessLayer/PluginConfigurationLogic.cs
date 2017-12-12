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
        private readonly IPluginManager pluginManager;
        private readonly IMapper mapper;

        public PluginConfigurationLogic(IRepository<DataAccessLayer.Entities.PluginConfiguration> pluginConfigurationRepository, IPluginManager pluginManager, IMapper mapper)
        {
            this.pluginConfigurationRepository = pluginConfigurationRepository;
            this.pluginManager = pluginManager;
            this.mapper = mapper;
        }

        public void AddPluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationRepository.Create(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
            NotifyChanges(pluginConfiguration.Name);
        }

        public void DeletePluginConfiguration(string name, string key)
        {
            pluginConfigurationRepository.Delete(i => i.Name == name && i.Key == key);
            NotifyChanges(name);
        }

        public void DeletePluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationRepository.Delete(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
            NotifyChanges(pluginConfiguration.Name);
        }

        public void DeletePluginConfigurations(IPlugin plugin)
        {
            pluginConfigurationRepository.Delete(i => i.Name == plugin.Name);
            NotifyChanges(plugin);
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations()
        {
            return mapper.Map<IEnumerable<PluginConfiguration>>(pluginConfigurationRepository.Read(i => true));
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations(IPlugin plugin)
        {
            return GetPluginConfigurations(plugin.Name);
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations(string name)
        {
            return mapper.Map<IEnumerable<PluginConfiguration>>(pluginConfigurationRepository.Read(i => i.Name == name));
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
            pluginConfigurationRepository.Save(mapper.Map<DataAccessLayer.Entities.PluginConfiguration>(pluginConfiguration));
            NotifyChanges(pluginConfiguration.Name);
        }

        public void SavePluginConfigurations(IEnumerable<PluginConfiguration> pluginConfigurations)
        {
            foreach(PluginConfiguration p in pluginConfigurations)
            {
                SavePluginConfiguration(p);
            }
        }

        private void NotifyChanges(IPlugin plugin)
        {
            pluginManager.NotifyChanges(plugin);
        }

        private void NotifyChanges(string pluginName)
        {
            pluginManager.NotifyChanges(pluginName);
        }
    }
}
