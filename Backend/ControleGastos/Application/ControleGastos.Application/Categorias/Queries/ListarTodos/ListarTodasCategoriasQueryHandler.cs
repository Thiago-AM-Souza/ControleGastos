
using ControleGastos.Application.Dtos.Categorias;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Categorias.Queries.ListarTodos
{
    internal class ListarTodasCategoriasQueryHandler(ICategoriaRepository categoriaRepository)
        : IQueryHandler<ListarTodasCategoriasQuery, ListarTodasCategoriasResult>
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task<ListarTodasCategoriasResult> Handle(ListarTodasCategoriasQuery request, CancellationToken cancellationToken)
        {
            var categorias = await _categoriaRepository.Listar();

            var dto = categorias.Select(x => 
                                    new CategoriaDto(x.Id,
                                                     x.Descricao,
                                                     (int)x.Finalidade))
                                .ToList();

            return new ListarTodasCategoriasResult(dto);
        }
    }
}
