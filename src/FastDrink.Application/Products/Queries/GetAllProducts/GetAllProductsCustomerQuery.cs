using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Queries.GetAllProducts;

public class GetAllProductsCustomerQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public string? OrderBy { get; set; }
    public string? Brand { get; set; }
    public string? Search { get; set; }
}

public class GetAllProductsCustomerQueryHandler : IRequestHandler<GetAllProductsCustomerQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProductsCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsCustomerQuery request, CancellationToken cancellationToken)
    {
        var products = _context.Products.AsQueryable();

        products.Include(x => x.Brand);

        if (request.Brand != null)
        {
            products = products.Where(x => x.Brand!.Name == request.Brand).AsQueryable();
        }

        if (request.Search != null)
        {
            products = products
                .Where(x => x.Name.ToLower() == request.Search ||
                    x.Name.ToLower().Contains(request.Search) ||
                    x.Category!.Name.ToLower() == request.Search ||
                    x.Category.Name.ToLower().Contains(request.Search) ||
                    x.Brand!.Name.ToLower() == request.Search ||
                    x.Brand.Name.ToLower().Contains(request.Search))
                .AsQueryable();
        }

        switch (request.OrderBy)
        {
            case "more_recent":
                return await products
                    .OrderByDescending(x => x.Created)
                    .Where(x => x.DeletedAt == null)
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            case "highest_price":
                return await products
                    .OrderByDescending(x => x.Price)
                    .Where(x => x.DeletedAt == null)
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            case "lowest_price":
                return await products
                    .OrderBy(x => x.Price)
                    .Where(x => x.DeletedAt == null)
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            default:
                return await products
                    .OrderBy(x => x.Name)
                    .Where(x => x.DeletedAt == null)
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
