using ControleGastos.Application.Dtos.Pessoas;
using ControleGastos.Application.Pessoas.Queries;
using Dapper;
using System.Data;

namespace ControleGastos.Infrastructure.Repositories.Pessoas
{
    public sealed class PessoaQueryRepository(IDbConnection connection) : IPessoaQueryRepository
    {
        private readonly IDbConnection _connection = connection;

        public async Task<IReadOnlyList<ConsultaTotalPorPessoaDto>> ConsultaTotalPorPessoaQuery(CancellationToken cancellationToken)
        {
			const string sql = @"
                with total_por_pessoa as (
					select nome,
					   receita,
					   despesa,
					   receita - despesa as saldo_liquido
					from (
						select p.nome, 
							sum(
								case
									when t.tipo = 0 then t.valor
									else 0
								end
							) as despesa,
							sum(
								case
									when t.tipo = 1 then t.valor
									else 0
								end
							) as receita
						from pessoa.pessoas p
						left join transacao.transacoes t
						on t.pessoa_id = p.id
						group by p.nome
					)
				)

				select nome, 
					   receita, 
					   despesa, 
					   saldo_liquido as SaldoLiquido
				from total_por_pessoa

				union all

				select 'Total' as nome,
						sum(receita) as receita,
						sum(despesa) as despesa,
						sum(saldo_liquido) as SaldoLiquido
				from total_por_pessoa;
            ";

			var result = await _connection.QueryAsync<ConsultaTotalPorPessoaDto>(sql);
			return result.AsList();
        }
    }
}
