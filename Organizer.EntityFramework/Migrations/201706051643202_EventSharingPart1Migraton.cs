namespace Organizer.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class EventSharingPart1Migraton : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppEvents", "UserId", "dbo.AbpUsers");
            DropIndex("dbo.AppEvents", new[] { "UserId" });
            DropTable("dbo.AppEvents",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Event_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 2048),
                        StartAt = c.DateTime(nullable: false),
                        EndAt = c.DateTime(nullable: false),
                        IsFullDay = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Event_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AppEvents", "UserId");
            AddForeignKey("dbo.AppEvents", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: true);
        }
    }
}
