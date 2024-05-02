using Microsoft.EntityFrameworkCore;

namespace Inception.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Empregado> Produtos { get; set; }
    public virtual DbSet<Endereco> Enderecos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer
              ("Data Source=Macoratti;Initial Catalog=InventarioDB;Integrated Security=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empregado>(entity =>
        {
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
        });
        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rua)
                .HasMaxLength(250);
        });
        //OnModelCreatingPartial(modelBuilder);
    }

    //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}