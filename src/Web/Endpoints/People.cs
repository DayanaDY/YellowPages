using Microsoft.AspNetCore.Mvc;
using YellowPages.Application.People.Commands.CreatePerson;
using YellowPages.Application.People.Commands.DeletePerson;
using YellowPages.Application.People.Commands.UpdatePerson;
using YellowPages.Application.People.Queries.GetPeople;

namespace YellowPages.Web.Endpoints;

public class People : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetPeople)
            .MapPost(CreatePerson)
            .MapPut(UpdatePerson, "{id}")
            .MapDelete(DeletePerson, "{id}");
    }

    [ProducesResponseType(typeof(PersonDto), 200)]
    public async Task<PersonVm> GetPeople(ISender sender)
    {
        return await sender.Send(new GetPeopleQuery());
    }

    public async Task<int> CreatePerson(ISender sender, CreatePersonCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdatePerson(ISender sender, int id, UpdatePersonCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeletePerson(ISender sender, int id)
    {
        await sender.Send(new DeletePersonCommand(id));
        return Results.NoContent();
    }
}
