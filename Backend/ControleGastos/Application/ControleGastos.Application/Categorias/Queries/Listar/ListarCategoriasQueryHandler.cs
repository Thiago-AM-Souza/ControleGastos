
using ControleGastos.Application.Dtos.Categorias;
using ControleGastos.BuildingBlocks.Pagination;
using ControleGastos.Domain.Interfaces;
using Mapster;

namespace ControleGastos.Application.Categorias.Queries.Listar
{
    internal class ListarCategoriasQueryHandler(ICategoriaRepository categoriaRepository)
        : IQueryHandler<ListarCategoriasQuery, ListarCategoriasResult>
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task<ListarCategoriasResult> Handle(ListarCategoriasQuery query, CancellationToken cancellationToken)
        {
            var index = query.Pagination.PageIndex;
            var tamanho = query.Pagination.PageSize;

            var totalCategorias = await _categoriaRepository.ObterTotalCadastrados();

            var categorias = await _categoriaRepository.ListarPaginado(index, tamanho, cancellationToken);

            return new ListarCategoriasResult(
                new PaginatedResult<CategoriaDto>(
                    index,
                    tamanho,
                    totalCategorias,
                    categorias.Adapt<IEnumerable<CategoriaDto>>()
                ));
        }
    }
}
