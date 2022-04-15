namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806101749 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "FullName", c => c.String(nullable: false));
        }
    }
}
