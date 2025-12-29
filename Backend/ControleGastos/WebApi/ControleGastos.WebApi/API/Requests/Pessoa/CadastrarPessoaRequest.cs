namespace ControleGastos.WebApi.API.Requests.Pessoa
{
    public record CadastrarPessoaRequest(string Nome,
                                         DateTime DataNascimento);
}
