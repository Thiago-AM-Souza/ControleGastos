export const FINALIDADE = {
  Despesa: 0,
  Receita: 1,
  Ambas: 2,
} as const;

export type Finalidade = (typeof FINALIDADE)[keyof typeof FINALIDADE];