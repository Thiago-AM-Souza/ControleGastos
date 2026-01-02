import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { PageHeader, Toast } from '../../components';
import { categoriaService } from '../../services/categoriaService';
import { FINALIDADE } from '../../types';
import { useToast } from '../../hooks';

function CategoriasForm() {
  const navigate = useNavigate();
  const { toasts, removeToast, success, error } = useToast();
  const [loading, setLoading] = useState(false);
  const [form, setForm] = useState({
    descricao: '',
    finalidade: '',
  });
  const [errors, setErrors] = useState<Record<string, string>>({});

  const validate = () => {
    const newErrors: Record<string, string> = {};

    if (!form.descricao.trim()) {
      newErrors.descricao = 'Descrição é obrigatória';
    } else if (form.descricao.length < 3) {
      newErrors.descricao = 'Descrição deve ter pelo menos 3 caracteres';
    }

    if (form.finalidade === '') {
      newErrors.finalidade = 'Finalidade é obrigatória';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) return;

    setLoading(true);
    try {
      await categoriaService.cadastrar({
        descricao: form.descricao,
        finalidade: parseInt(form.finalidade),
      });
      success('Categoria cadastrada com sucesso!');
      setTimeout(() => navigate('/categorias'), 1500);
    } catch (err) {
      error('Erro ao cadastrar categoria');
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

  return (
    <div className="container-fluid">
      <PageHeader
        title="Nova Categoria"
        subtitle="Cadastre uma nova categoria de transação"
        backTo="/categorias"
      />

      <div className="card">
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
                placeholder="Ex: Alimentação, Transporte, Salário..."
              />
              {errors.descricao && (
                <div className="invalid-feedback">{errors.descricao}</div>
              )}
            </div>

            <div className="mb-3">
              <label htmlFor="finalidade" className="form-label">Finalidade *</label>
              <select
                id="finalidade"
                name="finalidade"
                className={`form-select ${errors.finalidade ? 'is-invalid' : ''}`}
                value={form.finalidade}
                onChange={handleChange}
              >
                <option value="">Selecione a finalidade</option>
                <option value={FINALIDADE.Despesa}>Despesa</option>
                <option value={FINALIDADE.Receita}>Receita</option>
                <option value={FINALIDADE.Ambas}>Ambas</option>
              </select>
              {errors.finalidade && (
                <div className="invalid-feedback">{errors.finalidade}</div>
              )}
              <div className="form-text">Define se a categoria é para despesas, receitas ou ambos</div>
            </div>

            <div className="d-flex justify-content-end gap-2">
              <button type="button" className="btn btn-secondary" onClick={() => navigate('/categorias')}>Cancelar</button>
              <button type="submit" className="btn btn-primary" disabled={loading}>{loading ? 'Salvando...' : 'Salvar'}</button>
            </div>
          </form>
        </div>
      </div>

      <div className="toast-container">
        {toasts.map((toast) => (
          <Toast key={toast.id} {...toast} onClose={removeToast} />
        ))}
      </div>
    </div>
  );
}

export default CategoriasForm;
