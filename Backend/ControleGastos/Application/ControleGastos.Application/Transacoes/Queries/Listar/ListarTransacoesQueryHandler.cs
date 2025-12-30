
using ControleGastos.Application.Dtos.Transacoes;
using ControleGastos.Application.Extensions;
using ControleGastos.BuildingBlocks.Pagination;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Transacoes.Queries.Listar
{
    internal class ListarTransacoesQueryHandler(ITransacaoRepository transacaoRepository)
        : IQueryHandler<ListarTransacoesQuery, ListarTransacoesResult>
    {
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

        public async Task<ListarTransacoesResult> Handle(ListarTransacoesQuery query, CancellationToken cancellationToken)
        {
            var index = query.Pagination.PageIndex;
            var tamanho = query.Pagination.PageSize;

            var totalTransacoes = await _transacaoRepository.ObterTotalCadastrados();

            var transacoes = await _transacaoRepository.ListarPaginado(index, tamanho, cancellationToken);

            return new ListarTransacoesResult(new PaginatedResult<TransacaoDto>(
                    index, 
                    tamanho, 
                    totalTransacoes, 
                    transacoes.MapearParaDtoList()
                ));
        }
    }
}
