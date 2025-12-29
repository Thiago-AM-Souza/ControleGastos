using ControleGastos.Domain.Categorias;
using ControleGastos.Domain.Pessoas;
using ControleGastos.Domain.Transacoes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ControleGastos.Infrastructure.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
