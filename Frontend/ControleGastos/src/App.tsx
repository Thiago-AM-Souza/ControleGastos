import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Layout } from './components';
import { 
  PessoasList,
  PessoasForm,
  CategoriasList,
  CategoriasForm,
  TransacoesList,
  TransacoesForm,
  RelatorioTotaisPorPessoa
} from './pages'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>          
          <Route path="pessoas" element={<PessoasList />} />
          <Route path="pessoas/novo" element={<PessoasForm />} />

          <Route path="categorias" element={<CategoriasList />} />
          <Route path="categorias/novo" element={<CategoriasForm />} />

          <Route path="transacoes" element={<TransacoesList />} />
          <Route path="transacoes/novo" element={<TransacoesForm />} />

          <Route path="relatorios/pessoas" element={<RelatorioTotaisPorPessoa />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App;
