using AutoMapper;
using HashidsNet;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Orders.DTOs;
using MediatR;
using FastDrink.Domain.Enums;

namespace FastDrink.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<PaginatedList<OrderAdminDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = string.Empty;
    public string OrderByStatus { get; set; } = string.Empty;
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderAdminDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;

    public GetOrdersQueryHandler(IMapper mapper, IApplicationDbContext context, IHashids hashids)
    {
        _mapper = mapper;
        _context = context;
        _hashids = hashids;
    }

    public async Task<PaginatedList<OrderAdminDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderQuery = _context.Order.AsQueryable();

        if(request.Search.Length > 0 && request.Search[0] != '#'){
            int id = _hashids.Decode(request.Search)[0];

            orderQuery = orderQuery.Where(x => x.Id == id).AsQueryable();
        }

        if(request.OrderByStatus.Length > 0){
            var status = (OrderStatus)Enum.Parse(typeof(OrderStatus),request.OrderByStatus);
            
            orderQuery = orderQuery.Where(x => x.OrderStatus == status).AsQueryable();
        }

        return await orderQuery
            .ProjectTo<OrderAdminDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.LastModified)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}