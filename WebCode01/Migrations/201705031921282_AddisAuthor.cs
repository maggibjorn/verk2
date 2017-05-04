namespace WebCode01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "isAuthor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "isAuthor");
        }
    }
}
