using Chatbot.Common.Interfaces;
using System.Collections.Generic;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public interface IDataAccessLayer
    {
        string Name { get; }

        Dictionary<string, string> GetPluginConfiguration(IPlugin plugin);

        void SavePluginConfiguration(IPlugin plugin, Dictionary<string, string> configuration);
    }
}