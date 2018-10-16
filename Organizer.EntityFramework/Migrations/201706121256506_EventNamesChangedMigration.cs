namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventNamesChangedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppEvents", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppEvents", "AllDay", c => c.Boolean(nullable: false));
            DropColumn("dbo.AppEvents", "StartAt");
            DropColumn("dbo.AppEvents", "EndAt");
            DropColumn("dbo.AppEvents", "IsFullDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppEvents", "IsFullDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppEvents", "EndAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppEvents", "StartAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.AppEvents", "AllDay");
            DropColumn("dbo.AppEvents", "End");
            DropColumn("dbo.AppEvents", "Start");
        }
    }
}
