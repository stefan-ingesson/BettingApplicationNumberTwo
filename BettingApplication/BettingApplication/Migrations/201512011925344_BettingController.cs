namespace BettingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BettingController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlacedBets = c.String(),
                        HomeTeamWins = c.Boolean(),
                        Draw = c.Boolean(),
                        AwayTeamWins = c.Boolean(),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bets");
        }
    }
}
