namespace BettingApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkkoikj : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bets", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bets", "Points", c => c.Int(nullable: false));
        }
    }
}
