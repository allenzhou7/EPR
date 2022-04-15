namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806162340 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "Phone", c => c.String(nullable: false));
        }
    }
}
