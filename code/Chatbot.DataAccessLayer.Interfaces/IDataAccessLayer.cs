using Chatbot.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace Chatbot.DataAccessLayer.Interfaces
{
    public interface IDataAccessLayer
    {
        IRepository<T> GetRepository<T>() where T : IEntity;

        //string Name { get; }

        //Dictionary<string, string> GetPluginConfiguration(IPlugin plugin);

        //void SavePluginConfiguration(IPlugin plugin, Dictionary<string, string> configuration);
    }
}