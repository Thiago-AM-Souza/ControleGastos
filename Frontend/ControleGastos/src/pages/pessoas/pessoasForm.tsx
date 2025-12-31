import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { PageHeader, Toast } from '../../components';
import { pessoaService } from '../../services/pessoaService';
import { useToast } from '../../hooks';

function PessoasForm() {
  const navigate = useNavigate();
  const { toasts, removeToast, success, error } = useToast();
  const [loading, setLoading] = useState(false);
  const [form, setForm] = useState({
    nome: '',
    dataNascimento: '',
  });
  const [errors, setErrors] = useState<Record<string, string>>({});

  const validate = () => {
    const newErrors: Record<string, string> = {};

    if (!form.nome.trim()) {
      newErrors.nome = 'Nome é obrigatório';
    } else if (form.nome.length < 2) {
      newErrors.nome = 'Nome deve ter pelo menos 2 caracteres';
    } else if (form.nome.length > 100) {
      newErrors.nome = 'Nome deve ter no máximo 100 caracteres';
    }

    if (!form.dataNascimento) {
      newErrors.dataNascimento = 'Data de nascimento é obrigatória';
    } else {
      const data = new Date(form.dataNascimento);
      if (data > new Date()) {
        newErrors.dataNascimento = 'Data de nascimento não pode ser futura';
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
      await pessoaService.cadastrar({
        nome: form.nome,
        dataNascimento: form.dataNascimento,
      });
      success('Pessoa cadastrada com sucesso!');
      setTimeout(() => navigate('/pessoas'), 1500);
    } catch (err) {
      error('Erro ao cadastrar pessoa');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: '' }));
    }
  };

  return (
    <div className="container-fluid">
      <PageHeader
        title="Nova Pessoa"
        subtitle="Cadastre uma nova pessoa no sistema"
        backTo="/pessoas"
      />

      <div className="card">
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="nome" className="form-label">Nome *</label>
              <input
                type="text"
                id="nome"
                name="nome"
                className={`form-control ${errors.nome ? 'is-invalid' : ''}`}
                value={form.nome}
                onChange={handleChange}
                placeholder="Digite o nome completo"
                maxLength={100}
              />
              {errors.nome && <div className="invalid-feedback">{errors.nome}</div>}
            </div>

            <div className="mb-3">
              <label htmlFor="dataNascimento" className="form-label">Data de Nascimento *</label>
              <input
                type="date"
                id="dataNascimento"
                name="dataNascimento"
                className={`form-control ${errors.dataNascimento ? 'is-invalid' : ''}`}
                value={form.dataNascimento}
                onChange={handleChange}
                max={new Date().toISOString().split('T')[0]}
              />
              {errors.dataNascimento && (
                <div className="invalid-feedback">{errors.dataNascimento}</div>
              )}
            </div>

            <div className="d-flex justify-content-end gap-2">
              <button type="button" className="btn btn-secondary" onClick={() => navigate('/pessoas')}>Cancelar</button>
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

export default PessoasForm;
