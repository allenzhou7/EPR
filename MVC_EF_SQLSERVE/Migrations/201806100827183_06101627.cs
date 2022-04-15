namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06101627 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Product", "Length", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Colour", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "FullName", c => c.String());
            AlterColumn("dbo.Product", "Colour", c => c.String());
            AlterColumn("dbo.Product", "Length", c => c.String());
            AlterColumn("dbo.Product", "Name", c => c.String());
        }
    }
}
