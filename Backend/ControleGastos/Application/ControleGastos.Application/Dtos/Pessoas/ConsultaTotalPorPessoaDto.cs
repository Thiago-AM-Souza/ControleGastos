namespace ControleGastos.Application.Dtos.Pessoas
{
    public class ConsultaTotalPorPessoaDto
    {
        public string Nome { get; set; } = default!;
        public decimal Receita { get; set; }
        public decimal Despesa { get; set; }
        public decimal SaldoLiquido { get; set; }
    }
}
