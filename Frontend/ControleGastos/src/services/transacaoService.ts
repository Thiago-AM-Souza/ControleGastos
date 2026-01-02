import { api } from './api';
import type { CadastrarTransacaoRequest } from "../types/models/transacao/requests/cadastrarTransacaoRequest";
import type { CadastrarTransacaoResponse } from "../types/models/transacao/response/cadastrarTransacaoResponse";
import type { Transacao } from "../types/models/transacao/transacao";
import type { PaginatedResult } from "../types/utils/paginatedResult";

export const transacaoService = {
  listar: async (pageIndex = 0, pageSize = 10): Promise<PaginatedResult<Transacao>> => {
    const response = await api.get('/transacao', {
        params: { pageIndex, pageSize },
      });
        
    return response.data.transacoes;
  },

  cadastrar: async (request: CadastrarTransacaoRequest): Promise<CadastrarTransacaoResponse> => {
    const response = await api.post('/transacao', request);
    
    return response.data as CadastrarTransacaoResponse;
  },

};