import type { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import { ArrowLeft, Plus } from 'lucide-react';

interface PageHeaderProps {
  title: string;
  subtitle?: string;
  backTo?: string;
  actionLabel?: string;
  actionTo?: string;
  onAction?: () => void;
  children?: ReactNode;
}

function PageHeader({
  title,
  subtitle,
  backTo,
  actionLabel,
  actionTo,
  onAction,
  children,
}: PageHeaderProps) {
  const navigate = useNavigate();

  const handleAction = () => {
    if (onAction) {
      onAction();
    } else if (actionTo) {
      navigate(actionTo);
    }
  };

  return (
    <div className="d-flex justify-content-between align-items-center mb-3">
      <div className="d-flex align-items-center gap-3">
        {backTo && (
          <button className="btn btn-link p-0" onClick={() => navigate(backTo)}>
            <ArrowLeft size={20} />
          </button>
        )}
        <div>
          <h1 className="h5 mb-0">{title}</h1>
          {subtitle && <div className="text-muted small">{subtitle}</div>}
        </div>
      </div>

      <div className="d-flex align-items-center gap-2">
        {children}
        {actionLabel && (
          <button className="btn btn-primary d-flex align-items-center gap-2" onClick={handleAction}>
            <Plus size={16} />
            {actionLabel}
          </button>
        )}
      </div>
    </div>
  );
}

export default PageHeader;
