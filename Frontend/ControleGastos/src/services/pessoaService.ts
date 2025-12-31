import { api } from './api';
import type { Pessoa } from '../types/models/pessoa/pessoa';
import type { PaginatedResult } from '../types/utils/paginatedResult';
import type { CadastrarPessoaRequest } from '../types/models/pessoa/requests/cadastrarPessoaRequest';
import type { CadastrarPessoaResponse } from '../types/models/pessoa/response/cadastrarPessoaResponse';

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

  async deletar(id: string): Promise<boolean> {
    const response = await api.delete('/pessoa', {
      params: { id },
    });

    return response.data as boolean;
  },

  async cadastrar(request: CadastrarPessoaRequest): Promise<CadastrarPessoaResponse> {
    const response = await api.post('/pessoa', request);

    return response.data as CadastrarPessoaResponse;
  }
};