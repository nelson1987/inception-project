using Inception.Core;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using SharpCompress.Common;

namespace Inception.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Empregado> Produtos { get; set; }
    public virtual DbSet<User> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseSqlServer
            //  ("Data Source=Macoratti;Initial Catalog=InventarioDB;Integrated Security=True");
            var connectionString = "mongodb://root:password@localhost:27017/";
            var databaseName = "sales";
            optionsBuilder.UseMongoDB(connectionString, databaseName);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empregado>(entity =>
        {
            entity.ToCollection("documents");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Imagem)
                .HasMaxLength(250);
            entity.Property(e => e.Nascimento)
                .HasColumnType("date");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(18, 2)");
            entity.HasOne(x => x.Endereco);
        });
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
    }
}