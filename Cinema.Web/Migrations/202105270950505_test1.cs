namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCarts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCarts", new[] { "OwnerId" });
            AlterColumn("dbo.ShoppingCarts", "OwnerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShoppingCarts", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ShoppingCarts", "OwnerId");
            AddForeignKey("dbo.ShoppingCarts", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
