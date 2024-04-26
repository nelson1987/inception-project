using Inception.Database;

namespace Inception.Api.Features.Empregados;

public interface IEmpregadoRepository
{
    Task<Empregado?> GetByIdAsync(CancellationToken cancellationToken = default);
}

public class EmpregadoRepository : IEmpregadoRepository
{
    public Task<Empregado?> GetByIdAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}