namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806152331 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Filter", "CustomerID", c => c.Int());
            AlterColumn("dbo.Filter", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Filter", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filter", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Filter", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Filter", "CustomerID", c => c.Int(nullable: false));
        }
    }
}
