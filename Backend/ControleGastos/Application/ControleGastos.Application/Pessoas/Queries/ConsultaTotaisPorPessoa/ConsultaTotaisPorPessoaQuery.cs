namespace ControleGastos.Application.Pessoas.Queries.ConsultaTotaisPorPessoa
{
    public record ConsultaTotaisPorPessoaQuery()
        : IQuery<IReadOnlyList<ConsultaTotaisPorPessoaResult>>;
    
    public record ConsultaTotaisPorPessoaResult(string Nome,
                                                decimal Receita,
                                                decimal Despesa,
                                                decimal SaldoLiquido);
    
}
