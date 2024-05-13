using Inception.Core.Entities;
using Inception.Core.UseCases;
using Inception.Infrastructure.Persistence;

namespace Inception.Application.Features.ContaBancarias.Abertura;

public class AberturaContaHandler(IInceptionDbContext context) : IAberturaContaHandler
{
    public async Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default)
    {
        await context.Usuarios.AddAsync(new User() { Id = command.Id, Username = "superman", Password = "superman", Role = "Manager" }, cancellationToken);
    }
}