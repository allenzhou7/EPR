namespace MVC_EF_SQLSERVE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201806152325 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filter",
                c => new
                    {
                        FilterID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FilterID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Filter");
        }
    }
}
