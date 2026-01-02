import { useEffect, useState } from 'react';
import { PageHeader, PaginatedTable, Badge, Toast } from '../../components';
import { transacaoService } from '../../services/transacaoService';
import type { Transacao } from '../../types/models/transacao/transacao';
import { formatCurrency, getTipoTransacaoLabel, getTipoTransacaoColor } from '../../utils/formatters';
import { useToast } from '../../hooks';
import type { PaginatedResult } from '../../types/utils/paginatedResult';

function TransacoesList() {
  const [transacoes, setTransacoes] = useState<PaginatedResult<Transacao>>({
    pageIndex: 0,
    pageSize: 10,
    totalCount: 0,
    data: [],
  });
  const [loading, setLoading] = useState(true);
  const { toasts, removeToast, error } = useToast();

  const carregarTransacoes = async (pageIndex = 0) => {
    setLoading(true);
    try {
      const result = await transacaoService.listar(pageIndex, 10);
      setTransacoes(result);
    } catch (err) {
      error('Erro ao carregar transações');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarTransacoes();
  }, []);

  const columns = [
    { key: 'descricao', header: 'Descrição' },
    {
      key: 'pessoa',
      header: 'Pessoa',
      render: (transacao: Transacao) => transacao.pessoaNome || '-',
    },
    {
      key: 'categoria',
      header: 'Categoria',
      render: (transacao: Transacao) => transacao.categoriaDescricao || '-',
    },
    {
      key: 'tipo',
      header: 'Tipo',
      render: (transacao: Transacao) => (
        <Badge
          label={getTipoTransacaoLabel(transacao.tipo)}
          color={getTipoTransacaoColor(transacao.tipo) as 'success' | 'danger'}
        />
      ),
    },
    {
      key: 'valor',
      header: 'Valor',
      render: (transacao: Transacao) => (
        <span className={transacao.tipo === 1 ? 'text-success' : 'text-danger'}>
          {formatCurrency(transacao.valor)}
        </span>
      ),
    },
  ];

  return (
    <div className="container-fluid">
      <PageHeader
        title="Transações"
        subtitle="Gerencie as transações financeiras"
        actionLabel="Nova Transação"
        actionTo="/transacoes/novo"
      />

      <PaginatedTable
        data={transacoes.data}
        columns={columns}
        pageIndex={transacoes.pageIndex}
        pageSize={transacoes.pageSize}
        totalCount={transacoes.totalCount}
        onPageChange={carregarTransacoes}
        loading={loading}
        emptyMessage="Nenhuma transação cadastrada"
      />

      <div className="toast-container">
        {toasts.map((toast) => (
          <Toast key={toast.id} {...toast} onClose={removeToast} />
        ))}
      </div>
    </div>
  );
}

export default TransacoesList;
