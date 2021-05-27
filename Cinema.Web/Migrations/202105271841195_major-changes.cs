namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majorchanges : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TicketModels", "MovieId");
            AddForeignKey("dbo.TicketModels", "MovieId", "dbo.MovieModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketModels", "MovieId", "dbo.MovieModels");
            DropIndex("dbo.TicketModels", new[] { "MovieId" });
        }
    }
}
