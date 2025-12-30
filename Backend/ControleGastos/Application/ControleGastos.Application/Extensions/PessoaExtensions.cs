using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.Domain.Pessoas;

namespace ControleGastos.Application.Extensions
{
    public static class PessoaExtensions
    {
        public static IEnumerable<PessoaDto> MapearParaDtoList(this IEnumerable<Pessoa> pessoas)
        {
            return pessoas.Select(p => new PessoaDto(p.Id, 
                                                     p.Nome, 
                                                     p.DataNascimento));
        }
    }
}
