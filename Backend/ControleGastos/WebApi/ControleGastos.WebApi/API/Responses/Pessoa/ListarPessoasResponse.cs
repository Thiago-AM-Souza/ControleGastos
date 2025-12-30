using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.WebApi.API.Responses.Pessoa
{
    public record ListarPessoasResponse(PaginatedResult<PessoaDto> Pessoas);
}
