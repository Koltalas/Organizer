using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organizer.Contacts
{
    public interface IContactManager : IDomainService
    {
        Task<Contact> GetAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task DeleteAsync(Contact contact);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Contact contact);
        Task LinkUserProfileAsync(Guid contactId, long profileId);
    }
}
