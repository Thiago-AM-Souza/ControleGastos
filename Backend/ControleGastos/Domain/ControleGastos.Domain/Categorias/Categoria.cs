using ControleGastos.Domain.Categorias.Enums;

namespace ControleGastos.Domain.Categorias
{
    public class Categoria : AggregateRoot
    {
        public string Descricao { get; private set; }
        public Finalidade Finalidade { get; private set; }
        public Categoria(string descricao,
                         Finalidade finalidade)
        {
            Descricao = descricao;
            Finalidade = finalidade;
        }
    }
}