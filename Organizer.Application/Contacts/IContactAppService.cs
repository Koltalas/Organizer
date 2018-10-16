using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.Contacts.Dto;
using System;
using System.Threading.Tasks;

namespace Organizer.Contacts
{
    public interface IContactAppService : IApplicationService
    {
        Task<ContactDetailOutput> Create(CreateContactInput input);
        Task Delete(EntityDto<Guid> input);
        Task<ContactDetailOutput> GetDetail(EntityDto<Guid> input);
        Task<ListResultDto<ContactDetailOutput>> GetList();
        Task Update(UpdateContactInput input);
        Task LinkUserProfile(LinkUserProfileInput input);
        Task<ListResultDto<ContactDetailOutput>> GetRealUsers();
    }
}
