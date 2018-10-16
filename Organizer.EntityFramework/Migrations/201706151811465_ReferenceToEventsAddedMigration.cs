namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenceToEventsAddedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppToDoLists", "EventId", c => c.Guid());
            CreateIndex("dbo.AppToDoLists", "EventId");
            AddForeignKey("dbo.AppToDoLists", "EventId", "dbo.AppEvents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppToDoLists", "EventId", "dbo.AppEvents");
            DropIndex("dbo.AppToDoLists", new[] { "EventId" });
            DropColumn("dbo.AppToDoLists", "EventId");
        }
    }
}
