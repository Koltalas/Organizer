using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Organizer.EntityFramework.Repositories
{
    public abstract class OrganizerRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<OrganizerDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected OrganizerRepositoryBase(IDbContextProvider<OrganizerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class OrganizerRepositoryBase<TEntity> : OrganizerRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected OrganizerRepositoryBase(IDbContextProvider<OrganizerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
