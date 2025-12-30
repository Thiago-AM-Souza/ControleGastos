using ControleGastos.Application.Dtos.Categorias;
using ControleGastos.BuildingBlocks.Pagination;

namespace ControleGastos.WebApi.API.Responses.Categoria
{
    public record ListarCategoriasResponse(PaginatedResult<CategoriaDto> Categorias);
}
