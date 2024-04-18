using YellowPages.Application.Addresses.Commands.CreateAddress;
using YellowPages.Application.Addresses.Commands.DeleteAddress;
using YellowPages.Application.Addresses.Commands.UpdateAddress;
using YellowPages.Application.Addresses.Queries.GetAddresses;

namespace YellowPages.Web.Endpoints;

public class Addresses : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAddresses)
            .MapPost(CreateAddress)
            .MapPut(UpdateAddress, "{id}")
            ////.MapPut(UpdateAddressDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteAddress, "{id}");
    }

    public async Task<AddressVm> GetAddresses(ISender sender)
    {
        return await sender.Send(new GetAddressesQuery());
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

    ////public async Task<IResult> UpdateAddressDetail(ISender sender, int id, UpdateAddressCommand command)
    ////{
    ////    if (id != command.Id) return Results.BadRequest();
    ////    await sender.Send(command);
    ////    return Results.NoContent();
    ////}

    public async Task<IResult> DeleteAddress(ISender sender, int id)
    {
        await sender.Send(new DeleteAddressCommand(id));
        return Results.NoContent();
    }
}
