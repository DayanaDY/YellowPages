using YellowPages.Application.Addresses.Commands.CreateAddress;
using YellowPages.Application.Addresses.Commands.DeleteAddress;
using YellowPages.Application.People.Commands.CreatePerson;
using YellowPages.Domain.Entities;

namespace YellowPages.Application.FunctionalTests.TodoItems.Commands;

using static Testing;

public class DeleteAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteAddressCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var personId = await SendAsync(new CreatePersonCommand
        {
            FullName = "Test Person"
        });

        var address = await SendAsync(new CreateAddressCommand
        {
            Street = "New Street",
            City = "Sofia",
            AddressType = Domain.Enums.AddressType.Home,
            PersonId = personId
        });

        await SendAsync(new DeleteAddressCommand(address));

        var item = await FindAsync<Address>(address);

        item.Should().BeNull();
    }
}
