using YellowPages.Application.Common.Interfaces;
using YellowPages.Domain.Entities;
using YellowPages.Domain.Enums;

namespace YellowPages.Application.Addresses.Commands.CreateAddress;

public record CreateAddressCommand : IRequest<int>
{
    public string? Street { get; init; }

    public string? City { get; init;}

    public string? PostalCode { get; init; }

    public AddressType? AddressType { get; init; }

    public int PersonId { get; init; }

}

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAddressCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = new Address();

        entity.Street = request.Street;
        entity.City = request.City;
        entity.PersonId = request.PersonId;
        entity.AddressType = request.AddressType;

        /// get the person from the context and add the address to his list of addresses

        var person = _context.People.First(x => x.Id == request.PersonId);
        if (person.Addresses == null || !person.Addresses.Any()) {
            person.Addresses = new List<Address>();
        }

        entity.Person = person;
        person.Addresses.Add(entity);
        _context.Addresses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
