import { toast as sonnerToast } from 'sonner'

type ToastType = 'default' | 'success' | 'error' | 'loading'

export function useToast() {
  const toast = (
    message: string,
    type: ToastType = 'default',
    options?: { duration?: number }
  ) => {
    const duration = options?.duration || 4000

    switch (type) {
      case 'success':
        sonnerToast.success(message, { duration })
        break
      case 'error':
        sonnerToast.error(message, { duration })
        break
      case 'loading':
        sonnerToast.loading(message, { duration })
        break
      default:
        sonnerToast(message, { duration })
    }
  }

  return { toast }
}
