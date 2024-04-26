using Inception.Api.Features.Empregados;

namespace Inception.Api.Features;
public interface IAppDbContext
{
    IProduto Produtos { get; }
}
public interface IProduto
{
    Task<object> ToListAsync(CancellationToken cancellationToken = default);
    Task<object?> FirstOrDefaultAsync(Func<Empregado, bool> value, CancellationToken cancellationToken);
}