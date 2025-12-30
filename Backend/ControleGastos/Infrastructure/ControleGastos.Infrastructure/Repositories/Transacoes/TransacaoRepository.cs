using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Transacoes;
using ControleGastos.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories.Transacoes
{
    internal class TransacaoRepository(AppDbContext dbContext) : ITransacaoRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task Cadastrar(Transacao transacao)
        {
            await _dbContext.Transacoes.AddAsync(transacao);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transacao>> Listar()
        {
            var transacoes = await _dbContext.Transacoes.ToListAsync();

            if (transacoes is null)
            {
                return Enumerable.Empty<Transacao>();
            }

            return transacoes;
        }

        public async Task<IEnumerable<Transacao>> ListarPaginado(int index, int tamanho, CancellationToken cancellationToken)
        {
            var transacao = await _dbContext.Transacoes
                                            .Include(x => x.Pessoa)
                                            .Include(x => x.Categoria)
                                            .AsNoTracking()                                            
                                            .Skip(tamanho * index)
                                            .Take(tamanho)
                                            .ToListAsync(cancellationToken);

            if (transacao is null)
            {
                return Enumerable.Empty<Transacao>();
            }

            return transacao;
        }

        public async Task<long> ObterTotalCadastrados()
        {
            return await _dbContext.Transacoes.LongCountAsync();
        }
    }
}
