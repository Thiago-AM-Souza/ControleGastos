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
