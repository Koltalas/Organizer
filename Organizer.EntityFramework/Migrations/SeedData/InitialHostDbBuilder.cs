using Organizer.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Organizer.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly OrganizerDbContext _context;

        public InitialHostDbBuilder(OrganizerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
