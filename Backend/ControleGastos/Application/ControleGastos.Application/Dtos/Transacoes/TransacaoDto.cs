namespace ControleGastos.Application.Dtos.Transacoes
{
    public record TransacaoDto(Guid Id,
                               string Descricao,
                               decimal Valor,
                               int Tipo,
                               string CategoriaDescricao,
                               string PessoaNome);
}
