using ControleGastos.Domain.Categorias;
using ControleGastos.Domain.Categorias.Enums;
using ControleGastos.Domain.Pessoas;

namespace ControleGastos.Domain.Transacoes
{
    public class Transacao : AggregateRoot
    {
        public string Descricao { get; private set; } = default!;
        public Valor Valor { get; private set; } = default!;
        public Finalidade Tipo { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid PessoaId { get; private set; }

        // Relacionamento EF
        public Pessoa Pessoa { get; private set; } = default!;
        public Categoria Categoria { get; private set; } = default!;

        // Construtor para EF
        protected Transacao() { }

        public Transacao(string descricao, 
                         decimal valor, 
                         Finalidade tipo,
                         Pessoa pessoa, 
                         Categoria categoria)
        {
            Descricao = descricao;
            Valor = new Valor(valor);
            Tipo = tipo;
            Pessoa = pessoa;
            Categoria = categoria;
            CategoriaId = categoria.Id;
            PessoaId = pessoa.Id;
        }
    }
}