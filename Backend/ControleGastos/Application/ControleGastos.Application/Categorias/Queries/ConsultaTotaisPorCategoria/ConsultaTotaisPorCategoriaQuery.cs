namespace ControleGastos.Application.Categorias.Queries.ConsultaTotaisPorCategoria
{
    public record ConsultaTotaisPorCategoriaQuery()
        : IQuery<IReadOnlyList<ConsultaTotaisPorCategoriaResult>>;

    public record ConsultaTotaisPorCategoriaResult(string Descricao,
                                                   decimal Receita,
                                                   decimal Despesa,
                                                   decimal SaldoLiquido);
}