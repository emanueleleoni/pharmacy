namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "ShippingDate", c => c.DateTime());
            AlterColumn("dbo.Transactions", "DeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "DeliveryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Transactions", "ShippingDate", c => c.DateTime(nullable: false));
        }
    }
}
