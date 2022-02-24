using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.BaseTypes.Commands;

public class CreateBaseTypeCommand<T> : IRequest<Result> where T : BaseType
{
    public string Name { get; set; }
}

public class CreateBaseTypeCommandHandler<T> : IRequestHandler<CreateBaseTypeCommand<T>, Result> where T : BaseType
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _set;

    public CreateBaseTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _set = _context.GetDbSetType<T>();
    }

    public async Task<Result> Handle(CreateBaseTypeCommand<T> request, CancellationToken cancellationToken)
    {
        var nameLower = request.Name.ToLower();
        var alreadyExist = await _set.FirstOrDefaultAsync(x => x.Name == nameLower, cancellationToken);

        if (alreadyExist != null)
        {
            return Result.Failure(new[]
            {
                $"Already exist a {typeof(T).Name} with the name {request.Name}."
            });
        }

        T? entity = Activator.CreateInstance(typeof(T)) as T;

        entity.Name = nameLower;

        await _set.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
