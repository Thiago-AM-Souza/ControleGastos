
using ControleGastos.Domain.Categorias;
using ControleGastos.Domain.Categorias.Enums;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Categorias.Commands.Cadastrar
{
    internal class CadastrarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        : ICommandHandler<CadastrarCategoriaCommand, CadastrarCategoriaResult>
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task<CadastrarCategoriaResult> Handle(CadastrarCategoriaCommand command, CancellationToken cancellationToken)
        {
            var finalidadeExiste = Enum.IsDefined(typeof(Finalidade), command.Finalidade);

            if (!finalidadeExiste)
            {
                // throw
            }

            var categoria = new Categoria(command.Descricao,
                                          (Finalidade)command.Finalidade);

            await _categoriaRepository.Cadastrar(categoria);

            return new CadastrarCategoriaResult(categoria.Id);
        }
    }
}
