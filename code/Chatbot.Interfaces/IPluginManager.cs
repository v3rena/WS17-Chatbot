using Chatbot.Models;

namespace Chatbot.Interfaces
{
    public interface IPluginManager
    {
        System.Collections.Generic.IEnumerable<IPlugin> Plugins { get; }

        void Add(IPlugin plugin);

        void Add(string plugin);

        void Clear();

        IPlugin ChoosePlugin(Message message);
    }
}