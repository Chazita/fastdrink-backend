using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands;

public class DeleteDetailsCommand<T> : IRequest<Result> where T : BaseDetails
{
    public int Id { get; set; }
}

public class DeleteDetailsCommandHandler<T> : IRequestHandler<DeleteDetailsCommand<T>, Result> where T : BaseDetails
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _set;

    public DeleteDetailsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _set = _context.GetDbSetDetails<T>();
    }

    public async Task<Result> Handle(DeleteDetailsCommand<T> request, CancellationToken cancellationToken)
    {
        var details = await _set.FirstOrDefaultAsync(x => x.ProductId == request.Id, cancellationToken);

        if (details == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Producto", "Producto no existe.");
            return Result.Failure(errors);
        }

        _set.Remove(details);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
