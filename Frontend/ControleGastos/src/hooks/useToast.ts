import { useState, useCallback } from 'react';

interface ToastItem {
  id: string;
  type: 'success' | 'error' | 'warning' | 'info';
  message: string;
}

export function useToast() {
  const [toasts, setToasts] = useState<ToastItem[]>([]);

  const addToast = useCallback(
    (type: ToastItem['type'], message: string) => {
      const id = Math.random().toString(36).substring(2, 9);
      setToasts((prev) => [...prev, { id, type, message }]);
    },
    []
  );

  const removeToast = useCallback((id: string) => {
    setToasts((prev) => prev.filter((toast) => toast.id !== id));
  }, []);

  const success = useCallback(
    (message: string) => addToast('success', message),
    [addToast]
  );

  const error = useCallback(
    (message: string) => addToast('error', message),
    [addToast]
  );

  const warning = useCallback(
    (message: string) => addToast('warning', message),
    [addToast]
  );

  const info = useCallback(
    (message: string) => addToast('info', message),
    [addToast]
  );

  return {
    toasts,
    addToast,
    removeToast,
    success,
    error,
    warning,
    info,
  };
}
