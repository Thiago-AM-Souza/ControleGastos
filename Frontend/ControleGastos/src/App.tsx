import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Layout } from './components';
import { PessoasList } from './pages'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>          
          <Route path="pessoas" element={<PessoasList />} />                    
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App;
