using AntecipacaoDeRecebivel.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AntecipacaoDeRecebivel.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<EmpresaDbModel> Empresas { get; set; }
        public DbSet<NotaFiscalDbModel> NotasFiscais { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF Configuration
            modelBuilder.Entity<EmpresaDbModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Cnpj).IsRequired().HasMaxLength(14);
                entity.Property(e => e.FaturamentoMensal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Ramo).IsRequired().HasConversion<int>();
                entity.HasIndex(e => e.Cnpj).IsUnique();
            });

            modelBuilder.Entity<NotaFiscalDbModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Numero).IsRequired().HasMaxLength(9);
                entity.Property(e => e.Valor).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.ValorAntecipado).HasColumnType("decimal(18,2)");
                entity.HasOne<EmpresaDbModel>()
                      .WithMany()
                      .HasForeignKey(e => e.EmpresaId);
                entity.Property(e => e.DataDeVencimento).IsRequired();
                entity.HasIndex(e => e.Numero).IsUnique();
                entity.HasIndex(e => e.EmpresaId);
            });

            modelBuilder.Entity<NotaFiscalDbModel>()
                .HasOne(n => n.Empresa)
                .WithMany(e => e.NotasFiscais)
                .HasForeignKey(n => n.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
