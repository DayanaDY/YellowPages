using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public record GetAddressesByPersonIdQuery : IRequest<AddressVm>
{
    public int PersonId { get; init; } = 0;
}

public class GetAddressesByPersonIdQueryHandler : IRequestHandler<GetAddressesByPersonIdQuery, AddressVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAddressesByPersonIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(GetAddressesByPersonIdQuery request, CancellationToken cancellationToken)
    {
        return new AddressVm
        {
            Lists = await _context.Addresses
               .AsNoTracking()
               .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
               .Where(x => x.PersonId == request.PersonId)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken)
        };
    }
}
