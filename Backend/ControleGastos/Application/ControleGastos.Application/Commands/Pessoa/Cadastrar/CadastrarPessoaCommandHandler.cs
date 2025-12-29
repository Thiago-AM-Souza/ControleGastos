using ControleGastos.BuildingBlocks.CQRS;

namespace ControleGastos.Application.Commands.Pessoa.Cadastrar
{
    internal class CadastrarPessoaCommandHandler : ICommandHandler<CadastrarPessoaCommand, CadastrarPessoaResult>
    {
        public async Task<CadastrarPessoaResult> Handle(CadastrarPessoaCommand request, CancellationToken cancellationToken)
        {
            return new CadastrarPessoaResult(Guid.NewGuid());
        }
    }
}
