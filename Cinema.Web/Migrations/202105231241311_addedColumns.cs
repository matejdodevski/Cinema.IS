namespace Cinema.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GenreId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieModels");
        }
    }
}
