using ControleGastos.Application.Dtos.Transacoes;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.Application.Transacoes.Queries.Listar
{
    public record ListarTransacoesQuery(PaginationRequest Pagination)
        : IQuery<ListarTransacoesResult>;

    public record ListarTransacoesResult(PaginatedResult<TransacaoDto> Transacoes);
}
