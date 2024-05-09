using Inception.Database;
using Inception.Domain;

namespace Inception.Core.Features.ContaBancarias.Abertura;

public interface IAberturaContaHandler
{
    Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default);
}

public class AberturaContaHandler : IAberturaContaHandler
{
    private readonly IInceptionDbContext _context;

    public AberturaContaHandler(IInceptionDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default)
    {
        await _context.Usuarios.AddAsync(new User() { Id = command.Id, Username = "superman", Password = "superman", Role = "Manager" });
        //_context.Save
    }
}