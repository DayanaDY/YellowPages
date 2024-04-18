using Microsoft.AspNetCore.Mvc;
using YellowPages.Application.Addresses.Queries.GetAddresses;
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
            .MapGet(GetPeopleById, "byPersonId/{personId}")
            .MapPost(CreatePerson)
            .MapPut(UpdatePerson, "{id}")
            .MapDelete(DeletePerson, "{id}");
    }

    [ProducesResponseType(typeof(PersonDto), 200)]
    public async Task<PersonVm> GetPeople(ISender sender)
    {
        return await sender.Send(new GetPeopleQuery());
    }

    public async Task<PersonVm> GetPeopleById(ISender sender, int personId)
    {
        var query = new GetPeopleByIdQuery { PersonId = personId };
        return await sender.Send(query);
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
