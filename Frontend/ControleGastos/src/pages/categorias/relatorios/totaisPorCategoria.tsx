import { useEffect, useState } from 'react';
import { TrendingUp, TrendingDown } from 'lucide-react';
import { PageHeader, SummaryCard } from '../../../components';
import { categoriaService } from '../../../services';
import type { ConsultaTotalPorCategoria } from '../../../types';
import { formatCurrency } from '../../../utils/formatters';

function RelatorioTotaisPorCategoria() {
  const [itens, setItens] = useState<ConsultaTotalPorCategoria[]>([]);
  const [totalGeral, setTotalGeral] = useState<ConsultaTotalPorCategoria | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const carregarTotais = async () => {
      try {
        const data = await categoriaService.consultarTotais();

        if (!data || data.length === 0) {
          setItens([]);
          setTotalGeral(null);
          return;
        }

        const total = data[data.length - 1];

        const categorias = data.slice(0, -1);

        setItens(categorias);
        setTotalGeral(total);
      } catch (error) {
        console.error('Erro ao carregar totais:', error);
      } finally {
        setLoading(false);
      }
    };

    carregarTotais();
  }, []);

  if (loading) {
    return (
      <div className="d-flex align-items-center">
        <div className="spinner-border me-2" role="status" />
        <span>Carregando relatório...</span>
      </div>
    );
  }

  const categoriasReceita = itens.filter((t) => t.receita > 0);
  const categoriasDespesa = itens.filter((t) => t.despesa > 0);

  return (
    <div className="container-fluid">
      <PageHeader
        title="Totais por Categoria"
        subtitle="Resumo de receitas e despesas por categoria"
      />

      {/* Cards  */}
      {totalGeral && (
        <div className="row g-3 mb-3">
          <div className="col-md-6">
            <SummaryCard
              title="Total Receitas"
              value={formatCurrency(totalGeral.receita)}
              icon={<TrendingUp size={24} />}
              color="success"
            />
          </div>

          <div className="col-md-6">
            <SummaryCard
              title="Total Despesas"
              value={formatCurrency(totalGeral.despesa)}
              icon={<TrendingDown size={24} />}
              color="danger"
            />
          </div>
        </div>
      )}

      <div className="row">
        {/* RECEITAS */}
        <div className="col-md-6">
          <h5 className="d-flex align-items-center gap-2">
            <TrendingUp size={18} className="text-success" />
            Receitas por Categoria
          </h5>

          <div className="table-responsive mb-3">
            <table className="table table-striped">
              <thead>
                <tr>
                  <th>Categoria</th>
                  <th className="text-end">Valor</th>
                  <th className="text-end">%</th>
                </tr>
              </thead>
              <tbody>
                {categoriasReceita.length === 0 ? (
                  <tr>
                    <td colSpan={3} className="text-center py-3">
                      Nenhuma receita registrada
                    </td>
                  </tr>
                ) : (
                  categoriasReceita
                    .sort((a, b) => b.receita - a.receita)
                    .map((categoria) => {
                      const percentual =
                        totalGeral && totalGeral.receita > 0
                          ? ((categoria.receita / totalGeral.receita) * 100).toFixed(1)
                          : '0.0';

                      return (
                        <tr key={categoria.categoriaId}>
                          <td>{categoria.descricao}</td>
                          <td className="text-end text-success">
                            {formatCurrency(categoria.receita)}
                          </td>
                          <td className="text-end">{percentual}%</td>
                        </tr>
                      );
                    })
                )}
              </tbody>

              {totalGeral && categoriasReceita.length > 0 && (
                <tfoot>
                  <tr>
                    <td className="fw-bold">Total</td>
                    <td className="text-end text-success fw-bold">
                      {formatCurrency(totalGeral.receita)}
                    </td>
                    <td className="text-end fw-bold">100%</td>
                  </tr>
                </tfoot>
              )}
            </table>
          </div>
        </div>

        {/* DESPESAS */}
        <div className="col-md-6">
          <h5 className="d-flex align-items-center gap-2">
            <TrendingDown size={18} className="text-danger" />
            Despesas por Categoria
          </h5>

          <div className="table-responsive mb-3">
            <table className="table table-striped">
              <thead>
                <tr>
                  <th>Categoria</th>
                  <th className="text-end">Valor</th>
                  <th className="text-end">%</th>
                </tr>
              </thead>
              <tbody>
                {categoriasDespesa.length === 0 ? (
                  <tr>
                    <td colSpan={3} className="text-center py-3">
                      Nenhuma despesa registrada
                    </td>
                  </tr>
                ) : (
                  categoriasDespesa
                    .sort((a, b) => b.despesa - a.despesa)
                    .map((categoria) => {
                      const percentual =
                        totalGeral && totalGeral.despesa > 0
                          ? ((categoria.despesa / totalGeral.despesa) * 100).toFixed(1)
                          : '0.0';

                      return (
                        <tr key={categoria.categoriaId}>
                          <td>{categoria.descricao}</td>
                          <td className="text-end text-danger">
                            {formatCurrency(categoria.despesa)}
                          </td>
                          <td className="text-end">{percentual}%</td>
                        </tr>
                      );
                    })
                )}
              </tbody>

              {totalGeral && categoriasDespesa.length > 0 && (
                <tfoot>
                  <tr>
                    <td className="fw-bold">Total</td>
                    <td className="text-end text-danger fw-bold">
                      {formatCurrency(totalGeral.despesa)}
                    </td>
                    <td className="text-end fw-bold">100%</td>
                  </tr>
                </tfoot>
              )}
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RelatorioTotaisPorCategoria;
