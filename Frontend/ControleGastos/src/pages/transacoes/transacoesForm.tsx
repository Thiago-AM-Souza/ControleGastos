import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { AlertCircle } from 'lucide-react';
import { PageHeader, Toast } from '../../components';
import { transacaoService, pessoaService, categoriaService } from '../../services';
import type { Pessoa, Categoria } from '../../types';
import { TIPOTRANSACAO, FINALIDADE } from '../../types';
import { useToast } from '../../hooks';
import { getFinalidadeLabel } from '../../utils/formatters';

function TransacoesForm() {
  const navigate = useNavigate();
  const { toasts, removeToast, success, error } = useToast();
  const [loading, setLoading] = useState(false);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [form, setForm] = useState({
    descricao: '',
    valor: '',
    tipo: '',
    pessoaId: '',
    categoriaId: '',
  });
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [tipoWarning, setTipoWarning] = useState('');

  useEffect(() => {
    const carregarDados = async () => {
      try {
        const [pessoasData, categoriasData] = await Promise.all([
          pessoaService.buscarTodos(),
          categoriaService.buscarTodos(),
        ]);
        setPessoas(pessoasData);
        setCategorias(categoriasData);
      } catch (err) {
        error('Erro ao carregar dados');
        console.error(err);
      }
    };

    carregarDados();
  }, []);

  useEffect(() => {
    if (!form.categoriaId || !form.tipo) {
      setTipoWarning('');
      return;
    }

    const categoria = categorias.find((c) => c.id === form.categoriaId);
    if (!categoria) return;

    const tipo = parseInt(form.tipo);

    if (categoria.finalidade === FINALIDADE.Despesa && tipo !== TIPOTRANSACAO.Despesa) {
      setTipoWarning('Esta categoria aceita apenas Despesas');
    } else if (categoria.finalidade === FINALIDADE.Receita && tipo !== TIPOTRANSACAO.Receita) {
      setTipoWarning('Esta categoria aceita apenas Receitas');
    } else {
      setTipoWarning('');
    }
  }, [form.categoriaId, form.tipo, categorias]);

  const validate = () => {
    const newErrors: Record<string, string> = {};

    if (!form.descricao.trim()) {
      newErrors.descricao = 'Descrição é obrigatória';
    }

    if (!form.valor) {
      newErrors.valor = 'Valor é obrigatório';
    } else if (parseFloat(form.valor) <= 0) {
      newErrors.valor = 'Valor deve ser maior que zero';
    }

    if (form.tipo === '') {
      newErrors.tipo = 'Tipo é obrigatório';
    }

    if (!form.pessoaId) {
      newErrors.pessoaId = 'Pessoa é obrigatória';
    }

    if (!form.categoriaId) {
      newErrors.categoriaId = 'Categoria é obrigatória';
    }

    if (form.categoriaId && form.tipo !== '') {
      const categoria = categorias.find((c) => c.id === form.categoriaId);
      const tipo = parseInt(form.tipo);

      if (categoria) {
        if (categoria.finalidade === FINALIDADE.Despesa && tipo !== TIPOTRANSACAO.Despesa) {
          newErrors.tipo = 'Esta categoria aceita apenas Despesas';
        } else if (categoria.finalidade === FINALIDADE.Receita && tipo !== TIPOTRANSACAO.Receita) {
          newErrors.tipo = 'Esta categoria aceita apenas Receitas';
        }
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) return;

    setLoading(true);
    try {
      await transacaoService.cadastrar({
        descricao: form.descricao,
        valor: parseFloat(form.valor),
        tipo: parseInt(form.tipo),
        pessoaId: form.pessoaId,
        categoriaId: form.categoriaId,
      });
      success('Transação cadastrada com sucesso!');
      setTimeout(() => navigate('/transacoes'), 1500);
    } catch (err) {
      error('Erro ao cadastrar transação');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: '' }));
    }
  };

  const getCategoriasDisponiveis = () => {
    if (form.tipo === '') return categorias;
    
    const tipo = parseInt(form.tipo);
    return categorias.filter((c) => {
      if (c.finalidade === FINALIDADE.Ambas) return true;
      return c.finalidade === tipo;
    });
  };

  const categoriaSelecionada = categorias.find((c) => c.id === form.categoriaId);

  return (
    <div>
      <PageHeader
        title="Nova Transação"
        subtitle="Cadastre uma nova transação financeira"
        backTo="/transacoes"
      />

      <div className="card mb-3">
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="descricao" className="form-label">Descrição *</label>
              <input
                type="text"
                id="descricao"
                name="descricao"
                className={`form-control ${errors.descricao ? 'is-invalid' : ''}`}
                value={form.descricao}
                onChange={handleChange}
                placeholder="Ex: Supermercado, Salário, Conta de Luz..."
              />
              {errors.descricao && (
                <div className="invalid-feedback">{errors.descricao}</div>
              )}
            </div>

            <div className="row">
              <div className="col-md-6 mb-3">
                <label htmlFor="valor" className="form-label">Valor *</label>

                <div className="input-group">
                  <span className="input-group-text">R$</span>

                  <input
                    type="text"
                    id="valor"
                    name="valor"
                    className={`form-control ${errors.valor ? 'is-invalid' : ''}`}
                    value={form.valor}
                    onChange={handleChange}
                    placeholder="0,00"
                    inputMode="decimal"
                  />

                  {errors.valor && (
                    <div className="invalid-feedback d-block">
                      {errors.valor}
                    </div>
                  )}
                </div>
              </div>

              <div className="col-md-6 mb-3">
                <label htmlFor="tipo" className="form-label">Tipo *</label>
                <select
                  id="tipo"
                  name="tipo"
                  className={`form-select ${errors.tipo ? 'is-invalid' : ''}`}
                  value={form.tipo}
                  onChange={handleChange}
                >
                  <option value="">Selecione o tipo</option>
                  <option value={TIPOTRANSACAO.Despesa}>Despesa</option>
                  <option value={TIPOTRANSACAO.Receita}>Receita</option>
                </select>
                {errors.tipo && <div className="invalid-feedback">{errors.tipo}</div>}
              </div>
            </div>

            <div className="mb-3">
              <label htmlFor="pessoaId" className="form-label">Pessoa *</label>
              <select
                id="pessoaId"
                name="pessoaId"
                className={`form-select ${errors.pessoaId ? 'is-invalid' : ''}`}
                value={form.pessoaId}
                onChange={handleChange}
              >
                <option value="">Selecione uma pessoa</option>
                {pessoas.map((pessoa) => (
                  <option key={pessoa.id} value={pessoa.id}>
                    {pessoa.nome}
                  </option>
                ))}
              </select>
              {errors.pessoaId && (
                <div className="invalid-feedback">{errors.pessoaId}</div>
              )}
            </div>

            <div className="mb-3">
              <label htmlFor="categoriaId" className="form-label">Categoria *</label>
              <select
                id="categoriaId"
                name="categoriaId"
                className={`form-select ${errors.categoriaId ? 'is-invalid' : ''}`}
                value={form.categoriaId}
                onChange={handleChange}
              >
                <option value="">Selecione uma categoria</option>
                {getCategoriasDisponiveis().map((categoria) => (
                  <option key={categoria.id} value={categoria.id}>
                    {categoria.descricao} ({getFinalidadeLabel(categoria.finalidade)})
                  </option>
                ))}
              </select>
              {errors.categoriaId && (
                <div className="invalid-feedback">{errors.categoriaId}</div>
              )}
              {categoriaSelecionada && (
                <div className="form-text">Finalidade: {getFinalidadeLabel(categoriaSelecionada.finalidade)}</div>
              )}
            </div>

            {tipoWarning && (
              <div className="alert alert-warning d-flex align-items-center" role="alert">
                <AlertCircle size={16} className="me-2" />
                <div>{tipoWarning}</div>
              </div>
            )}

            <div className="d-flex justify-content-end gap-2">
              <button type="button" className="btn btn-secondary" onClick={() => navigate('/transacoes')}>Cancelar</button>
              <button type="submit" className="btn btn-primary" disabled={loading}>{loading ? 'Salvando...' : 'Salvar'}</button>
            </div>
          </form>
        </div>
      </div>

      <div>
        {toasts.map((toast) => (
          <Toast key={toast.id} {...toast} onClose={removeToast} />
        ))}
      </div>
    </div>
  );
}

export default TransacoesForm;
