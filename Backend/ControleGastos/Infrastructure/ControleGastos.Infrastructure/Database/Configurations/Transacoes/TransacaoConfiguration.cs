using ControleGastos.Domain.Categorias.Enums;
using ControleGastos.Domain.Transacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.Infrastructure.Database.Configurations.Transacoes
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("transacao", "transacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasColumnName("descricao")
                   .HasMaxLength(300)
                   .IsRequired();

            builder.ComplexProperty(x => x.Valor, valorBuilder =>
            {
                valorBuilder.Property(v => v.Total)
                            .HasColumnName("valor")
                            .IsRequired();
            });

            builder.Property(x => x.Tipo)
                   .HasColumnName("tipo")
                   .HasConversion(
                        f => (int)f,
                        f => (Finalidade)f
                    )
                   .IsRequired();

            builder.Property(x => x.CategoriaId)
                   .HasColumnName("categoria_id");

            builder.Property(x => x.PessoaId)
                   .HasColumnName("pessoa_id");

            builder.HasOne(x => x.Categoria)
                   .WithMany()
                   .HasForeignKey(x => x.CategoriaId);
        }
    }
}
