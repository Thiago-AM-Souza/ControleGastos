// Labels para finalidade
export const getFinalidadeLabel = (finalidade: number): string => {
  const labels: Record<number, string> = {
    0: 'Despesa',
    1: 'Receita',
    2: 'Ambas',
  };
  return labels[finalidade] || 'Desconhecido';
};

// Cores para finalidade
export const getFinalidadeColor = (finalidade: number): string => {
  const colors: Record<number, string> = {
    0: 'danger',
    1: 'success',
    2: 'info',
  };
  return colors[finalidade] || 'default';
};

// Formatação de moeda
export const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

// Labels para tipo de transação
export const getTipoTransacaoLabel = (tipo: number): string => {
  const labels: Record<number, string> = {
    0: 'Despesa',
    1: 'Receita',
  };
  return labels[tipo] || 'Desconhecido';
};

// Cores para tipo de transação
export const getTipoTransacaoColor = (tipo: number): string => {
  return tipo === 1 ? 'success' : 'danger';
};


// Formatação de moeda com sinal
export const formatCurrencyWithSign = (value: number): string => {
  const sign = value >= 0 ? '+' : '';
  return sign + formatCurrency(value);
};




