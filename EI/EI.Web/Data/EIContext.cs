using EI.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EI.Web.Data;

public partial class EIContext : DbContext
{
    public EIContext(DbContextOptions<EIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Pacientes");

            entity.Property(e => e.Nombre)
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.PesoKg)
                  .HasColumnType("decimal(5,2)");

            entity.Property(e => e.EstaturaM)
                  .HasColumnType("decimal(4,2)");

            entity.Property(e => e.Imc)
                  .HasColumnType("decimal(6,4)");

            entity.Property(e => e.Padecimientos)
                  .HasMaxLength(200)
                  .IsUnicode(false);

            entity.Property(e => e.Genero)
                  .HasMaxLength(1)
                  .IsUnicode(false)
                  .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
