namespace Chatbot.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueIndexToPluginConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("chatbot.PluginConfigurations", new[] { "Name", "Key" }, unique: true, name: "PluginNameAndKeyIsUnique");
        }
        
        public override void Down()
        {
            DropIndex("chatbot.PluginConfigurations", "PluginNameAndKeyIsUnique");
        }
    }
}
