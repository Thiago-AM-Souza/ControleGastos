using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Pessoas;
using ControleGastos.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories.Pessoas
{
    internal class PessoaRepository(AppDbContext dbContext) : IPessoaRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        
        public async Task Cadastrar(Pessoa pessoa)
        {
            await _dbContext.Pessoas.AddAsync(pessoa);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Excluir(Pessoa pessoa)
        {
            _dbContext.Pessoas.Remove(pessoa);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pessoa>> Listar()
        {
            var pessoas = await _dbContext.Pessoas.ToListAsync();

            if (pessoas is null)
            {
                return Enumerable.Empty<Pessoa>();
            }

            return pessoas;
        }

        public async Task<IEnumerable<Pessoa>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken)
        {
            var pessoas = await _dbContext.Pessoas
                                          .OrderBy(x => x.Nome)
                                          .AsNoTracking()
                                          .Skip(tamanho * index)
                                          .Take(tamanho)
                                          .ToListAsync(cancellationToken);

            if (pessoas is null)
            {
                return Enumerable.Empty<Pessoa>();
            }

            return pessoas;
        }

        public async Task<Pessoa?> ObterPorId(Guid id)
        {
            var pessoa = await _dbContext.Pessoas.FindAsync(id);

            return pessoa;
        }

        public Task<long> ObterTotalCadastrados()
        {
            var total = _dbContext.Pessoas.LongCountAsync();

            return total;
        }
    }
}
