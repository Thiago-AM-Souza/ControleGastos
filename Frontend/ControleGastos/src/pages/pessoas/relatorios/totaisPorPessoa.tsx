import { useEffect, useState } from 'react';
import { TrendingUp, TrendingDown, Wallet } from 'lucide-react';
import { PageHeader, SummaryCard } from '../../../components';
import { pessoaService } from '../../../services';
import type { ConsultaTotalPorPessoa } from '../../../types';
import { formatCurrency, formatCurrencyWithSign } from '../../../utils/formatters';

function RelatorioTotaisPorPessoa() {
  const [totais, setTotais] = useState<ConsultaTotalPorPessoa[]>([]);
  const [totalGeral, setTotalGeral] = useState<ConsultaTotalPorPessoa | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const carregarTotais = async () => {
      try {
        const data = await pessoaService.consultarTotais();

        if (!data || data.length === 0) {
          setTotais([]);
          setTotalGeral(null);
          return;
        }

        const total = data[data.length - 1];

        const itens = data.slice(0, -1);

        setTotais(itens);
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

  return (
    <div className="container-fluid">
      <PageHeader
        title="Totais por Pessoa"
        subtitle="Resumo financeiro de cada pessoa cadastrada"
      />

      {/* Cards resumo */}
      {totalGeral && (
        <div className="row g-3 mb-3">
          <div className="col-md-4">
            <SummaryCard
              title="Total Receitas"
              value={formatCurrency(totalGeral.receita)}
              icon={<TrendingUp size={24} />}
              color="success"
            />
          </div>

          <div className="col-md-4">
            <SummaryCard
              title="Total Despesas"
              value={formatCurrency(totalGeral.despesa)}
              icon={<TrendingDown size={24} />}
              color="danger"
            />
          </div>

          <div className="col-md-4">
            <SummaryCard
              title="Saldo Líquido"
              value={formatCurrencyWithSign(totalGeral.saldoLiquido)}
              icon={<Wallet size={24} />}
              color={totalGeral.saldoLiquido >= 0 ? 'success' : 'danger'}
            />
          </div>
        </div>
      )}

      {/* Tabela */}
      <div className="table-responsive">
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Nome</th>
              <th className="text-end">Receitas</th>
              <th className="text-end">Despesas</th>
              <th className="text-end">Saldo Líquido</th>
            </tr>
          </thead>

          <tbody>
            {totais.length === 0 ? (
              <tr>
                <td colSpan={4} className="text-center py-3">
                  Nenhum dado encontrado
                </td>
              </tr>
            ) : (
              totais.map((pessoa, index) => (
                <tr key={index}>
                  <td>{pessoa.nome}</td>
                  <td className="text-end text-success">
                    {formatCurrency(pessoa.receita)}
                  </td>
                  <td className="text-end text-danger">
                    {formatCurrency(pessoa.despesa)}
                  </td>
                  <td
                    className={`text-end fw-bold ${
                      pessoa.saldoLiquido >= 0 ? 'text-success' : 'text-danger'
                    }`}
                  >
                    {formatCurrencyWithSign(pessoa.saldoLiquido)}
                  </td>
                </tr>
              ))
            )}
          </tbody>

          {/* Total geral vindo do banco */}
          {totalGeral && (
            <tfoot>
              <tr>
                <td className="fw-bold">Total Geral</td>
                <td className="text-end text-success fw-bold">
                  {formatCurrency(totalGeral.receita)}
                </td>
                <td className="text-end text-danger fw-bold">
                  {formatCurrency(totalGeral.despesa)}
                </td>
                <td
                  className={`text-end fw-bold ${
                    totalGeral.saldoLiquido >= 0 ? 'text-success' : 'text-danger'
                  }`}
                >
                  {formatCurrencyWithSign(totalGeral.saldoLiquido)}
                </td>
              </tr>
            </tfoot>
          )}
        </table>
      </div>
    </div>
  );
}

export default RelatorioTotaisPorPessoa;
