import { useEffect, useState } from 'react';
import { PageHeader, PaginatedTable, Badge, Toast } from '../../components';
import { categoriaService } from '../../services/categoriaService';
import type { Categoria } from '../../types/models/categoria/categoria';
import type { PaginatedResult } from '../../types/utils/paginatedResult';
import { getFinalidadeLabel, getFinalidadeColor } from '../../utils/formatters';
import { useToast } from '../../hooks';

function CategoriasList() {
  const [categorias, setCategorias] = useState<PaginatedResult<Categoria>>({
    pageIndex: 0,
    pageSize: 10,
    totalCount: 0,
    data: [],
  });
  const [loading, setLoading] = useState(true);

  const { toasts, removeToast, error } = useToast();

  const carregarCategorias = async (pageIndex = 0) => {
    setLoading(true);
    try {
      const result = await categoriaService.listar(pageIndex, 10);
      setCategorias(result);
    } catch (err) {
      error('Erro ao carregar categorias');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarCategorias();
  }, []);

  const columns = [
    { key: 'descricao', header: 'Descrição' },
    {
      key: 'finalidade',
      header: 'Finalidade',
      render: (categoria: Categoria) => (
        <Badge
          label={getFinalidadeLabel(categoria.finalidade)}
          color={getFinalidadeColor(categoria.finalidade) as 'success' | 'danger' | 'info'}
        />
      ),
    },
  ];

  return (
    <div className="container-fluid">
      <PageHeader
        title="Categorias"
        subtitle="Gerencie as categorias de transações"
        actionLabel="Nova Categoria"
        actionTo="/categorias/novo"
      />

      <PaginatedTable
        data={categorias.data}
        columns={columns}
        pageIndex={categorias.pageIndex}
        pageSize={categorias.pageSize}
        totalCount={categorias.totalCount}
        onPageChange={carregarCategorias}
        loading={loading}
        emptyMessage="Nenhuma categoria cadastrada"
      />

      <div className="toast-container">
        {toasts.map((toast) => (
          <Toast key={toast.id} {...toast} onClose={removeToast} />
        ))}
      </div>
    </div>
  );
}

export default CategoriasList;
