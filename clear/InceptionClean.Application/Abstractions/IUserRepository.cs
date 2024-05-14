using InceptionClean.Domain.Entities;

namespace InceptionClean.Application.Abstractions;
public interface IUserRepository
{
    Task<User> GetPerson(string login, string password, CancellationToken cancellationToken = default);
}