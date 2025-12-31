
using Mapster;

namespace ControleGastos.Application.Categorias.Queries.ConsultaTotaisPorCategoria
{
    internal class ConsultaTotaisPorCategoriaQueryHandler(ICategoriaQueryRepository repository)
        : IQueryHandler<ConsultaTotaisPorCategoriaQuery, IReadOnlyList<ConsultaTotaisPorCategoriaResult>>
    {
        private readonly ICategoriaQueryRepository _repository = repository;

        public async Task<IReadOnlyList<ConsultaTotaisPorCategoriaResult>> Handle(ConsultaTotaisPorCategoriaQuery request, CancellationToken cancellationToken)
        {
            var consulta = await _repository.ConsultaTotalPorCategoriaQuery(cancellationToken);

            return consulta.Select(x => x.Adapt<ConsultaTotaisPorCategoriaResult>()).ToList();
        }
    }
}
