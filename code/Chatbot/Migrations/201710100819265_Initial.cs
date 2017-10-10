namespace Chatbot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                    })
                .PrimaryKey(t => t.TestID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tests");
        }
    }
}
