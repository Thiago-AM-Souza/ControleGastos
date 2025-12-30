using ControleGastos.Domain.Transacoes;

namespace ControleGastos.Domain.Pessoas
{
    public class Pessoa : AggregateRoot
    {
        public string Nome { get; private set; } = default!;
        public DateTime DataNascimento { get; private set; }

        private List<Transacao> _transacoes = new();
        public IReadOnlyList<Transacao> Transacoes => _transacoes;

        // Construtor para EF
        protected Pessoa() { }

        public Pessoa(string nome,
                      DateTime dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento.ToUniversalTime();
        }
    }
}
