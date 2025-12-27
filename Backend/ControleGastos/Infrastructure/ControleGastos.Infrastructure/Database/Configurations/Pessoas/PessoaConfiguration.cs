using ControleGastos.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.Infrastructure.Database.Configurations.Pessoas
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("pessoas", "pessoa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(x => x.Nome)
                   .HasColumnName("nome")
                   .HasMaxLength(80)
                   .IsRequired();

            builder.Property(x => x.DataNascimento)
                   .HasColumnName("data_nascimento")
                   .IsRequired();

            builder.HasMany(x => x.Transacoes)
                   .WithOne(t => t.Pessoa)
                   .HasForeignKey(t => t.PessoaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
