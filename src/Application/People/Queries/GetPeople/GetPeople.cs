using YellowPages.Application.Common.Interfaces;
using YellowPages.Application.Common.Security;

namespace YellowPages.Application.People.Queries.GetPeople;

[Authorize]
public record GetPeopleQuery : IRequest<PersonVm>;

public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, PersonVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPeopleQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PersonVm> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        return new PersonVm
        {
            Lists = await _context.People
                .AsNoTracking()
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken)
        };
    }
}
