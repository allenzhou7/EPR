namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806102146 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cost", "CostDesc", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "FullName", c => c.String());
            AlterColumn("dbo.Customer", "Phone", c => c.String());
            AlterColumn("dbo.Customer", "Name", c => c.String());
            AlterColumn("dbo.Cost", "CostDesc", c => c.String());
        }
    }
}
