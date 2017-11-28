using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using System.Collections.Generic;

namespace Chatbot.BusinessLayer.Interfaces
{
    public interface IBusinessLayer
    {
        void AddPluginConfiguration(PluginConfiguration pluginConfiguration);

        void DeletePluginConfiguration(string name, string key);

        void DeletePluginConfiguration(PluginConfiguration pluginConfiguration);

        SessionKey GenerateSession();

        IEnumerable<PluginConfiguration> GetPluginConfigurations();

        IEnumerable<PluginConfiguration> GetPluginConfigurations(IPlugin plugin);

        PluginConfiguration GetPluginConfiguration(IPlugin plugin, string key);

        PluginConfiguration GetPluginConfiguration(string name, string key);

        Message ProcessMessage(Message message);

        void SavePluginConfiguration(PluginConfiguration pluginConfiguration);
    }
}