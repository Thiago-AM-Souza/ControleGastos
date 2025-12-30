using ControleGastos.Domain.Categorias;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Pessoas;
using ControleGastos.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories.Categorias
{
    internal class CategoriaRepository(AppDbContext dbContext) : ICategoriaRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task Cadastrar(Categoria categoria)
        {
            await _dbContext.Categorias.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> Listar()
        {
            var categorias = await _dbContext.Categorias.ToListAsync();

            if (categorias is null)
            {
                return Enumerable.Empty<Categoria>();
            }

            return categorias;
        }

        public async Task<IEnumerable<Categoria>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken)
        {
            var categorias = await _dbContext.Categorias
                                          .OrderBy(x => x.Descricao)
                                          .AsNoTracking()
                                          .Skip(tamanho * index)
                                          .Take(tamanho)
                                          .ToListAsync(cancellationToken);

            if (categorias is null)
            {
                return Enumerable.Empty<Categoria>();
            }

            return categorias;
        }

        public Task<long> ObterTotalCadastrados()
        {
            var total = _dbContext.Categorias.LongCountAsync();

            return total;
        }
    }
}
