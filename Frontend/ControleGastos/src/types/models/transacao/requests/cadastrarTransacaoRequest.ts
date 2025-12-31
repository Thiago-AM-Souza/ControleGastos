export interface CadastrarTransacaoRequest {
  descricao: string;
  valor: number;
  tipo: number;
  categoriaId: string;
  pessoaId: string;
}