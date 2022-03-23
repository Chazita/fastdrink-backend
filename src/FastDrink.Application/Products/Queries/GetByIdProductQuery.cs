using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Queries;

public class GetByIdProductQuery : IRequest<ResultProduct>
{
    public int Id { get; set; }
}

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ResultProduct>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdProductQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResultProduct> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .ProjectTo<ProductWithDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Producto", $"El product con el ID: {request.Id} no existe.");

            return ResultProduct.Failure(errors);
        }

        return ResultProduct.Success(product);
    }
}

public class ResultProduct : Result
{
    public ResultProduct(bool succeeded, IDictionary<string, string> errors, ProductWithDetailsDto? product) : base(succeeded, errors)
    {
        Product = product;
    }

    public ProductWithDetailsDto? Product { get; set; }

    public static ResultProduct Success(ProductWithDetailsDto product)
    {
        return new ResultProduct(true, new Dictionary<string, string>(), product);
    }
    public static ResultProduct Failure(IDictionary<string, string> errors)
    {
        return new ResultProduct(false, errors, null);
    }
}