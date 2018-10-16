namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventListsNamesMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEventLists", "Color", c => c.String(maxLength: 7));
            DropColumn("dbo.AppEventLists", "HexColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppEventLists", "HexColor", c => c.String(maxLength: 7));
            DropColumn("dbo.AppEventLists", "Color");
        }
    }
}
