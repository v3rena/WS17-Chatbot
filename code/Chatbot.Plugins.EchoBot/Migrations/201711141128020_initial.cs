namespace Chatbot.Plugins.EchoBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "echobot.EchoBotTables",
                c => new
                    {
                        EchoBotTableID = c.Int(nullable: false, identity: true),
                        EchoBotName = c.String(),
                    })
                .PrimaryKey(t => t.EchoBotTableID);
            
        }
        
        public override void Down()
        {
            DropTable("echobot.EchoBotTables");
        }
    }
}
