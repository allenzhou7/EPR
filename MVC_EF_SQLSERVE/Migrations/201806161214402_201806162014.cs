namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806162014 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sale", "Number", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sale", "Number", c => c.Int(nullable: false));
        }
    }
}
