using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Organizer.EntityFramework;

namespace Organizer.Migrator
{
    [DependsOn(typeof(OrganizerDataModule))]
    public class OrganizerMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<OrganizerDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}