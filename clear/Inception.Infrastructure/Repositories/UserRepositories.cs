using InceptionClean.Application.Abstractions;
using InceptionClean.Domain.Entities;
using InceptionClean.Infrastructure.DbContexts;

namespace InceptionClean.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public Task<User> GetPerson(string login, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        //return await _context.Person.FirstOfDefault();
    }
}