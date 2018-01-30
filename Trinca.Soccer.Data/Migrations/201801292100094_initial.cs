namespace Trinca.Soccer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.Int(nullable: false),
                        Place = c.String(),
                        Date = c.DateTime(nullable: false),
                        MinimumPlayers = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WithBarbecue = c.Boolean(nullable: false),
                        BarbecueValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsFinished = c.Boolean(nullable: false),
                        BlueTeamScore = c.Int(nullable: false),
                        RedTeamScore = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Name = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        IsGuest = c.Boolean(nullable: false),
                        WithBarbecue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .ForeignKey("dbo.Match", t => t.MatchId)
                .Index(t => t.MatchId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Player", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Match", "CreatedBy", "dbo.Employee");
            DropIndex("dbo.Player", new[] { "EmployeeId" });
            DropIndex("dbo.Player", new[] { "MatchId" });
            DropIndex("dbo.Match", new[] { "CreatedBy" });
            DropTable("dbo.Player");
            DropTable("dbo.Match");
            DropTable("dbo.Employee");
        }
    }
}
