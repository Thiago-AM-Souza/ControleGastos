import { ChevronLeft, ChevronRight } from 'lucide-react';

interface Column<T> {
  key: string;
  header: string;
  render?: (item: T) => React.ReactNode;
}

interface PaginatedTableProps<T> {
  data: T[];
  columns: Column<T>[];
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  onPageChange: (page: number) => void;
  loading?: boolean;
  emptyMessage?: string;
}

function PaginatedTable<T extends { id: string }>({
  data,
  columns,
  pageIndex,
  pageSize,
  totalCount,
  onPageChange,
  loading = false,
  emptyMessage = 'Nenhum registro encontrado',
}: PaginatedTableProps<T>) {
  const totalPages = Math.ceil(totalCount / pageSize);
  const startItem = pageIndex * pageSize + 1;
  const endItem = Math.min((pageIndex + 1) * pageSize, totalCount);

  if (loading) {
    return (
      <div className="d-flex align-items-center">
        <div className="spinner-border me-2" role="status" />
        <span>Carregando...</span>
      </div>
    );
  }

  return (
    <div>
      <div className="table-responsive">
        <table className="table table-striped table-hover">
          <thead>
            <tr>
              {columns.map((col) => (
                <th key={col.key}>{col.header}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.length === 0 ? (
              <tr>
                <td colSpan={columns.length} className="text-center py-3">
                  {emptyMessage}
                </td>
              </tr>
            ) : (
              data.map((item) => (
                <tr key={item.id}>
                  {columns.map((col) => (
                    <td key={col.key}>
                      {col.render
                        ? col.render(item)
                        : (item as Record<string, unknown>)[col.key]?.toString()}
                    </td>
                  ))}
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {totalCount > 0 && (
        <div className="d-flex justify-content-between align-items-center mt-2">
          <div className="text-muted">
            Mostrando {startItem} - {endItem} de {totalCount}
          </div>

          <div className="d-flex align-items-center gap-2">
            <button
              className="btn btn-outline-secondary btn-sm"
              onClick={() => onPageChange(pageIndex - 1)}
              disabled={pageIndex === 0}
            >
              <ChevronLeft size={16} />
              Anterior
            </button>

            <span className="small">
              Página {pageIndex + 1} de {totalPages}
            </span>

            <button
              className="btn btn-outline-secondary btn-sm"
              onClick={() => onPageChange(pageIndex + 1)}
              disabled={pageIndex + 1 >= totalPages}
            >
              Próxima
              <ChevronRight size={16} />
            </button>
          </div>
        </div>

      )}
    </div>
  );
}

export default PaginatedTable;
