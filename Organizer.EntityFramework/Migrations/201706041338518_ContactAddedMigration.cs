namespace Organizer.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    public partial class ContactAddedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FName = c.String(nullable: false, maxLength: 128),
                        LName = c.String(maxLength: 128),
                        Email = c.String(maxLength: 128),
                        PhoneNumber = c.String(maxLength: 20),
                        Birthday = c.DateTime(),
                        Adress = c.String(maxLength: 128),
                        UserId = c.Long(nullable: false),
                        ProfileId = c.Long(),
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
                    { "DynamicFilter_Contact_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.ProfileId)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppContacts", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AppContacts", "ProfileId", "dbo.AbpUsers");
            DropIndex("dbo.AppContacts", new[] { "ProfileId" });
            DropIndex("dbo.AppContacts", new[] { "UserId" });
            DropTable("dbo.AppContacts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Contact_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
