namespace DayTideWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false, maxLength: 500),
                        Salary = c.Double(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Users", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationType = c.String(nullable: false, maxLength: 50),
                        Massage = c.String(nullable: false),
                        SentBy = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false),
                        Accepted_RejectedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.SentBy, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Accepted_RejectedBy)
                .Index(t => t.SentBy)
                .Index(t => t.Accepted_RejectedBy);
            
            CreateTable(
                "dbo.CartBackups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 50),
                        ProductId = c.Int(nullable: false),
                        Quantiry = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 50),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price_unit_ = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false, maxLength: 500),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Delivery_Man_Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DelManID = c.String(nullable: false, maxLength: 128),
                        Rating = c.Int(nullable: false),
                        Comments = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DeliveryMen", t => t.DelManID, cascadeDelete: true)
                .Index(t => t.DelManID)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.DeliveryMen",
                c => new
                    {
                        DelManId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false, maxLength: 500),
                        Salary = c.Double(nullable: false),
                        Picture = c.String(),
                        Complete_Task = c.Int(nullable: false),
                        In_Service = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DelManId)
                .ForeignKey("dbo.Users", t => t.DelManId)
                .Index(t => t.DelManId);
            
            CreateTable(
                "dbo.Moderators",
                c => new
                    {
                        ModeratorId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false, maxLength: 500),
                        Salary = c.Double(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.ModeratorId)
                .ForeignKey("dbo.Users", t => t.ModeratorId)
                .Index(t => t.ModeratorId);
            
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Massage = c.String(nullable: false, maxLength: 500),
                        Send_For = c.String(maxLength: 128),
                        Send_by = c.String(maxLength: 128),
                        Status = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Send_For)
                .ForeignKey("dbo.Users", t => t.Send_by)
                .Index(t => t.Send_For)
                .Index(t => t.Send_by);
            
            CreateTable(
                "dbo.Order_Detail",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 500),
                        District = c.String(nullable: false, maxLength: 50),
                        Amount = c.Double(nullable: false),
                        Payment_Type = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        DelManId = c.String(nullable: false, maxLength: 128),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DeliveryMen", t => t.DelManId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DelManId);
            
            CreateTable(
                "dbo.OrderRequests",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 500),
                        District = c.String(nullable: false, maxLength: 50),
                        Amount = c.Double(nullable: false),
                        Payment_Type = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Product_Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Comments = c.String(),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        CategoryId = c.Int(nullable: false),
                        Buying_Price = c.Double(nullable: false),
                        Selling_Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Picture = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Rating", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Product_Rating", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderRequests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Order_Detail", "DelManId", "dbo.DeliveryMen");
            DropForeignKey("dbo.Order_Detail", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Notices", "Send_by", "dbo.Users");
            DropForeignKey("dbo.Notices", "Send_For", "dbo.Users");
            DropForeignKey("dbo.Moderators", "ModeratorId", "dbo.Users");
            DropForeignKey("dbo.Delivery_Man_Rating", "DelManID", "dbo.DeliveryMen");
            DropForeignKey("dbo.DeliveryMen", "DelManId", "dbo.Users");
            DropForeignKey("dbo.Delivery_Man_Rating", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Application", "Accepted_RejectedBy", "dbo.Users");
            DropForeignKey("dbo.Application", "SentBy", "dbo.Users");
            DropForeignKey("dbo.Admin", "AdminId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Product_Rating", new[] { "CustomerId" });
            DropIndex("dbo.Product_Rating", new[] { "ProductId" });
            DropIndex("dbo.OrderRequests", new[] { "CustomerId" });
            DropIndex("dbo.Order_Detail", new[] { "DelManId" });
            DropIndex("dbo.Order_Detail", new[] { "CustomerId" });
            DropIndex("dbo.Notices", new[] { "Send_by" });
            DropIndex("dbo.Notices", new[] { "Send_For" });
            DropIndex("dbo.Moderators", new[] { "ModeratorId" });
            DropIndex("dbo.DeliveryMen", new[] { "DelManId" });
            DropIndex("dbo.Delivery_Man_Rating", new[] { "CustomerId" });
            DropIndex("dbo.Delivery_Man_Rating", new[] { "DelManID" });
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropIndex("dbo.Application", new[] { "Accepted_RejectedBy" });
            DropIndex("dbo.Application", new[] { "SentBy" });
            DropIndex("dbo.Admin", new[] { "AdminId" });
            DropTable("dbo.Products");
            DropTable("dbo.Product_Rating");
            DropTable("dbo.OrderRequests");
            DropTable("dbo.Order_Detail");
            DropTable("dbo.Notices");
            DropTable("dbo.Moderators");
            DropTable("dbo.DeliveryMen");
            DropTable("dbo.Delivery_Man_Rating");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.CartBackups");
            DropTable("dbo.Application");
            DropTable("dbo.Users");
            DropTable("dbo.Admin");
        }
    }
}
