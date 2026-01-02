export const TIPOTRANSACAO = {
    Despesa: 0,
  Receita: 1,
} as const;

export type TipoTransacao = (typeof TIPOTRANSACAO)[keyof typeof TIPOTRANSACAO];