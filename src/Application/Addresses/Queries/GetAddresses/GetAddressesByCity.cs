using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public record GetAddressesByCityQuery : IRequest<AddressVm>
{
    public string? City { get; init; }
}

public class GetAddressesByCityQueryHandler : IRequestHandler<GetAddressesByCityQuery, AddressVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAddressesByCityQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(GetAddressesByCityQuery request, CancellationToken cancellationToken)
    {
        return new AddressVm
        {
            Lists = await _context.Addresses
               .AsNoTracking()
               .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
               .Where(x => x.City == request.City)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken)
        };
    }
}
