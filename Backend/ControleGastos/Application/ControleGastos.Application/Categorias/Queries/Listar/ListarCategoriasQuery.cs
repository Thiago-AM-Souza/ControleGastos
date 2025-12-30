using ControleGastos.Application.Dtos.Categorias;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.Application.Categorias.Queries.Listar
{
    public record ListarCategoriasQuery(PaginationRequest Pagination)
        : IQuery<ListarCategoriasResult>;

    public record ListarCategoriasResult(PaginatedResult<CategoriaDto> Categorias);
}
