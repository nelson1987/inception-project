using InceptionClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InceptionClean.Infrastructure.DbContexts;
public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
}