using Chatbot.DataAccessLayer.Interfaces;

namespace Chatbot.DataAccessLayer.Entities
{
    public class PluginConfiguration : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
