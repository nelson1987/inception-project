using Inception.Api.ResponseHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Inception.Api.Features.Empregados.EmpregadosDelete;

public interface IEmpregadoGetAllIdHandler
{
}

public class EmpregadoGetAllIdHandler : IEmpregadoGetAllIdHandler
{

}

public interface IEmpregadoGetByIdHandler
{
}

public class EmpregadoGetByIdHandler : IEmpregadoGetByIdHandler
{

}

public interface IEmpregadoCreateHandler
{
}

public class EmpregadoCreateHandler : IEmpregadoCreateHandler
{

}

public interface IEmpregadoUpdateHandler
{
}

public class EmpregadoUpdateHandler : IEmpregadoUpdateHandler
{

}

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
        Response response;
        var empregado = _empregadoRepository.GetByIdAsync(cancellationToken);
        if(empregado == null) return new NotFoundResponse();

        return response;
    }
}
public interface IEmpregadoRepository 
{ 
    Task<Empregado?> GetByIdAsync(CancellationToken cancellationToken = default);
}