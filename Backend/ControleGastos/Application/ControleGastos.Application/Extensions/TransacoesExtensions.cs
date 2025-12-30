using ControleGastos.Application.Dtos.Transacoes;
using ControleGastos.Domain.Transacoes;

namespace ControleGastos.Application.Extensions
{
    public static class TransacoesExtensions
    {
        public static IEnumerable<TransacaoDto> MapearParaDtoList(this IEnumerable<Transacao> transacoes)
        {
            return transacoes.Select(t => new TransacaoDto(t.Id,
                                                           t.Descricao,
                                                           t.Valor.Total,
                                                           (int)t.Tipo,
                                                           t.Categoria.Descricao,
                                                           t.Pessoa.Nome));
        }
    }
}
