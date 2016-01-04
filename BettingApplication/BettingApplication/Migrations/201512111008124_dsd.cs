namespace BettingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bets", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bets", "Points");
        }
    }
}
