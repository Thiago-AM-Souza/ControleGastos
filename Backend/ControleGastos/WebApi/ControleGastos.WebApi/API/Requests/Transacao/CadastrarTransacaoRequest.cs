namespace ControleGastos.WebApi.API.Requests.Transacao
{
    public record CadastrarTransacaoRequest(string Descricao,
                                            decimal Valor,
                                            int Tipo,
                                            Guid CategoriaId,
                                            Guid PessoaId);
}
