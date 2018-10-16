using Abp.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Organizer.Notes
{
    public interface INoteManager: IDomainService
    {
        Task<Note> GetAsync(Guid id);
        Task<Note> CreateAsync(Note note);
        Task DeleteAsync(Note note);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Note note);
    }
}
