
using Mapster;

namespace ControleGastos.Application.Pessoas.Queries.ConsultaTotaisPorPessoa
{
    internal class ConsultaTotaisPorPessoaQueryHandler(IPessoaQueryRepository repository)
        : IQueryHandler<ConsultaTotaisPorPessoaQuery, IReadOnlyList<ConsultaTotaisPorPessoaResult>>
    {
        private readonly IPessoaQueryRepository _repository = repository;

        public async Task<IReadOnlyList<ConsultaTotaisPorPessoaResult>> Handle(ConsultaTotaisPorPessoaQuery query, CancellationToken cancellationToken)
        {
            var consulta = await _repository.ConsultaTotalPorPessoaQuery(cancellationToken);

            return consulta.Select(x => x.Adapt<ConsultaTotaisPorPessoaResult>())
                           .ToList();
        }
    }
}
