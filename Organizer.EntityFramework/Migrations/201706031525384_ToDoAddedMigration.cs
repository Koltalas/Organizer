namespace Organizer.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class ToDoAddedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppToDoElem",
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
                .ForeignKey("dbo.AppToDoList", t => t.ToDoListId, cascadeDelete: true)
                .Index(t => t.ToDoListId);
            
            CreateTable(
                "dbo.AppToDoList",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppToDoList", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AppToDoElem", "ToDoListId", "dbo.AppToDoList");
            DropIndex("dbo.AppToDoList", new[] { "UserId" });
            DropIndex("dbo.AppToDoElem", new[] { "ToDoListId" });
            DropTable("dbo.AppToDoList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppToDoElem",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ToDoElem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
