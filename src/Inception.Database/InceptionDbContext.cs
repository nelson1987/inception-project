using Inception.Core;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Inception.Database;

public partial class InceptionDbContext : DbContext
{
    public InceptionDbContext()
    { }

    public InceptionDbContext(DbContextOptions<InceptionDbContext> options)
        : base(options)
    { }

    public virtual DbSet<User> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "mongodb://root:password@localhost:27017/";
            var databaseName = "sales";
            optionsBuilder.UseMongoDB(connectionString, databaseName);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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