import { Link } from 'react-router-dom'
import { Button } from '@/components/ui/button'

export default function NotFound() {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-slate-900 dark:to-slate-800">
      <h1 className="text-6xl font-bold text-slate-900 dark:text-white mb-4">404</h1>
      <p className="text-2xl text-slate-600 dark:text-slate-300 mb-8">Página no encontrada</p>
      <Link to="/">
        <Button>Volver al inicio</Button>
      </Link>
    </div>
  )
}
