interface BadgeProps {
  label: string;
  color?: 'success' | 'danger' | 'warning' | 'info' | 'default';
}

function Badge({ label, color = 'default' }: BadgeProps) {
  const colorClass =
    color === 'success'
      ? 'bg-success'
      : color === 'danger'
      ? 'bg-danger'
      : color === 'warning'
      ? 'bg-warning text-dark'
      : color === 'info'
      ? 'bg-info text-dark'
      : 'bg-secondary';

  return <span className={`badge ${colorClass} rounded-pill`}>{label}</span>;
}

export default Badge;
