using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.BaseTypes.Commands;

public class UpdateBaseTypeCommand<T> : IRequest<Result> where T : BaseType
{
    public int Id { get; set; }
    public string NewName { get; set; } = "";
}

public class UpdateBaseTypeCommandHandler<T> : IRequestHandler<UpdateBaseTypeCommand<T>, Result> where T : BaseType
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _set;

    public UpdateBaseTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _set = _context.GetDbSetType<T>();
    }

    public async Task<Result> Handle(UpdateBaseTypeCommand<T> request, CancellationToken cancellationToken)
    {
        var entity = await _set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Id", $"No existe un {typeof(T).Name} con el Id: {request.Id}.");
            return Result.Failure(errors);
        }

        entity.Name = request.NewName.ToLower();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
