
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
                throw new FinalidadeInconsistenteException("Finalidade inválida.");
            }

            var categoriaExistente = await _categoriaRepository.ObterPorDescricao(command.Descricao);

            // O usuario não pode cadastrar categorias com mesma descricao e finalidade
            if (categoriaExistente is not null 
                && (int)categoriaExistente.Finalidade == command.Finalidade)
            {
                throw new FinalidadeInconsistenteException("Finalidade já existe no banco de dados.");
            }
            // caso usuario tente cadastar uma categoria ja existente e
            // com finalidade diferente apenas altero a finalidade para ambas
            else if (categoriaExistente is not null 
                    && (int)categoriaExistente.Finalidade != command.Finalidade)
            {
                categoriaExistente.AlterarFinalidade(Finalidade.Ambas);
                await _categoriaRepository.Atualizar(categoriaExistente);
                return new CadastrarCategoriaResult(categoriaExistente.Id);
            }

            var categoria = new Categoria(command.Descricao,
                                          (Finalidade)command.Finalidade);

            await _categoriaRepository.Cadastrar(categoria);

            return new CadastrarCategoriaResult(categoria.Id);
        }
    }
}
