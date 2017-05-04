namespace WebCode01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        fileBinary = c.Binary(),
                        projectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Files");
        }
    }
}
