using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Products.Commands;

public class CreateDetailsCommand<T> : IRequest<Result> where T : BaseDetails
{
    public T Entity { get; set; }
}

public class CreateDetailsCommandHandler<T> : IRequestHandler<CreateDetailsCommand<T>, Result> where T : BaseDetails
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _set;

    public CreateDetailsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _set = _context.GetDbSetDetails<T>();
    }
    public async Task<Result> Handle(CreateDetailsCommand<T> request, CancellationToken cancellationToken)
    {
        await _set.AddAsync(request.Entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
