namespace Chatbot.Plugins.DeliveryServicePlugin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "deliveryService.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionKey = c.Guid(nullable: false),
                        OrderJsonData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("deliveryService.Orders");
        }
    }
}
