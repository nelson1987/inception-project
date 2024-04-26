using Microsoft.EntityFrameworkCore;

namespace Inception.Database;
public partial class AppDbContext : DbContext
{
    public AppDbContext()
    { }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    public virtual DbSet<Produto> Produtos { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
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
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Imagem).HasMaxLength(250);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Preco).HasColumnType("decimal(18, 2)");
        });
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(80);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
