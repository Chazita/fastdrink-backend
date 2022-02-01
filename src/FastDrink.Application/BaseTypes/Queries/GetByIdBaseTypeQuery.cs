using FastDrink.Application.Common.Interfaces;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.BaseTypes.Queries;

public class GetByIdBaseTypeQuery<T> : IRequest<T?> where T : BaseType
{
    public int Id { get; set; }
}

public class GetByIdBaseTypeQueryHandler<T> : IRequestHandler<GetByIdBaseTypeQuery<T>, T?> where T : BaseType
{
    private readonly DbSet<T> _set;

    public GetByIdBaseTypeQueryHandler(IApplicationDbContext context)
    {
        _set = context.GetDbSetType<T>();
    }

    public async Task<T?> Handle(GetByIdBaseTypeQuery<T> request, CancellationToken cancellationToken)
    {
        return await _set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
