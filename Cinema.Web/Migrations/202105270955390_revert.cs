namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShoppingCarts", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCarts", "OwnerId");
            AddForeignKey("dbo.ShoppingCarts", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCarts", new[] { "OwnerId" });
            AlterColumn("dbo.ShoppingCarts", "OwnerId", c => c.String());
        }
    }
}
