using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Chatbot.Plugins.EchoBot

{
    public class EchoBotContext : DbContext
    {
        public EchoBotContext() : base("Chatbot")
        {

        }

        public DbSet<EchoBotTable> EchoBotTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("echobot");

            // Fluent API configuration
        }

    }
}