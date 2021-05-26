namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedColumnsAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        DateOfReservation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MovieModels", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieModels", "Price");
            DropTable("dbo.TicketModels");
        }
    }
}
