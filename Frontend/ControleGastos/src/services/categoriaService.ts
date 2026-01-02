import { api } from './api';
import type { Categoria } from "../types/models/categoria/categoria";
import type { PaginatedResult } from "../types/utils/paginatedResult";
import type { CadastrarCategoriaRequest } from '../types/models/categoria/requests/cadastrarCategoriaRequest';
import type { CadastrarCategoriaResponse } from '../types/models/categoria/response/cadastrarCategoriaResponse';
import type { ConsultaTotalPorCategoria } from '../types';

export const categoriaService = {
  listar: async (pageIndex = 0, pageSize = 10): Promise<PaginatedResult<Categoria>> => {
    const response = await api.get('/categoria', {
          params: { pageIndex, pageSize },
        });
    
        return response.data.categorias;
  },

  cadastrar: async (request: CadastrarCategoriaRequest): Promise<CadastrarCategoriaResponse> => {
    const response = await api.post('/categoria', request);

    return response.data as CadastrarCategoriaResponse;
  },

  buscarTodos: async() : Promise<Categoria[]> => {
    const response = await api.get('/categoria/listar-todos');

    return response.data;
  },

  async consultarTotais(): Promise<ConsultaTotalPorCategoria[]> {
      const response = await api.get('/categoria/consultar-totais');
  
      return response.data;
    }
};