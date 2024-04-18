using YellowPages.Application.Addresses.Commands.CreateAddress;
using YellowPages.Application.Addresses.Commands.DeleteAddress;
using YellowPages.Application.Addresses.Commands.UpdateAddress;
using YellowPages.Application.Addresses.Queries.GetAddresses;
using YellowPages.Domain.Enums;

namespace YellowPages.Web.Endpoints;

public class Addresses : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAddresses)
            .MapGet(GetAddressesByType, "byType/{addressType}")
            .MapGet(GetAddressesByCity, "byCity/{city}")
            .MapGet(GetAddressesByPersonId, "byPersonId/{personId}")
            .MapPost(CreateAddress)
            .MapPut(UpdateAddress, "{id}")
            .MapDelete(DeleteAddress, "{id}");
    }

    public async Task<AddressVm> GetAddresses(ISender sender)
    {
        return await sender.Send(new GetAddressesQuery());
    }

    public async Task<AddressVm> GetAddressesByType(ISender sender, AddressType addressType)
    {
        var query = new GetAddressesByTypeQuery { AddressType = addressType };
        return await sender.Send(query);
    }

    public async Task<AddressVm> GetAddressesByCity(ISender sender, string city)
    {
        var query = new GetAddressesByCityQuery { City = city };
        return await sender.Send(query);
    }

    public async Task<AddressVm> GetAddressesByPersonId(ISender sender, int personId)
    {
        var query = new GetAddressesByPersonIdQuery { PersonId = personId };
        return await sender.Send(query);
    }

    public async Task<int> CreateAddress(ISender sender, CreateAddressCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateAddress(ISender sender, int id, UpdateAddressCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteAddress(ISender sender, int id)
    {
        await sender.Send(new DeleteAddressCommand(id));
        return Results.NoContent();
    }
}
