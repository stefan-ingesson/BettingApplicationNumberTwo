namespace BettingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bets", "MatchDay", c => c.Int(nullable: false));
            DropColumn("dbo.Bets", "RoundId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bets", "RoundId", c => c.Int(nullable: false));
            DropColumn("dbo.Bets", "MatchDay");
        }
    }
}
