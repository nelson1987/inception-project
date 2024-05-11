namespace Inception.Domain.Repositories;
public interface IUserRepository
{
    Task<User?> Get(string username, string password, CancellationToken cancellationToken = default);
}