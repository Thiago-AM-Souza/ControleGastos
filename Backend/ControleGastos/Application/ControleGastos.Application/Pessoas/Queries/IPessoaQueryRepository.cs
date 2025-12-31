using ControleGastos.Application.Dtos.Pessoas;

namespace ControleGastos.Application.Pessoas.Queries
{
    public interface IPessoaQueryRepository
    {
        Task<IReadOnlyList<ConsultaTotalPorPessoaDto>> ConsultaTotalPorPessoaQuery(CancellationToken cancellationToken);
    }
}
