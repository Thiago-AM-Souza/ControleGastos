import { api } from './api';
import type { Pessoa } from '../types/models/pessoa/pessoa';
import type { PaginatedResult } from '../types/utils/paginatedResult';

export const pessoaService = {
  async listar(
    pageIndex = 0,
    pageSize = 10
  ): Promise<PaginatedResult<Pessoa>> {
    const response = await api.get('/pessoa', {
      params: { pageIndex, pageSize },
    });

    return response.data.pessoas;
  },
};