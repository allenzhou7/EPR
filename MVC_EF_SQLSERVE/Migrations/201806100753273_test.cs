namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sale", "CustomerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Sale", "ProductID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sale", "ProductID", c => c.String());
            AlterColumn("dbo.Sale", "CustomerID", c => c.String());
        }
    }
}
