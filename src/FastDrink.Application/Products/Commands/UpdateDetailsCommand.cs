using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands;

public class UpdateDetailsCommand<T> : IRequest<Result> where T : BaseDetails
{
    public T Entity { get; set; }
}

public class UpdateDetailsCommandHanlder<T> : IRequestHandler<UpdateDetailsCommand<T>, Result> where T : BaseDetails
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _set;

    public UpdateDetailsCommandHanlder(IApplicationDbContext context)
    {
        _context = context;
        _set = _context.GetDbSetDetails<T>();
    }

    public async Task<Result> Handle(UpdateDetailsCommand<T> request, CancellationToken cancellationToken)
    {
        var entityExist = await _set.FirstOrDefaultAsync(x => x.ProductId == request.Entity.ProductId, cancellationToken);

        if (entityExist == null)
        {
            return Result.Failure(new[]
            {
                "Product don't exist"
            });
        }

        _set.Update(request.Entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
