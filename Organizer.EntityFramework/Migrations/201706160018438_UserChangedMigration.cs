namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChangedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "Birthday", c => c.DateTime());
            AddColumn("dbo.AbpUsers", "Adress", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "Adress");
            DropColumn("dbo.AbpUsers", "Birthday");
        }
    }
}
