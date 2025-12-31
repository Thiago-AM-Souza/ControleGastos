import { useEffect, useState } from 'react';
import { pessoaService } from '../../services/pessoaService';
import type { Pessoa } from '../../types/models/pessoa/pessoa';
import type { PaginatedResult  } from '../../types/utils/paginatedResult';
// import { Link } from 'react-router-dom';
// import { Plus } from 'lucide-react';
import { useToast } from '../../hooks';
import { Trash2 } from 'lucide-react';
import { PageHeader, PaginatedTable, ConfirmModal, Toast } from '../../components';



function PessoasList() {
  const [pessoas, setPessoas] = useState<PaginatedResult<Pessoa>>({
    pageIndex: 0,
    pageSize: 10,
    totalCount: 0,
    data: [],
    count: 0,
  });

  const [loading, setLoading] = useState(true);
    const [deleteModal, setDeleteModal] = useState<{ isOpen: boolean; pessoa: Pessoa | null }>({
    isOpen: false,
    pessoa: null,
  });
  const { toasts, removeToast, success, error } = useToast();

  const carregarPessoas = async (pageIndex = 0) => {
    setLoading(true);
    try {
      const result = await pessoaService.listar(pageIndex, 10);
      console.log(result);
      setPessoas(result);
    } catch (err) {
      console.log('Erro ao carregar pessoas');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarPessoas();
    }, []);

    const handleDelete = async () => {
    if (!deleteModal.pessoa) return;

    try {
      await pessoaService.deletar(deleteModal.pessoa.id);
      success(`${deleteModal.pessoa.nome} foi excluído com sucesso!`);
      setDeleteModal({ isOpen: false, pessoa: null });
      carregarPessoas(pessoas.pageIndex);
    } catch (err) {
      error('Erro ao excluir pessoa');
      console.error(err);
    }
  };




  const columns = [
    { key: 'nome', header: 'Nome' },
    {
      key: 'idade',
      header: 'Idade'
    },
    {
      key: 'acoes',
      header: 'Ações',
      render: (pessoa: Pessoa) => (
        <button
          className="btn btn-outline-danger btn-sm d-inline-flex align-items-center justify-content-center"
          onClick={() => setDeleteModal({ isOpen: true, pessoa })}
          title="Excluir"
        >
          <Trash2 size={16} />
        </button>
      ),
    },
  ];

  return (
    <div className="container-fluid">
      <PageHeader
        title="Pessoas"
        subtitle="Gerencie as pessoas cadastradas"
        actionLabel="Nova Pessoa"
        actionTo="/pessoas/novo"
      />

      <PaginatedTable
        data={pessoas.data}
        columns={columns}
        pageIndex={pessoas.pageIndex}
        pageSize={pessoas.pageSize}
        totalCount={pessoas.count}
        onPageChange={carregarPessoas}
        loading={loading}
        emptyMessage="Nenhuma pessoa cadastrada"
      />

      <ConfirmModal
        isOpen={deleteModal.isOpen}
        title="Confirmar Exclusão"
        message={`Tem certeza que deseja excluir ${deleteModal.pessoa?.nome}? Esta ação não pode ser desfeita.`}
        confirmText="Excluir"
        cancelText="Cancelar"
        type="danger"
        onConfirm={handleDelete}
        onCancel={() => setDeleteModal({ isOpen: false, pessoa: null })}
      />

      <div className="toast-container">
        {toasts.map((toast) => (
          <Toast key={toast.id} {...toast} onClose={removeToast} />
        ))}
      </div>
    </div>
  );
}

export default PessoasList;
