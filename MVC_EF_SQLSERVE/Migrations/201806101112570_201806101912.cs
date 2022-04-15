namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806101912 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaleCustomer", "Sale_SaleID", "dbo.Sale");
            DropForeignKey("dbo.SaleCustomer", "Customer_CustomerID", "dbo.Customer");
            DropForeignKey("dbo.ProductSale", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductSale", "Sale_SaleID", "dbo.Sale");
            DropIndex("dbo.SaleCustomer", new[] { "Sale_SaleID" });
            DropIndex("dbo.SaleCustomer", new[] { "Customer_CustomerID" });
            DropIndex("dbo.ProductSale", new[] { "Product_ProductID" });
            DropIndex("dbo.ProductSale", new[] { "Sale_SaleID" });
            CreateIndex("dbo.Sale", "CustomerID");
            CreateIndex("dbo.Sale", "ProductID");
            AddForeignKey("dbo.Sale", "CustomerID", "dbo.Customer", "CustomerID", cascadeDelete: true);
            AddForeignKey("dbo.Sale", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
            DropTable("dbo.SaleCustomer");
            DropTable("dbo.ProductSale");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductSale",
                c => new
                    {
                        Product_ProductID = c.Int(nullable: false),
                        Sale_SaleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductID, t.Sale_SaleID });
            
            CreateTable(
                "dbo.SaleCustomer",
                c => new
                    {
                        Sale_SaleID = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sale_SaleID, t.Customer_CustomerID });
            
            DropForeignKey("dbo.Sale", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Sale", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Sale", new[] { "ProductID" });
            DropIndex("dbo.Sale", new[] { "CustomerID" });
            CreateIndex("dbo.ProductSale", "Sale_SaleID");
            CreateIndex("dbo.ProductSale", "Product_ProductID");
            CreateIndex("dbo.SaleCustomer", "Customer_CustomerID");
            CreateIndex("dbo.SaleCustomer", "Sale_SaleID");
            AddForeignKey("dbo.ProductSale", "Sale_SaleID", "dbo.Sale", "SaleID", cascadeDelete: true);
            AddForeignKey("dbo.ProductSale", "Product_ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.SaleCustomer", "Customer_CustomerID", "dbo.Customer", "CustomerID", cascadeDelete: true);
            AddForeignKey("dbo.SaleCustomer", "Sale_SaleID", "dbo.Sale", "SaleID", cascadeDelete: true);
        }
    }
}
