using Chatbot.BusinessLayer.Interfaces;
using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System.Collections.Generic;

namespace Chatbot.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IMessagingLogic messagingLogic;
        private readonly ISessionLogic sessionLogic;
        private readonly IPluginConfigurationLogic pluginConfigurationLogic;

        public BusinessLayer(IMessagingLogic messagingLogic, ISessionLogic sessionLogic, IPluginConfigurationLogic pluginConfigurationLogic)
        {
            this.messagingLogic = messagingLogic;
            this.sessionLogic = sessionLogic;
            this.pluginConfigurationLogic = pluginConfigurationLogic;
        }

        public Message ProcessMessage(Message message)
        {
            return messagingLogic.ProcessMessage(message);
        }

        public SessionKey GenerateSession()
        {
            return sessionLogic.GenerateSession();
        }

        public void AddPluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationLogic.AddPluginConfiguration(pluginConfiguration);
        }

        public void DeletePluginConfiguration(string name, string key)
        {
            pluginConfigurationLogic.DeletePluginConfiguration(name, key);
        }

        public void DeletePluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationLogic.DeletePluginConfiguration(pluginConfiguration);
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations()
        {
            return pluginConfigurationLogic.GetPluginConfigurations();
        }

        public IEnumerable<PluginConfiguration> GetPluginConfigurations(IPlugin plugin)
        {
            return pluginConfigurationLogic.GetPluginConfigurations(plugin);
        }

        public PluginConfiguration GetPluginConfiguration(IPlugin plugin, string key)
        {
            return pluginConfigurationLogic.GetPluginConfiguration(plugin, key);
        }

        public PluginConfiguration GetPluginConfiguration(string name, string key)
        {
            return pluginConfigurationLogic.GetPluginConfiguration(name, key);
        }

        public void SavePluginConfiguration(PluginConfiguration pluginConfiguration)
        {
            pluginConfigurationLogic.SavePluginConfiguration(pluginConfiguration);
        }
    }
}