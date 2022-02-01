using FastDrink.Application.Common.Interfaces;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.BaseTypes.Queries;

public class GetAllBaseTypeQuery<T> : IRequest<IList<BaseType>> where T : BaseType
{
}

public class GetAllBaseTypeQueryHandler<T> : IRequestHandler<GetAllBaseTypeQuery<T>, IList<BaseType>> where T : BaseType
{
    private readonly DbSet<T> _set;

    public GetAllBaseTypeQueryHandler(IApplicationDbContext context)
    {
        _set = context.GetDbSetType<T>();
    }

    public async Task<IList<BaseType>> Handle(GetAllBaseTypeQuery<T> request, CancellationToken cancellationToken)
    {
        return await _set.ToListAsync<BaseType>(cancellationToken);
    }
}
