using YellowPages.Application.TelephoneNumbers.Commands.CreateTelephoneNumber;
using YellowPages.Application.TelephoneNumbers.Commands.DeleteTelephoneNumber;
using YellowPages.Application.TelephoneNumbers.Commands.UpdateTelephoneNumber;
using YellowPages.Application.TelephoneNumbers.Queries.GetTelephoneNumbers;

namespace YellowPages.Web.Endpoints;

public class TelephoneNumber : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTelephoneNumbers)
            .MapPost(CreateTelephoneNumber)
            .MapPut(UpdateTelephoneNumber, "{id}")
            .MapDelete(DeleteTelephoneNumber, "{id}");
    }

    public async Task<TelephoneNumberVm> GetTelephoneNumbers(ISender sender)
    {
        return await sender.Send(new GetTelephoneNumbersQuery());
    }

    public async Task<int> CreateTelephoneNumber(ISender sender, CreateTelephoneNumberCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateTelephoneNumber(ISender sender, int id, UpdateTelephoneNumberCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTelephoneNumber(ISender sender, int id)
    {
        await sender.Send(new DeleteTelephoneNumberCommand(id));
        return Results.NoContent();
    }
}
