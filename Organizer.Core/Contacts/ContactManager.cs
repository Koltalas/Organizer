using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organizer.Contacts
{
    public class ContactManager : IContactManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IRepository<Contact, Guid> _contactRepository;

        public ContactManager(
            IRepository<Contact, Guid> contactRepository)
        {
            _contactRepository = contactRepository;

            EventBus = NullEventBus.Instance;
        }


        public async Task<Contact> CreateAsync(Contact contact)
        {
            var newContact = await _contactRepository.InsertAsync(contact);
            return newContact;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _contactRepository.DeleteAsync(id);
        }

        public async Task DeleteAsync(Contact contact)
        {
            await _contactRepository.DeleteAsync(contact);
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(id);
            if (contact == null)
                throw new UserFriendlyException("Could not found the contact, maybe it's deleted!");
            return contact;

        }

        public async Task UpdateAsync(Contact contact)
        {
            await _contactRepository.UpdateAsync(contact);
        }

        public async Task LinkUserProfileAsync(Guid contactId, long profileId)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(contactId);
            if (contact == null)
                throw new UserFriendlyException("Could not found the contact, maybe it's deleted!");
            contact.ChangeProfile(profileId);
        }
        
    }
}
