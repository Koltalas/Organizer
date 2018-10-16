namespace Organizer.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ColorToEventListAddedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEventLists", "HexColor", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEventLists", "HexColor");
        }
    }
}
