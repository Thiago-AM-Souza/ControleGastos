using ControleGastos.Application.Dtos.Categorias;

namespace ControleGastos.Application.Categorias.Queries
{
    public interface ICategoriaQueryRepository
    {
        Task<IReadOnlyList<ConsultaTotalPorCategoriaDto>> ConsultaTotalPorCategoriaQuery(CancellationToken cancellationToken);
    }
}
