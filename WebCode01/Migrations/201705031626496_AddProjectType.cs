namespace WebCode01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Projects", "projectTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "projectTypeId");
            DropTable("dbo.ProjectTypes");
        }
    }
}
