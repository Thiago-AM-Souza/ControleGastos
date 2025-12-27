using ControleGastos.Domain.Categorias.Enums;

namespace ControleGastos.Domain.Categorias
{
    public class Categoria : AggregateRoot
    {
        public string Descricao { get; private set; } = default!;
        public Finalidade Finalidade { get; private set; }

        // Construtor para EF
        protected Categoria() { }

        public Categoria(string descricao,
                         Finalidade finalidade)
        {
            Descricao = descricao;
            Finalidade = finalidade;
        }
    }
}