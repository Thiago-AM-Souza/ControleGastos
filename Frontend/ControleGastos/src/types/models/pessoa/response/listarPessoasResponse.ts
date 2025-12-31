import type { Pessoa } from '../pessoa';
import type { PaginatedResult } from '../../../utils/paginatedResult';

export interface ListarPessoasResponse {
  pessoas: PaginatedResult<Pessoa>;
}