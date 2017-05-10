namespace WebCode01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        projectId = c.Int(nullable: false),
                        userName = c.String(),
                        text = c.String(),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
