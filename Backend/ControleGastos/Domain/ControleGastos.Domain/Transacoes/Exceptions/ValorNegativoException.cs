using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Transacoes.Exceptions
{
    public class ValorNegativoException : DomainException
    {
        public ValorNegativoException() 
            : base("O valor da transação não pode ser negativo ou igual a zero.")
        {
        }
    }
}
