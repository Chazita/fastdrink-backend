using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Orders.DTOs;
using MediatR;

namespace FastDrink.Application.Orders.Queries;

public class GetMyOrdersQuery : IRequest<PaginatedList<OrderDto>>
{
    public int UserId { get; set; }

    public int PageNumber { get; set; } = 1;
}

public class GeyMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GeyMyOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context
            .Order
            .Where(x => x.UserId == request.UserId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, 10);

        return orders;
    }
}
