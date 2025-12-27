namespace ControleGastos.Domain.Transacoes.Exceptions
{
    public class CategoriaNaoEquivalente : DomainException
    {
        public CategoriaNaoEquivalente(string message) 
            : base("Categoria selecionada difere do tipo de finalidade.")
        {
        }
    }
}
