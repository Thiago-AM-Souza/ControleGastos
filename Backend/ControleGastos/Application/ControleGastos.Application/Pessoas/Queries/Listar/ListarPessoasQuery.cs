using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.BuildingBlocks.CQRS;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.Application.Pessoas.Queries.Listar
{
    public record ListarPessoasQuery(PaginationRequest Pagination) : 
        IQuery<ListarPessoasResult>;

    public record ListarPessoasResult(PaginatedResult<PessoaDto> Pessoas);
}
