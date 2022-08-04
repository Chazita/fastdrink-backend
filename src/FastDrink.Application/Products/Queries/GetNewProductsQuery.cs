using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Queries;

public class GetNewProductsQuery : IRequest<IList<ProductDto>>
{
}

public class GetNewProductsQueryHandler : IRequestHandler<GetNewProductsQuery, IList<ProductDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNewProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IList<ProductDto>> Handle(GetNewProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Where(x => x.DeletedAt == null)
            .OrderByDescending(x => x.Created)
            .Take(6)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
