using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.ToDos.Dto;
using System;
using System.Threading.Tasks;

namespace Organizer.ToDos
{
    public interface IToDoAppService: IApplicationService
    {
        Task<ToDoListDetailOutput> CreateList(CreateListInput input);
        Task<ToDoElemDetailOutput> CreateElem(CreateElemInput input);        
        Task DeleteList(EntityDto<Guid> input);        
        Task DeleteElem(EntityDto<Guid> input);
        Task<ListResultDto<ToDoListDetailOutput>> GetAllLists();                
        Task<ListResultDto<ToDoElemDetailOutput>> GetAllElems();        
        Task<ToDoListDetailOutput> GetListDetail(EntityDto<Guid> input);        
        Task<ToDoElemDetailOutput> GetElemDetail(EntityDto<Guid> input);        
        Task UpdateList(UpdateToDoListInput input);        
        Task UpdateElem(UpdateToDoElemInput input);
        Task<AddUserToListOutput> AddUserToList(AddUserToListInput input);
        Task RemoveUserFromList(RemoveUserFromListInput input);
        Task<GenereateSharingOutput> GenerateSharing(GenerateSharingInput input);
        Task<ToDoListDetailOutput> GetAccessToList(GetAccessToListInput input);
        Task AddReferenceToEvent(AddReferenceToEventInput input);

    }
}
