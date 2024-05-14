using InceptionClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InceptionClean.Infrastructure.DbContexts;
public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
}