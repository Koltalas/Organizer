using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Organizer.Contacts;
using Organizer.Contacts.Dto;
using Shouldly;
using Xunit;

namespace Organizer.Tests.Contacts
{
    public class ContactAppService_Tests : OrganizerTestBase
    {
        private readonly IContactAppService _contactAppService;

        public ContactAppService_Tests()
        {
            _contactAppService = Resolve<IContactAppService>();
        }

        [Fact]
        public async Task GetList_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            await UsingDbContextAsync(async context =>
            {
                context.Contacts.Add(Contact.Create("TestContact", (await GetCurrentUserAsync()).Id));
            });

            //Act
            var output = await _contactAppService.GetList();

            //Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetDetail_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            Contact testContact = null;
            await UsingDbContextAsync(async context =>
            {
                testContact = Contact.Create("TestContact", (await GetCurrentUserAsync()).Id);
                context.Contacts.Add(testContact);
            });

            //Act
            var output = await _contactAppService.GetDetail(
                new EntityDto<Guid>(id: testContact.Id));

            //Assert
            output.ShouldNotBeNull();

        }

        [Fact]
        public async Task Create_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            await _contactAppService.Create(
                new CreateContactInput
                {
                    FName = "TfName",
                    LName = "TlName",
                    Email = "Temail",
                    PhoneNumber = "TphoneNumber",
                    Birthday = new DateTime(2008, 3, 1, 7, 0, 0),
                    Adress = "Tadress"
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testContact = await context.Contacts.FirstOrDefaultAsync(e => e.FName == "TfName");
                testContact.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Delete_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act

            Contact createdContact = null;
            await UsingDbContextAsync(async context =>
            {
                createdContact = Contact.Create("TestContact", (await GetCurrentUserAsync()).Id);
                context.Contacts.Add(createdContact);
            });

            await _contactAppService.Delete(
                new EntityDto<Guid>
                {
                    Id = createdContact.Id
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testContact = await context.Contacts.FirstOrDefaultAsync(e => e.Id == createdContact.Id);
                testContact.IsDeleted.ShouldBeTrue();
            });
        }

        [Fact]
        public async Task Update_Test()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            Contact createdContact = null;
            await UsingDbContextAsync(async context =>
            {
                createdContact = Contact.Create("TestContact", (await GetCurrentUserAsync()).Id);
                context.Contacts.Add(createdContact);
            });

            await _contactAppService.Update(
                new UpdateContactInput
                {
                    Id = createdContact.Id,
                    FName = "UpdatedTestFName"
                });

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var testContact1 = await context.Contacts.FirstOrDefaultAsync(e => e.FName == "UpdatedTestFName");
                var testContact2 = await context.Contacts.FirstOrDefaultAsync(e => e.FName == "TfName");
                testContact1.ShouldNotBeNull();
                testContact2.ShouldBeNull();
            });
        }
    }
}
