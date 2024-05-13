using Inception.Core.Entities;

namespace Inception.Core.Repositories;
public interface IUserRepository
{
    Task<User?> Get(string username, string password, CancellationToken cancellationToken = default);
}