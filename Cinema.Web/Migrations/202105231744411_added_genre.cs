namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_genre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenreModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.TicketModels", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketModels", "OrderId", c => c.Int(nullable: false));
            DropTable("dbo.GenreModels");
        }
    }
}
