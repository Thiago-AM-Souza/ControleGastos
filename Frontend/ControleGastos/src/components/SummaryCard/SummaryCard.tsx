import { ReactNode } from 'react';

interface SummaryCardProps {
  title: string;
  value: string | number;
  icon?: ReactNode;
  color?: 'primary' | 'success' | 'danger' | 'warning' | 'info';
}

function SummaryCard({ title, value, icon, color = 'primary' }: SummaryCardProps) {
  return (
    <div className={`card mb-2 border-${color}`}>
      <div className="card-body d-flex align-items-center">
        {icon && <div className="me-3">{icon}</div>}
        <div>
          <h6 className="card-title mb-1">{title}</h6>
          <p className={`mb-0 h5 text-${color}`}>{value}</p>
        </div>
      </div>
    </div>
  );
}

export default SummaryCard;
