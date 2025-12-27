using ControleGastos.Domain.Categorias;
using ControleGastos.Domain.Categorias.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.Infrastructure.Database.Configurations.Categorias
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categoria", "categorias");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasColumnName("descricao")
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Finalidade)
                   .HasColumnName("finalidade")
                   .HasConversion(
                        f => (int)f,
                        f => (Finalidade)f
                    )
                   .IsRequired();
        }
    }
}
