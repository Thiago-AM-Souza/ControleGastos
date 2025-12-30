namespace ControleGastos.BuildingBlocks.Extensions
{
    public static class DatetimeExtensions
    {
        public static int ObterIdade(this DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento.Date > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }
    }
}
