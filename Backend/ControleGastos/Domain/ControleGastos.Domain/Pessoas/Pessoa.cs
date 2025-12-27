namespace ControleGastos.Domain.Pessoas
{
    public class Pessoa : AggregateRoot
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Pessoa(string nome,
                      DateTime dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
        }
    }
}
