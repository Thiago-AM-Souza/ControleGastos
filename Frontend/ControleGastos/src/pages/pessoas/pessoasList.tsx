import { useEffect, useState } from 'react';
import { pessoaService } from '../../services/pessoaService';
import type { Pessoa } from '../../types/models/pessoa/pessoa';
import type { PaginatedResult  } from '../../types/utils/paginatedResult';
import { Link } from 'react-router-dom';
import { Plus } from 'lucide-react';

function PessoasList() {
  const [pessoas, setPessoas] = useState<PaginatedResult<Pessoa>>({
    pageIndex: 0,
    pageSize: 10,
    totalCount: 0,
    data: [],
  });

  const [loading, setLoading] = useState(true);

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

  const temProximaPagina = pessoas.data.length === pessoas.pageSize;


  return (
    <div className="container-fluid p-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h1 className="h4 mb-1">Pessoas</h1>
          <p className="text-muted mb-0">
            Gerencie as pessoas cadastradas
          </p>
        </div>

        <Link to="/pessoas/novo" className="btn btn-primary">
          <Plus size={18} className="me-2" />
          Nova Pessoa
        </Link>
      </div>

      <div className="card">
        <div className="card-body p-0">
          {loading ? (
            <div className="text-center p-4">
              <div className="spinner-border" />
            </div>
          ) : pessoas.data.length === 0 ? (
            <div className="text-center p-4 text-muted">
              Nenhuma pessoa cadastrada
            </div>
          ) : (
            <div className="table-responsive">
              <table className="table table-hover mb-0">
                <thead className="table-light">
                  <tr>
                    <th>Nome</th>
                    <th>Idade</th>
                  </tr>
                </thead>
                <tbody>
                  {pessoas.data.map(pessoa => (
                    <tr key={pessoa.id}>
                      <td>{pessoa.nome}</td>
                      <td>{pessoa.idade} anos</td>                      
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>

        <div className="card-footer d-flex justify-content-between align-items-center">
          <small className="text-muted">
            Página {pessoas.pageIndex + 1}
          </small>

          <div className="btn-group">
            <button
              className="btn btn-outline-secondary btn-sm"
              disabled={pessoas.pageIndex === 0}
              onClick={() => carregarPessoas(pessoas.pageIndex - 1)}
            >
              Anterior
            </button>

            <button
              className="btn btn-outline-secondary btn-sm"
              disabled={!temProximaPagina}
              onClick={() => carregarPessoas(pessoas.pageIndex + 1)}
            >
              Próxima
            </button>
          </div>
        </div>
      </div>

    </div>
  );
}

export default PessoasList;
