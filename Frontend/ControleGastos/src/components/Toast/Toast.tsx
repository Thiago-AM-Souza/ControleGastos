import { useEffect } from 'react';
import { CheckCircle, XCircle, AlertCircle } from 'lucide-react';

export interface ToastProps {
  id: string;
  type: 'success' | 'error' | 'warning' | 'info';
  message: string;
  onClose: (id: string) => void;
  duration?: number;
}

const icons = {
  success: CheckCircle,
  error: XCircle,
  warning: AlertCircle,
  info: AlertCircle,
};

function Toast({ id, type, message, onClose, duration = 3000 }: ToastProps) {
  const Icon = icons[type];

  useEffect(() => {
    const timer = setTimeout(() => {
      onClose(id);
    }, duration);

    return () => clearTimeout(timer);
  }, [id, duration, onClose]);

  const alertClass =
    type === 'success'
      ? 'alert-success'
      : type === 'error'
      ? 'alert-danger'
      : type === 'warning'
      ? 'alert-warning'
      : 'alert-info';

  return (
    <div className={`toast-container ${alertClass} d-flex align-items-center justify-content-between`} role="alert">
      <div className="d-flex align-items-center gap-2">
        <Icon size={20} />
        <div>{message}</div>
      </div>
      <button type="button" className="btn-close" aria-label="Close" onClick={() => onClose(id)} />
    </div>
  );
}

export default Toast;
