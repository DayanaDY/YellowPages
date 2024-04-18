using System.Runtime.Serialization;
using YellowPages.Application.Common.Interfaces;
using YellowPages.Domain.Entities;

namespace YellowPages.Application.TelephoneNumbers.Commands.CreateTelephoneNumber;

public record CreateTelephoneNumberCommand : IRequest<int>
{
    public string? Number { get; init; }

    [DataMember]
    public int AddressId { get; init; }
}

public class CreateTelephoneNumberCommandHandler : IRequestHandler<CreateTelephoneNumberCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTelephoneNumberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTelephoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = new TelephoneNumber();

        entity.Number = request.Number;
        entity.AddressId = request.AddressId;

        var address = _context.Addresses.First(x => x.Id == request.AddressId);
        if (address.TelephoneNumbers == null || !address.TelephoneNumbers.Any())
        {
            address.TelephoneNumbers = new List<TelephoneNumber>();
        }

        entity.Address = address;
        address.TelephoneNumbers.Add(entity);
        _context.TelephoneNumbers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
