using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Organizer.Notes.Dto;
using System;
using System.Threading.Tasks;

namespace Organizer.Notes
{
    public interface INoteAppService: IApplicationService
    {
        
        Task<ListResultDto<NoteListDto>> GetList();

        Task<NoteDetailOutput> GetDetail(EntityDto<Guid> input);

        Task<NoteDetailOutput> Create(CreateNoteInput input);
        
        Task Delete(EntityDto<Guid> input);
        
        Task Update(UpdateNoteInput input);

    }
}
