using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Organizer.Notes;
using Organizer.Notes.Dto;
using Shouldly;
using Xunit;

namespace Organizer.Tests.Notes
{
    public class NoteAppService_Tests : OrganizerTestBase
    {
        private readonly INoteAppService _noteAppService;

        public NoteAppService_Tests()
        {
            _noteAppService = Resolve<INoteAppService>();
        }

        [Fact]
        public async Task GetList_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            await UsingDbContextAsync(async context =>
            {
                context.Notes.Add(Note.Create("TestNote", (await GetCurrentUserAsync()).Id));
            });

            //Act
            var output = await _noteAppService.GetList();

            //Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetDetail_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            Note testNote = null;
            await UsingDbContextAsync(async context =>
            {
                testNote = Note.Create("TestNote", (await GetCurrentUserAsync()).Id);
                context.Notes.Add(testNote);
            });

            //Act
            var output = await _noteAppService.GetDetail(
                new EntityDto<Guid>(id: testNote.Id));

            //Assert
            output.ShouldNotBeNull();

        }

        [Fact]
        public async Task Create_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            await _noteAppService.Create(
                new CreateNoteInput
                {
                    Content = "TestContent",
                    HashTag = "#test",
                    Title = "TestTitle"
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testNote = await context.Notes.FirstOrDefaultAsync(e => e.Title == "TestTitle");
                testNote.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Delete_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act

            Note createdNote = null;
            await UsingDbContextAsync(async context =>
            {
                createdNote = Note.Create("TestNote", (await GetCurrentUserAsync()).Id);
                context.Notes.Add(createdNote);
            });

            await _noteAppService.Delete(
                new EntityDto<Guid>
                {
                    Id = createdNote.Id
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testNote = await context.Notes.FirstOrDefaultAsync(e => e.Id == createdNote.Id);
                testNote.IsDeleted.ShouldBeTrue();
            });
        }

        [Fact]
        public async Task Update_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            Note createdNote = null;
            await UsingDbContextAsync(async context =>
            {
                createdNote = Note.Create("TestNote", (await GetCurrentUserAsync()).Id);
                context.Notes.Add(createdNote);
            });

            await _noteAppService.Update(
                new UpdateNoteInput
                {
                    Id = createdNote.Id,
                    Title = "UpdatedTestTitle"
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testNote1 = await context.Notes.FirstOrDefaultAsync(e => e.Title == "UpdatedTestTitle");
                var testNote2 = await context.Notes.FirstOrDefaultAsync(e => e.Title == "TestNote");
                testNote1.ShouldNotBeNull();
                testNote2.ShouldBeNull();
            });
        }
    }
}
