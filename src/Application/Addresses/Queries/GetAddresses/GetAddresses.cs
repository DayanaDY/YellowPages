using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;

namespace YellowPages.Application.Addresses.Queries.GetAddresses;

[Authorize]
public record GetAddressesQuery : IRequest<AddressVm>;

public class GetAddressQueryHandler : IRequestHandler<GetAddressesQuery, AddressVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAddressQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddressVm> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
    {
        return new AddressVm
        {
            Lists = await _context.Addresses
                .AsNoTracking()
                .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
    }
}
