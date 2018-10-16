namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharingKeysAddedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEventLists", "SharingKey", c => c.String(maxLength: 16));
            AddColumn("dbo.AppEventLists", "SharingPassword", c => c.String(maxLength: 8));
            AddColumn("dbo.AppToDoLists", "SharingKey", c => c.String(maxLength: 16));
            AddColumn("dbo.AppToDoLists", "SharingPassword", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppToDoLists", "SharingPassword");
            DropColumn("dbo.AppToDoLists", "SharingKey");
            DropColumn("dbo.AppEventLists", "SharingPassword");
            DropColumn("dbo.AppEventLists", "SharingKey");
        }
    }
}
