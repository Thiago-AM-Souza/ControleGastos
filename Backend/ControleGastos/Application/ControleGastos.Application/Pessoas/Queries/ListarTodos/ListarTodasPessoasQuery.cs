using ControleGastos.Application.Dtos.Pessoas;

namespace ControleGastos.Application.Pessoas.Queries.ListarTodos
{
    public record ListarTodasPessoasQuery
        : IQuery<ListarTodasPessoasResult>;

    public record ListarTodasPessoasResult(List<PessoaDto> Pessoas);
}
