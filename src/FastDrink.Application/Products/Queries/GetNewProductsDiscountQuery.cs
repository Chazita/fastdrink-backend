using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDrink.Application.Products.Queries;

public class GetNewProductsDiscountQuery : IRequest<IList<ProductDto>>
{
}

public class GetNewProductsDiscountQueryHandler : IRequestHandler<GetNewProductsDiscountQuery, IList<ProductDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetNewProductsDiscountQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IList<ProductDto>> Handle(GetNewProductsDiscountQuery request, CancellationToken cancellationToken)
    {

        return await _dbContext.Products
            .Where(x => x.DeletedAt == null)
            .Where(x => x.Discount != null)
            .OrderByDescending(x => x.LastModified)
            .Take(6)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
