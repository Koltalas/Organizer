namespace Organizer.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NameChangedMirgation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AppNote", newName: "AppNotes");
            RenameTable(name: "dbo.AppToDoElem", newName: "AppToDoElems");
            RenameTable(name: "dbo.AppToDoList", newName: "AppToDoLists");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AppToDoLists", newName: "AppToDoList");
            RenameTable(name: "dbo.AppToDoElems", newName: "AppToDoElem");
            RenameTable(name: "dbo.AppNotes", newName: "AppNote");
        }
    }
}
