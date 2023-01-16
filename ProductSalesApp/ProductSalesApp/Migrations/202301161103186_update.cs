namespace ProductSalesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductStock", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "ProductPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductPrice", c => c.String());
            AlterColumn("dbo.Products", "ProductStock", c => c.String());
        }
    }
}
