import { Finalidade } from "../../enums/finalidade";

export interface Categoria {
  id: string;
  descricao: string;
  finalidade: Finalidade;
}