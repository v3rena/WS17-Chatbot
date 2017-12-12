using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Plugins.RPGPlugin
{
   public class RPGPluginContext : DbContext
    {
        public RPGPluginContext() : base("Chatbot")
        {

        }

        // public DbSet<RPGTable> RPGTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("data");
        }
    }
}
