namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordLengthChangedMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppEventLists", "SharingPassword", c => c.String(maxLength: 68));
            AlterColumn("dbo.AppToDoLists", "SharingPassword", c => c.String(maxLength: 68));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppToDoLists", "SharingPassword", c => c.String(maxLength: 8));
            AlterColumn("dbo.AppEventLists", "SharingPassword", c => c.String(maxLength: 8));
        }
    }
}
