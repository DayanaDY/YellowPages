using YellowPages.Application.Addresses.Commands.CreateAddress;
using YellowPages.Application.Common.Exceptions;
using YellowPages.Application.People.Commands.CreatePerson;
using YellowPages.Domain.Entities;

namespace YellowPages.Application.FunctionalTests.Addresses.Commands;

using static Testing;

public class CreateAddressTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateAddressCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateAddress()
    {
        var userId = await RunAsDefaultUserAsync();

        var personId = await SendAsync(new CreatePersonCommand
        {
            FullName = "Test Person"
        });

        var command = new CreateAddressCommand
        {
            Street = "New Street",
            City = "Sofia",
            AddressType = Domain.Enums.AddressType.Home,
            PersonId = personId
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Address>(itemId);

        item.Should().NotBeNull();
        item!.Street.Should().Be(command.Street);
        item.City.Should().Be(command.City);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
