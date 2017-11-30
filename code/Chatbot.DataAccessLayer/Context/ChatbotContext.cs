using Chatbot.DataAccessLayer.Entities;
using System.Data.Entity;

namespace Chatbot.DataAccessLayer.Context
{
    public class ChatbotContext : DbContext
    {
        public ChatbotContext() : base("Chatbot")
        {

        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SessionKey> SessionKeys { get; set; }

        public DbSet<PluginConfiguration> PluginConfigurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("chatbot");

            // Fluent API configuration
        }

    }
}