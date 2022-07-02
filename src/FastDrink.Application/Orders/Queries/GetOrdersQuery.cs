using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Orders.DTOs;
using MediatR;

namespace FastDrink.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<PaginatedList<OrderAdminDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderAdminDto>>
{
    private readonly IApplicationDbContext _context;
    public readonly IMapper _mapper;

    public GetOrdersQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<OrderAdminDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Order
            .ProjectTo<OrderAdminDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}