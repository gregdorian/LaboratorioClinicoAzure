import { useLabAuth } from '@/hooks'

export default function Dashboard() {
  const { user } = useLabAuth()

  return (
    <div className="min-h-screen bg-slate-50 dark:bg-slate-900">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h1 className="text-3xl font-bold text-slate-900 dark:text-white">
          Bienvenido, {user?.email}
        </h1>
        <p className="text-slate-600 dark:text-slate-300 mt-2">
          Dashboard de paciente - En desarrollo
        </p>
      </div>
    </div>
  )
}
