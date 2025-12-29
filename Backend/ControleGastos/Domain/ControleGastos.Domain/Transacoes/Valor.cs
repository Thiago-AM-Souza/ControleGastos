using ControleGastos.Domain.Transacoes.Exceptions;

namespace ControleGastos.Domain.Transacoes
{
    public record Valor
    {
        private const decimal _valorBase = 0;
        public decimal Total { get; init; }

        // Construtor para EF
        protected Valor() { }

        public Valor(decimal valor)
        {
            if (valor <= _valorBase)
            {
                throw new ValorNegativoException();
            }

            Total = valor;
        }
    }
}
