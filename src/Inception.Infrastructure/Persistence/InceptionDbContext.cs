using Inception.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Inception.Infrastructure.Persistence;
public interface IInceptionDbContext
{
    DbSet<User> Usuarios { get; init; }

}
public partial class InceptionDbContext : DbContext, IInceptionDbContext
{
    public InceptionDbContext()
    { }

    public InceptionDbContext(DbContextOptions<InceptionDbContext> options)
        : base(options)
    { }

    public virtual DbSet<User> Usuarios { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToCollection("stocks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username)
                .HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(250);
            entity.Property(e => e.Role)
                .HasMaxLength(250);
        });
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" });
        modelBuilder.Entity<User>().HasData(new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" });
    }
}