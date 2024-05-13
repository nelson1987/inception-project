using Inception.Core.Entities;

namespace Inception.Infrastructure;
public interface IUserRepository
{
    Task<User?> Get(string username, string password, CancellationToken cancellationToken = default);
}