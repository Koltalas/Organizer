using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Organizer.Notes.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;

namespace Organizer.Notes
{
    [AbpAuthorize]
    public class NoteAppService : OrganizerAppServiceBase, INoteAppService
    {
        private readonly INoteManager _noteManager;
        private readonly IRepository<Note, Guid> _noteRepository;

        public NoteAppService(
            INoteManager noteManager,
            IRepository<Note, Guid> noteRepository)
        {
            _noteManager = noteManager;
            _noteRepository = noteRepository;
        }

        public async Task<NoteDetailOutput> Create(CreateNoteInput input)
        {
            var note = Note.Create(input.Title, AbpSession.UserId.Value, input.Content, input.HashTag);
            var newNote = await _noteManager.CreateAsync(note);
            return newNote.MapTo<NoteDetailOutput>();
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);
            
            if (note == null)
                throw new UserFriendlyException("Could not found the note, maybe it's already deleted.");

            await _noteManager.DeleteAsync(note);
        }

        public async Task<NoteDetailOutput> GetDetail(EntityDto<Guid> input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);

            if (note == null)
                throw new UserFriendlyException("Could not found the note, maybe it's deleted.");

            return note.MapTo<NoteDetailOutput>();

        }

        public async Task<ListResultDto<NoteListDto>> GetList()
        {
            var notes = await _noteRepository.GetAllListAsync(e => e.UserId == AbpSession.UserId);
            
            return new ListResultDto<NoteListDto>(notes.MapTo<List<NoteListDto>>());
        }

        public async Task Update(UpdateNoteInput input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(e => e.Id == input.Id && e.UserId == AbpSession.UserId);

            if (note == null)
                throw new UserFriendlyException("Could not found the note, maybe it's deleted.");

            note.Update(input.Title, input.Content, input.HashTag, input.UserId);
            await _noteManager.UpdateAsync(note);
        }
    }
}
