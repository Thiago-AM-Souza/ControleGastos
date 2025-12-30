using ControleGastos.BuildingBlocks.CQRS;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Pessoas.Commands.Deletar
{
    internal class DeletarPessoaCommandHandler(IPessoaRepository pessoaRepository)
        : ICommandHandler<DeletarPessoaCommand, DeletarPessoaResult>
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        public async Task<DeletarPessoaResult> Handle(DeletarPessoaCommand command, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPorId(command.Id);

            if (pessoa is null)
            {
                throw new Exception();
            }

            await _pessoaRepository.Excluir(pessoa);

            return new DeletarPessoaResult(true);
        }
    }
}
