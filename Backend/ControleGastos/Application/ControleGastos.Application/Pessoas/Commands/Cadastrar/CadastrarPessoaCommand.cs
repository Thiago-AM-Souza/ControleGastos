using FluentValidation;

namespace ControleGastos.Application.Pessoas.Commands.Cadastrar
{
    public record CadastrarPessoaCommand(string Nome,
                                         DateTime DataNascimento) : ICommand<CadastrarPessoaResult>;

    public record CadastrarPessoaResult(Guid Id);

    public class CadastrarPessoaCommandValidator : AbstractValidator<CadastrarPessoaCommand>
    {
        public CadastrarPessoaCommandValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(x => x.DataNascimento).NotNull().WithMessage("A data de nascimento é obrigatória.");            
        }
    }
}
