namespace Organizer.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoSharingPart1Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppToDoElems", "ToDoListId", "dbo.AppToDoLists");
            DropForeignKey("dbo.AppToDoLists", "UserId", "dbo.AbpUsers");
            DropIndex("dbo.AppToDoElems", new[] { "ToDoListId" });
            DropIndex("dbo.AppToDoLists", new[] { "UserId" });
            DropTable("dbo.AppToDoElems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoElem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppToDoLists",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppToDoLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 2048),
                        UserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppToDoElems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        IsCompleted = c.Boolean(nullable: false),
                        ToDoListId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoElem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AppToDoLists", "UserId");
            CreateIndex("dbo.AppToDoElems", "ToDoListId");
            AddForeignKey("dbo.AppToDoLists", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppToDoElems", "ToDoListId", "dbo.AppToDoLists", "Id", cascadeDelete: true);
        }
    }
}
