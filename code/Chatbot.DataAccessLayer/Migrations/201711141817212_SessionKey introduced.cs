namespace Chatbot.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionKeyintroduced : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("chatbot.Messages");
            CreateTable(
                "chatbot.SessionKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("chatbot.Messages", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("chatbot.Messages", "IsUserMessage", c => c.Boolean(nullable: false));
            AddColumn("chatbot.Messages", "SessionKey_Id", c => c.Int());
            AddPrimaryKey("chatbot.Messages", "Id");
            CreateIndex("chatbot.Messages", "SessionKey_Id");
            AddForeignKey("chatbot.Messages", "SessionKey_Id", "chatbot.SessionKeys", "Id");
            DropColumn("chatbot.Messages", "MessageID");
        }
        
        public override void Down()
        {
            AddColumn("chatbot.Messages", "MessageID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("chatbot.Messages", "SessionKey_Id", "chatbot.SessionKeys");
            DropIndex("chatbot.Messages", new[] { "SessionKey_Id" });
            DropPrimaryKey("chatbot.Messages");
            DropColumn("chatbot.Messages", "SessionKey_Id");
            DropColumn("chatbot.Messages", "IsUserMessage");
            DropColumn("chatbot.Messages", "Id");
            DropTable("chatbot.SessionKeys");
            AddPrimaryKey("chatbot.Messages", "MessageID");
        }
    }
}
