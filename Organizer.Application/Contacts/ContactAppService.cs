using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Organizer.Contacts.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organizer.Contacts
{
    [AbpAuthorize]
    public class ContactAppService : OrganizerAppServiceBase, IContactAppService
    {
        private readonly IContactManager _contactManager;
        private readonly IRepository<Contact, Guid> _contactRepository;

        public ContactAppService(
            IContactManager contactManager,
            IRepository<Contact, Guid> contactRepository)
        {
            _contactManager = contactManager;
            _contactRepository = contactRepository;
        }

        public async Task<ContactDetailOutput> Create(CreateContactInput input)
        {
            var user = await UserManager.GetUserByUserNameAsync(input.ProfileUserName);

            var contact = Contact.Create(input.FName, AbpSession.UserId.Value,
                input.LName, input.Email, input.PhoneNumber, input.Birthday, input.Adress, user?.Id);
            var newContact = await _contactManager.CreateAsync(contact);
            return newContact.MapTo<ContactDetailOutput>();
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);

            if (contact == null)
                throw new UserFriendlyException("Could not found the contact, maybe it's already deleted.");

            await _contactManager.DeleteAsync(contact);
        }

        public async Task<ContactDetailOutput> GetDetail(EntityDto<Guid> input)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);

            if (contact == null)
                throw new UserFriendlyException("Could not found the contact");

            return contact.MapTo<ContactDetailOutput>();
        }

        public async Task<ListResultDto<ContactDetailOutput>> GetList()
        {
            var contacts = await _contactRepository.GetAllListAsync(e => e.UserId == AbpSession.UserId);

            return new ListResultDto<ContactDetailOutput>(contacts.MapTo<List<ContactDetailOutput>>());
        }

        public async Task Update(UpdateContactInput input)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);

            if (contact == null)
                throw new UserFriendlyException("Could not found the contact, maybe it's deleted.");

            contact.Update(input.FName, input.LName, input.Email, input.PhoneNumber,
                input.Birthday, input.Adress, input.ProfileId, input.UserId);
            await _contactManager.UpdateAsync(contact);
        }

        public async Task LinkUserProfile(LinkUserProfileInput input)
        {
            var contact = await _contactRepository.FirstOrDefaultAsync(e => e.Id == input.ContactId && e.UserId == AbpSession.UserId);
            if (contact == null)
                throw new UserFriendlyException("Could not found the contact, maybe it's deleted.");

            var user = await UserManager.GetUserByUserNameAsync(input.UserName);
            if (user == null)
                throw new UserFriendlyException("Could not found the user, maybe it's deleted.");

            await _contactManager.LinkUserProfileAsync(contact.Id, user.Id);
        }

        public async Task<ListResultDto<ContactDetailOutput>> GetRealUsers()
        {
            var contacts = await _contactRepository.GetAllListAsync(e => e.UserId == AbpSession.UserId && e.ProfileId != null);
            if (contacts == null)
                throw new UserFriendlyException("Could not found the profiiles, maybe there are no linked.");

            return new ListResultDto<ContactDetailOutput>(contacts.MapTo<List<ContactDetailOutput>>());
        }

    }
}
