using Inception.Domain.Entities;
using Inception.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inception.Infrastructure.Persistence.Repositories;
public class UserRepository(IInceptionDbContext context) : IUserRepository
{
    private readonly IInceptionDbContext _context = context;

    public async Task<User?> Get(string username, string password, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase) && x.Password == password, cancellationToken);
    }
}