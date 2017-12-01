using Chatbot.DataAccessLayer.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chatbot.DataAccessLayer.Entities
{
    public class PluginConfiguration : IEntity
    {
        public int Id { get; set; }

        [Index("PluginNameAndKeyIsUnique", 1, IsUnique = true)]
        public string Name { get; set; }

        [Index("PluginNameAndKeyIsUnique", 2, IsUnique = true)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
