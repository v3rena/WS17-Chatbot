using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System.Collections.Generic;

namespace Chatbot.BusinessLayer.Interfaces
{
    public interface IPluginConfigurationLogic
    {
        void AddPluginConfiguration(PluginConfiguration pluginConfiguration);

        void DeletePluginConfiguration(string name, string key);

        void DeletePluginConfiguration(PluginConfiguration pluginConfiguration);

        void DeletePluginConfigurations(IPlugin plugin);

        IEnumerable<PluginConfiguration> GetPluginConfigurations();

        IEnumerable<PluginConfiguration> GetPluginConfigurations(IPlugin plugin);

        PluginConfiguration GetPluginConfiguration(IPlugin plugin, string key);

        PluginConfiguration GetPluginConfiguration(string name, string key);

        void SavePluginConfiguration(PluginConfiguration pluginConfiguration);

        void SavePluginConfigurations(IEnumerable<PluginConfiguration> pluginConfigurations);
    }
}