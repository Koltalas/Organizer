using Abp.Domain.Services;
using Organizer.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos
{
    public interface IToDoManager : IDomainService
    {
        Task<ToDoElem> GetElemAsync(Guid id);
        Task<ToDoList> GetListAsync(Guid id);
        Task<ToDoElem> CreateElemAsync(ToDoElem elem);
        Task<ToDoList> CreateListAsync(ToDoList list);
        Task DeleteElemAsync(ToDoElem elem);
        Task DeleteListAsync(ToDoList list);
        Task UpdateElemAsync(ToDoElem elem);
        Task UpdateListAsync(ToDoList list);
        Task<ToDoListUser> AddUserAsync(ToDoList toDoList, User user, SharingType sharingType = SharingType.Client);
        Task RemoveUserAsync(ToDoList toDoList, User user);
        Task<IReadOnlyList<User>> GetListUsersAsync(ToDoList toDoList);
        Task<IReadOnlyList<ToDoList>> GetUserListsAsync(User user);

    }
}
