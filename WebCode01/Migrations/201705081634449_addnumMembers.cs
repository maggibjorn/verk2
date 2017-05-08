namespace WebCode01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnumMembers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "numMembers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "numMembers");
        }
    }
}
