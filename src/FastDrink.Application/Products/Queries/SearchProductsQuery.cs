using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Queries;

public class SearchProductsQuery : IRequest<IList<ProductDto>>
{
    public string ProductName { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IList<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products

            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
