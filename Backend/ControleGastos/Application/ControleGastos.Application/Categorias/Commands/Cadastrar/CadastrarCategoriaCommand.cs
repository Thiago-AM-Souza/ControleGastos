namespace ControleGastos.Application.Categorias.Commands.Cadastrar
{
    public record CadastrarCategoriaCommand(string Descricao,
                                            int Finalidade)
        : ICommand<CadastrarCategoriaResult>;

    public record CadastrarCategoriaResult(Guid Id);
}
