using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Orders.Commands;

public class UpdateOrderStatusCommand : IRequest<Result>
{
    public string OrderId { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
}

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;

    public UpdateOrderStatusCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        int orderId = _hashids.Decode(request.OrderId)[0];
        var order = await _context
            .Order
            .Where(x => x.Id == orderId)
            .Include(x => x.Products!)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return Result.Failure(new Dictionary<string, string>
            {
                {"Orden",$"No existe orden con el Id: {request.OrderId}" }
            });
        }

        if (order.OrderStatus == Domain.Enums.OrderStatus.Declined || order.OrderStatus == Domain.Enums.OrderStatus.Canceled)
        {
            foreach (var product in order.Products!)
            {
                product.Product!.Stock -= product.Quantity;
            }
        }

        order.OrderStatusString = request.OrderStatus;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
