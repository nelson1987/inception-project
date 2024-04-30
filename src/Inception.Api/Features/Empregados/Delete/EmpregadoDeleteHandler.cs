using Inception.Api.ResponseHandlers;

namespace Inception.Api.Features.Empregados.Delete;

public interface IEmpregadoDeleteHandler
{
    Task<Response> Handle(CancellationToken cancellationToken = default);
}

public class EmpregadoDeleteHandler : IEmpregadoDeleteHandler
{
    private readonly IEmpregadoRepository _empregadoRepository;

    public EmpregadoDeleteHandler(IEmpregadoRepository empregadoRepository)
    {
        _empregadoRepository = empregadoRepository;
    }

    public async Task<Response> Handle(CancellationToken cancellationToken = default)
    {
        //Response response = new NotFoundResponse();
        //var empregado = _empregadoRepository.GetByIdAsync(cancellationToken);
        //if (empregado == null) return response;

        //return response;
        throw new NotImplementedException();
    }
}