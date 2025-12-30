namespace ControleGastos.Application.Transacoes.Commands.Cadastrar
{
    public record CadastrarTransacaoCommand(string Descricao,
                                            decimal Valor,
                                            int Tipo,
                                            Guid CategoriaId,
                                            Guid PessoaId)
        : ICommand<CadastrarTransacaoResult>;

    public record CadastrarTransacaoResult(Guid Id);
}
