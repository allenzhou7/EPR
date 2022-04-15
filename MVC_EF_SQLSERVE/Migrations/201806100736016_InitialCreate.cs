namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cost",
                c => new
                    {
                        CostID = c.Int(nullable: false, identity: true),
                        CostDate = c.DateTime(nullable: false),
                        CostDesc = c.String(),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CostID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Company = c.String(),
                        Address = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        CustomerID = c.String(),
                        ProductID = c.String(),
                        Number = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.SaleID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Length = c.String(),
                        Colour = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.SaleCustomer",
                c => new
                    {
                        Sale_SaleID = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sale_SaleID, t.Customer_CustomerID })
                .ForeignKey("dbo.Sale", t => t.Sale_SaleID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.Customer_CustomerID, cascadeDelete: true)
                .Index(t => t.Sale_SaleID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.ProductSale",
                c => new
                    {
                        Product_ProductID = c.Int(nullable: false),
                        Sale_SaleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductID, t.Sale_SaleID })
                .ForeignKey("dbo.Product", t => t.Product_ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sale", t => t.Sale_SaleID, cascadeDelete: true)
                .Index(t => t.Product_ProductID)
                .Index(t => t.Sale_SaleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSale", "Sale_SaleID", "dbo.Sale");
            DropForeignKey("dbo.ProductSale", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.SaleCustomer", "Customer_CustomerID", "dbo.Customer");
            DropForeignKey("dbo.SaleCustomer", "Sale_SaleID", "dbo.Sale");
            DropIndex("dbo.ProductSale", new[] { "Sale_SaleID" });
            DropIndex("dbo.ProductSale", new[] { "Product_ProductID" });
            DropIndex("dbo.SaleCustomer", new[] { "Customer_CustomerID" });
            DropIndex("dbo.SaleCustomer", new[] { "Sale_SaleID" });
            DropTable("dbo.ProductSale");
            DropTable("dbo.SaleCustomer");
            DropTable("dbo.Product");
            DropTable("dbo.Sale");
            DropTable("dbo.Customer");
            DropTable("dbo.Cost");
        }
    }
}
