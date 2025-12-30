using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Pessoas;

namespace ControleGastos.Application.Pessoas.Commands.Cadastrar
{
    internal class CadastrarPessoaCommandHandler(IPessoaRepository pessoaRepository) 
        : ICommandHandler<CadastrarPessoaCommand, CadastrarPessoaResult>
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

        public async Task<CadastrarPessoaResult> Handle(CadastrarPessoaCommand command, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(command.Nome,
                                    command.DataNascimento);

            await _pessoaRepository.Cadastrar(pessoa);

            return new CadastrarPessoaResult(pessoa.Id);
        }
    }
}
