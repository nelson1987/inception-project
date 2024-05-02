using Inception.Api.Contracts;

namespace Inception.Api.Features.Empregados.Create;

public interface IEmpregadoCreateHandler
{
    Task Handle(CreateEmpregadoRequest request,CancellationToken cancellationToken = default);
}

public class EmpregadoCreateHandler : IEmpregadoCreateHandler
{
    public Task Handle(CreateEmpregadoRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}