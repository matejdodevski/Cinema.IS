namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpayed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketModels", "Payed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketModels", "Payed");
        }
    }
}
