using System.Data.Common;
using Abp.Zero.EntityFramework;
using Organizer.Authorization.Roles;
using Organizer.MultiTenancy;
using Organizer.Users;
using System.Data.Entity;
using Organizer.Events;
using Organizer.Notes;
using Organizer.ToDos;
using Organizer.Contacts;

namespace Organizer.EntityFramework
{
    public class OrganizerDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Event> Events { get; set; }
        public virtual IDbSet<EventList> EventLists { get; set; }
        public virtual IDbSet<EventListUser> EventListUsers { get; set; }

        public virtual IDbSet<Note> Notes { get; set; }

        public virtual IDbSet<ToDoList> ToDoLists { get; set; }
        public virtual IDbSet<ToDoElem> ToDoElems { get; set; }
        public virtual IDbSet<ToDoListUser> ToDoListUsers { get; set; }

        public virtual IDbSet<Contact> Contacts { get; set; }





        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public OrganizerDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in OrganizerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of OrganizerDbContext since ABP automatically handles it.
         */
        public OrganizerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public OrganizerDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public OrganizerDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
