using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using System;
using System.Threading.Tasks;

namespace Organizer.Notes
{
    public class NoteManager : INoteManager
    {
        public IEventBus EventBus { get; set; }

        private readonly IRepository<Note, Guid> _noteRepository;

        public NoteManager(
            IRepository<Note, Guid> noteRepository)
        {
            _noteRepository = noteRepository;

            EventBus = NullEventBus.Instance;
        }

        public async Task<Note> GetAsync(Guid id)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(id);
            if (note == null)
                throw new UserFriendlyException("Could not found the note, maybe it's deleted!");
            return note;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            var newNote = await _noteRepository.InsertAsync(note);
            return newNote;
        }

        public async Task DeleteAsync(Note note)
        {
            await _noteRepository.DeleteAsync(note);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _noteRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Note note)
        {
            await _noteRepository.UpdateAsync(note);
        }
    }
}
