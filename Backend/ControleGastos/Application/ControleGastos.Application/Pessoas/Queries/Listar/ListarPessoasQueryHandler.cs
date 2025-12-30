using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.Application.Extensions;
using ControleGastos.BuildingBlocks.CQRS;
using ControleGastos.BuildingBlocks.Pagination;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Pessoas.Queries.Listar
{
    internal class ListarPessoasQueryHandler(IPessoaRepository pessoaRepository)
        : IQueryHandler<ListarPessoasQuery, ListarPessoasResult>
    {
        private readonly IPessoaRepository _pessoasRepository = pessoaRepository;

        public async Task<ListarPessoasResult> Handle(ListarPessoasQuery query, CancellationToken cancellationToken)
        {
            var index = query.Pagination.PageIndex;
            var tamanho = query.Pagination.PageSize;

            var totalPessoas = await _pessoasRepository.ObterTotalCadastrados();

            var pessoas = await _pessoasRepository.ListarPaginado(index, tamanho, cancellationToken);

            return new ListarPessoasResult(
                new PaginatedResult<PessoaDto>(
                    index,
                    tamanho,
                    totalPessoas,
                    pessoas.MapearParaDtoList()
                ));
        }
    }
}
