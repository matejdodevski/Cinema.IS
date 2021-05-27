namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieModels", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.MovieModels", new[] { "ShoppingCart_Id" });
            DropColumn("dbo.MovieModels", "ShoppingCart_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieModels", "ShoppingCart_Id", c => c.Int());
            CreateIndex("dbo.MovieModels", "ShoppingCart_Id");
            AddForeignKey("dbo.MovieModels", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
        }
    }
}
