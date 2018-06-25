namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeletedApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        Address = c.String(),
                        VAT = c.String(),
                        City = c.String(),
                        UserType = c.Int(nullable: false),
                        IsProfileCompleted = c.Boolean(nullable: false),
                        PictureURL = c.String(),
                        CF = c.String(),
                        RfId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Agreement = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ErrorID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        Stack = c.String(),
                        InnerExceptionMessage = c.String(),
                        InnerExceptionStack = c.String(),
                        Level = c.Int(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ErrorID);
            
            CreateTable(
                "dbo.MachineErrors",
                c => new
                    {
                        ErrorID = c.Guid(nullable: false),
                        MachineID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorID);
            
            CreateTable(
                "dbo.MachineMessages",
                c => new
                    {
                        MachineMessageID = c.Guid(nullable: false),
                        MachineID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AuthorType = c.Int(nullable: false),
                        Message = c.String(),
                        MessageType = c.String(),
                    })
                .PrimaryKey(t => t.MachineMessageID);
            
            CreateTable(
                "dbo.MachineReservations",
                c => new
                    {
                        MachineReservationID = c.Guid(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        ReleaseDate = c.DateTime(),
                        IsLocked = c.Boolean(nullable: false),
                        ConsumerID = c.String(),
                        MachineID = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        TransactionID = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MachineReservationID);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        MachineID = c.Guid(nullable: false),
                        Code = c.String(),
                        Address = c.String(),
                        Description = c.String(),
                        LastUpdate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MachineID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Guid(nullable: false),
                        MarcucciCode = c.String(),
                        Code = c.String(),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        ImageUrl = c.String(),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingID = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        Address = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.ShippingID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Guid(nullable: false),
                        MachineID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        MachineKeyBoardNumber = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockID);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        TransactionDetailID = c.Guid(nullable: false),
                        TransactionID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionDetailID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Guid(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        ShippingID = c.Guid(nullable: false),
                        MachineReservationID = c.Guid(),
                        MachineID = c.Guid(),
                        TotalAmount = c.Double(nullable: false),
                        Taxes = c.Double(nullable: false),
                        ConsumerID = c.String(),
                        Type = c.Int(nullable: false),
                        CashChannel = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ErrorID = c.String(),
                        Shipped = c.Boolean(nullable: false),
                        ShippingDate = c.DateTime(nullable: false),
                        Delivered = c.Boolean(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        TransactionDetail_TransactionDetailID = c.Guid(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.TransactionDetails", t => t.TransactionDetail_TransactionDetailID)
                .Index(t => t.TransactionDetail_TransactionDetailID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        Address = c.String(),
                        VAT = c.String(),
                        City = c.String(),
                        UserType = c.Int(nullable: false),
                        IsProfileCompleted = c.Boolean(nullable: false),
                        PictureURL = c.String(),
                        CF = c.String(),
                        RfId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Agreement = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "TransactionDetail_TransactionDetailID", "dbo.TransactionDetails");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Transactions", new[] { "TransactionDetail_TransactionDetailID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionDetails");
            DropTable("dbo.Stocks");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
            DropTable("dbo.Machines");
            DropTable("dbo.MachineReservations");
            DropTable("dbo.MachineMessages");
            DropTable("dbo.MachineErrors");
            DropTable("dbo.Errors");
            DropTable("dbo.DeletedApplicationUsers");
        }
    }
}
