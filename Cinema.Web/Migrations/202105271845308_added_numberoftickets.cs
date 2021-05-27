namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_numberoftickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketModels", "NumberOfTickets", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketModels", "NumberOfTickets");
        }
    }
}
