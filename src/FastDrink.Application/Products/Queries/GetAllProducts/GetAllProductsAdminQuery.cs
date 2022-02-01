using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using MediatR;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsAdminQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllProductsAdminQueryHanlder : IRequestHandler<GetAllProductsAdminQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllProductsAdminQueryHanlder(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsAdminQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
