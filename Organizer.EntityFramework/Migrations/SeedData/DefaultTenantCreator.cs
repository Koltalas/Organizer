using System.Linq;
using Organizer.EntityFramework;
using Organizer.MultiTenancy;

namespace Organizer.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly OrganizerDbContext _context;

        public DefaultTenantCreator(OrganizerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
