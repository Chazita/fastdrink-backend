using AutoMapper;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Orders.Commands;

public class CancelOrderCommand : IRequest<Result>
{
    public string OrderId { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
}

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;
    private readonly IMapper _mapper;

    public CancelOrderCommandHandler(IApplicationDbContext context, IHashids hashids, IMapper mapper)
    {
        _context = context;
        _hashids = hashids;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = _hashids.Decode(request.OrderId)[0];

        var order = await _context.Order
            .Where(x => x.Id == orderId)
            .Include(x => x.Products!)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return Result.Failure(
                new Dictionary<string, string> { { "Orden", $"No existe la orden con el Id:{request.OrderId}" } });
        }

        if (order.OrderStatus != Domain.Enums.OrderStatus.Pending)
        {
            return Result.Failure(new Dictionary<string, string> { { "Orden", "La orden no es pendiente." } });
        }

        foreach (var product in order.Products!)
        {
            product.Product!.Stock += product.Quantity;
        }

        order.OrderStatusString = request.OrderStatus;

        order.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
