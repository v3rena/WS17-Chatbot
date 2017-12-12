using Chatbot.Plugins.DeliveryService.Models;
using System.Data.Entity;

namespace Chatbot.Plugins.DeliveryServicePlugin
{
    public class DeliveryServiceContext : DbContext
    {
        public DeliveryServiceContext() : base("Chatbot")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DeliveryServiceContext, Migrations.Configuration>());
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("deliveryService");
        }
    }
}
