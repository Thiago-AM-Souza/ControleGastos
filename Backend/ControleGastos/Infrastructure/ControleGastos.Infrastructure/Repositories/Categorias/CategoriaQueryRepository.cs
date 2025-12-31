using ControleGastos.Application.Categorias.Queries;
using ControleGastos.Application.Dtos.Categorias;
using Dapper;
using System.Data;

namespace ControleGastos.Infrastructure.Repositories.Categorias
{
    internal class CategoriaQueryRepository(IDbConnection connection) 
		: ICategoriaQueryRepository
    {
		private readonly IDbConnection _connection = connection;

        public async Task<IReadOnlyList<ConsultaTotalPorCategoriaDto>> ConsultaTotalPorCategoriaQuery(CancellationToken cancellationToken)
        {
			const string sql = @"
                with total_por_categoria as (
					select descricao,
	   						receita,
	   						despesa,
	   						receita - despesa as saldo_liquido
					from (
						select c.descricao, 
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
						from categoria.categorias c
						left join transacao.transacoes t
						on t.categoria_id = c.id
						group by c.descricao
					)
				)

				select descricao, 
					   receita, 
					   despesa, 
					   saldo_liquido as SaldoLiquido
				from total_por_categoria

				union all

				select 'Total' as descricao,
						sum(receita) as receita,
						sum(despesa) as despesa,
						sum(saldo_liquido) as SaldoLiquido
				from total_por_categoria;
            ";

            var result = await _connection.QueryAsync<ConsultaTotalPorCategoriaDto>(sql);
            return result.AsList();
        }
    }
}
