namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "ProductID", "dbo.Products");
            DropIndex("dbo.Transactions", new[] { "ProductID" });
            DropColumn("dbo.Transactions", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "ProductID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Transactions", "ProductID");
            AddForeignKey("dbo.Transactions", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
    }
}
