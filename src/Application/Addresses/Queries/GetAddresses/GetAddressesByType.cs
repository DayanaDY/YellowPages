using YellowPages.Application.Common.Interfaces;
using YellowPages.Domain.Enums;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

public record GetAddressesByTypeQuery : IRequest<AddressVm>
{
    public AddressType AddressType { get; init; } = 0;
}

public class GetAddressesByTypeQueryHandler : IRequestHandler<GetAddressesByTypeQuery, AddressVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAddressesByTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(GetAddressesByTypeQuery request, CancellationToken cancellationToken)
    {
        return new AddressVm
        {
            Lists = await _context.Addresses
               .AsNoTracking()
               .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
               .Where(x => x.AddressType == request.AddressType)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken)
        };
    }
}
