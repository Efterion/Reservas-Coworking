using Coworking.Reservas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Reservas.Infrastructure.Data;

public class ReservasDbContext : DbContext
{
    public ReservasDbContext(DbContextOptions<ReservasDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Espacio> Espacios { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.HasKey(u => u.DNI);

            entity.Property(u => u.DNI)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(u => u.Nombre)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(u => u.Email)
                  .HasMaxLength(200)
                  .IsRequired();

            entity.HasIndex(u => u.Email).IsUnique();
        });

        // Espacio
        modelBuilder.Entity<Espacio>(entity =>
        {
            entity.ToTable("Espacios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).HasMaxLength(150).IsRequired();
        });

        // Reserva
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.ToTable("Reservas");

            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Usuario)
                  .WithMany(u => u.Reservas)
                  .HasForeignKey(r => r.UsuarioDNI)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.Espacio)
                  .WithMany(e => e.Reservas)
                  .HasForeignKey(r => r.EspacioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}