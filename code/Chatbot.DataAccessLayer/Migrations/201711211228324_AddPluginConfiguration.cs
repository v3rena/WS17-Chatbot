namespace Chatbot.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPluginConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "chatbot.PluginConfigurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("chatbot.PluginConfigurations");
        }
    }
}
