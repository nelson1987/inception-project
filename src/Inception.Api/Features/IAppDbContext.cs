using Inception.Core.Entities;

namespace Inception.Api.Features;

public interface IProduto
{
    Task<object> ToListAsync(CancellationToken cancellationToken = default);

    Task<object?> FirstOrDefaultAsync(Func<Empregado, bool> value, CancellationToken cancellationToken);
}