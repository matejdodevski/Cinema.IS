namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothing_added_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            AddColumn("dbo.MovieModels", "ShoppingCart_Id", c => c.Int());
            AddColumn("dbo.TicketModels", "ShoppingCartId", c => c.Int());
            CreateIndex("dbo.MovieModels", "ShoppingCart_Id");
            CreateIndex("dbo.TicketModels", "ShoppingCartId");
            AddForeignKey("dbo.MovieModels", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
            AddForeignKey("dbo.TicketModels", "ShoppingCartId", "dbo.ShoppingCarts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketModels", "ShoppingCartId", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieModels", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCarts", new[] { "OwnerId" });
            DropIndex("dbo.TicketModels", new[] { "ShoppingCartId" });
            DropIndex("dbo.MovieModels", new[] { "ShoppingCart_Id" });
            DropColumn("dbo.TicketModels", "ShoppingCartId");
            DropColumn("dbo.MovieModels", "ShoppingCart_Id");
            DropTable("dbo.ShoppingCarts");
        }
    }
}
