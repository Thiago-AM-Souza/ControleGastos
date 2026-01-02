
using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Pessoas.Queries.ListarTodos
{
    internal class ListarTodasPessoasQueryHandler(IPessoaRepository pessoaRepository)
        : IQueryHandler<ListarTodasPessoasQuery, ListarTodasPessoasResult>
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

        public async Task<ListarTodasPessoasResult> Handle(ListarTodasPessoasQuery query, CancellationToken cancellationToken)
        {
            var pessoas = await _pessoaRepository.Listar();

            var dto = pessoas.Select(x => 
                                        new PessoaDto(x.Id,
                                                      x.Nome,
                                                      x.DataNascimento))
                             .ToList();

            return new ListarTodasPessoasResult(dto);
        }
    }
}
