using ControleGastos.Application.Dtos.Categorias;

namespace ControleGastos.Application.Categorias.Queries.ListarTodos
{
    public record ListarTodasCategoriasQuery()
        : IQuery<ListarTodasCategoriasResult>;

    public record ListarTodasCategoriasResult(List<CategoriaDto> Categorias);
}
