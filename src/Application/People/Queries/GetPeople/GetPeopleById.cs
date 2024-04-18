using YellowPages.Application.Common.Interfaces;

namespace YellowPages.Application.People.Queries.GetPeople;

public record GetPeopleByIdQuery : IRequest<PersonVm>
{
    public int PersonId { get; init; } = 0;
}

public class GetPeopleByIdQueryHandler : IRequestHandler<GetPeopleByIdQuery, PersonVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPeopleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PersonVm> Handle(GetPeopleByIdQuery request, CancellationToken cancellationToken)
    {
        return new PersonVm
        {
            Lists = await _context.People
               .AsNoTracking()
               .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
               .Where(x => x.Id == request.PersonId)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken)
        };
    }
}
