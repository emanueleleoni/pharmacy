namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ForAdult", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ForAdult");
        }
    }
}
