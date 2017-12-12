namespace Trinca.Soccer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Worker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        PictureUrl = c.String(),
                        Password = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        MinimumPlayers = c.Int(nullable: false),
                        Players = c.Int(nullable: false),
                        Place = c.String(),
                        Canceled = c.Boolean(nullable: false),
                        Finished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Worker", "Game_Id", "dbo.Game");
            DropIndex("dbo.Worker", new[] { "Game_Id" });
            DropTable("dbo.Game");
            DropTable("dbo.Worker");
        }
    }
}
