namespace Organizer.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoSharingPart2Migration : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppToDoLists", t => t.ToDoListId, cascadeDelete: true)
                .Index(t => t.ToDoListId);
            
            CreateTable(
                "dbo.AppToDoLists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 2048),
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
                "dbo.AppToDoListUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SharingType = c.Int(nullable: false),
                        ToDoListId = c.Guid(nullable: false),
                        UserId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppToDoLists", t => t.ToDoListId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ToDoListId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppToDoListUsers", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AppToDoListUsers", "ToDoListId", "dbo.AppToDoLists");
            DropForeignKey("dbo.AppToDoElems", "ToDoListId", "dbo.AppToDoLists");
            DropIndex("dbo.AppToDoListUsers", new[] { "UserId" });
            DropIndex("dbo.AppToDoListUsers", new[] { "ToDoListId" });
            DropIndex("dbo.AppToDoElems", new[] { "ToDoListId" });
            DropTable("dbo.AppToDoListUsers");
            DropTable("dbo.AppToDoLists",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppToDoElems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoElem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
