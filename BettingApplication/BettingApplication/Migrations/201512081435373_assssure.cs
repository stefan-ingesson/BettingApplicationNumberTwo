namespace BettingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assssure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bets", "RoundId", c => c.Int(nullable: false));
            DropColumn("dbo.Bets", "MatchDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bets", "MatchDay", c => c.Int(nullable: false));
            DropColumn("dbo.Bets", "RoundId");
        }
    }
}
