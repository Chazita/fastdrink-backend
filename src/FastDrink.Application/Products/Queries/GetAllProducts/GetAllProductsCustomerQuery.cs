using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using MediatR;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsCustomerQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllProductsCustomerQueryHanlder : IRequestHandler<GetAllProductsCustomerQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductsCustomerQueryHanlder(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(x => x.DeletedAt == null)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
