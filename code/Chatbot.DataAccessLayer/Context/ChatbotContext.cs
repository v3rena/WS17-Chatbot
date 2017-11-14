using Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot.DataAccessLayer.Context
{
    public class ChatbotContext : DbContext
    {
        public ChatbotContext() : base("Chatbot")
        {

        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<SessionKey> SessionKeys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("chatbot");

            // Fluent API configuration
        }

    }
}