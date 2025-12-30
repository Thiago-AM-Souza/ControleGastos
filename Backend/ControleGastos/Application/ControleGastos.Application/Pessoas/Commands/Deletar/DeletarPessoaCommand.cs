namespace ControleGastos.Application.Pessoas.Commands.Deletar
{
    public record DeletarPessoaCommand(Guid Id) : ICommand<DeletarPessoaResult>;

    public record DeletarPessoaResult(bool Sucesso);
}
