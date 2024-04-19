namespace YellowPages.Application.FunctionalTests.TodoItems.Commands;

using YellowPages.Application.Addresses.Commands.CreateAddress;
using YellowPages.Application.Addresses.Commands.UpdateAddress;
using YellowPages.Application.People.Commands.CreatePerson;
using YellowPages.Domain.Entities;
using static Testing;

public class UpdateAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateAddressCommand { Id = 99, Street = "New Updated Street" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var personId = await SendAsync(new CreatePersonCommand
        {
            FullName = "Test Person"
        });

        var addressId = await SendAsync(new CreateAddressCommand
        {
            Street = "New Street",
            City = "Sofia",
            AddressType = Domain.Enums.AddressType.Home,
            PersonId = personId
        });


        var command = new UpdateAddressCommand
        {
            Id = addressId,
            PersonId = personId,
            City = "Updated city",
        };

        await SendAsync(command);

        var item = await FindAsync<Address>(addressId);

        item.Should().NotBeNull();
        item!.City.Should().Be(command.City);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
