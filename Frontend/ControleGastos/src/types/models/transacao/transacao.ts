import type { TipoTransacao } from "../../enums/tipoTransacao";

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  categoriaDescricao: string;
  pessoaNome: string;
}