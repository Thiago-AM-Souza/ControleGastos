import { AlertTriangle } from 'lucide-react';

interface ConfirmModalProps {
  isOpen: boolean;
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
  type?: 'danger' | 'warning' | 'info';
  onConfirm: () => void;
  onCancel: () => void;
}

function ConfirmModal({
  isOpen,
  title,
  message,
  confirmText = 'Confirmar',
  cancelText = 'Cancelar',
  type = 'danger',
  onConfirm,
  onCancel,
}: ConfirmModalProps) {
  if (!isOpen) return null;

  const color = type === 'danger' ? 'danger' : type === 'warning' ? 'warning' : 'primary';

  return (
    <>
      <div className="modal show d-block" tabIndex={-1} role="dialog" onClick={onCancel}>
        <div className="modal-dialog modal-dialog-centered" role="document" onClick={(e) => e.stopPropagation()}>
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{title}</h5>
              <button type="button" className="btn-close" aria-label="Close" onClick={onCancel}></button>
            </div>
            <div className="modal-body">
              <div className="d-flex align-items-start gap-3">
                <div className={`fs-3 text-${color}`}><AlertTriangle size={24} /></div>
                <div>{message}</div>
              </div>
            </div>
            <div className="modal-footer">
              <button className="btn btn-secondary" onClick={onCancel}>{cancelText}</button>
              <button className={`btn ${color === 'primary' ? 'btn-primary' : `btn-${color}`}`} onClick={onConfirm}>{confirmText}</button>
            </div>
          </div>
        </div>
      </div>
      <div className="modal-backdrop fade show"></div>
    </>
  );
}

export default ConfirmModal;
