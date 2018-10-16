using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.Events.Dto;
using System;
using System.Threading.Tasks;

namespace Organizer.Events
{
    public interface IEventAppService : IApplicationService
    {
        Task<ListResultDto<EventListDetailOutput>> GetAllList();
        Task<ListResultDto<EventDetailOutput>> GetAllEvents();
        Task<EventListDetailOutput> GetListDetail(EntityDto<Guid> input);
        Task<EventDetailOutput> GetEventDetail(EntityDto<Guid> input);
        Task<EventListDetailOutput> CreateList(CreateListInput input);
        Task<EventDetailOutput> CreateEvent(CreateEventInput input);
        Task DeleteList(EntityDto<Guid> input);
        Task DeleteEvent(EntityDto<Guid> input);
        Task UpdateList(UpdateEventListInput input);
        Task UpdateEvent(UpdateEventInput input);
        Task<AddUserToListOutput> AddUserToList(AddUserToListInput input);
        Task RemoveUserFromList(RemoveUserFromListInput input);
        Task<GenereateSharingOutput> GenerateSharing(GenerateSharingInput input);
        Task<EventListDetailOutput> GetAccessToList(GetAccessToListInput input);

    }
}
