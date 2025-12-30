using ControleGastos.Application.Dtos.Transacoes;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.WebApi.API.Responses.Transacao
{
    public record ListarTransacoesResponse(PaginatedResult<TransacaoDto> Transacoes);
}
