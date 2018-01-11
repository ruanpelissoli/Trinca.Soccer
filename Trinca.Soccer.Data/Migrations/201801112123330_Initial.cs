namespace Trinca.Soccer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(),
                        Time = c.String(),
                        MinimumPlayers = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WithBarbecue = c.Boolean(nullable: false),
                        BarbecueValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsFinished = c.Boolean(nullable: false),
                        BlueTeamScore = c.Int(nullable: false),
                        RedTeamScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeId = c.Int(),
                        IsGuest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Team", "GameId", "dbo.Game");
            DropIndex("dbo.Player", new[] { "EmployeeId" });
            DropIndex("dbo.Team", new[] { "PlayerId" });
            DropIndex("dbo.Team", new[] { "GameId" });
            DropTable("dbo.Player");
            DropTable("dbo.Team");
            DropTable("dbo.Game");
            DropTable("dbo.Employee");
        }
    }
}
