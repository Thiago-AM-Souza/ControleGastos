namespace ControleGastos.Application.Dtos.Categorias
{
    public class ConsultaTotalPorCategoriaDto
    {
        public string Descricao { get; set; } = default!;
        public decimal Receita { get; set; }
        public decimal Despesa { get; set; }
        public decimal SaldoLiquido { get; set; }
    }
}
