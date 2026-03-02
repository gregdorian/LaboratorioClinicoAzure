import { Link } from 'react-router-dom'
import { Button } from '@/components/ui/button'

export default function Landing() {

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-slate-900 dark:to-slate-800">
      <nav className="border-b border-blue-200 dark:border-slate-700 bg-white dark:bg-slate-900 shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex justify-between items-center h-16">
          <div className="flex items-center gap-2">
            <div className="w-8 h-8 bg-blue-600 dark:bg-blue-500 rounded-lg"></div>
            <span className="font-bold text-xl text-slate-900 dark:text-white">LabSystem</span>
          </div>
          <div className="flex gap-4">
            <Link to="/login">
              <Button variant="outline">Iniciar Sesión</Button>
            </Link>
            <Link to="/registro-laboratorio">
              <Button>Registrar Laboratorio</Button>
            </Link>
          </div>
        </div>
      </nav>

      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
        <div className="grid md:grid-cols-2 gap-12 items-center">
          <div>
            <h1 className="text-5xl font-bold text-slate-900 dark:text-white mb-6">
              Sistema Integral de Gestión de Laboratorios
            </h1>
            <p className="text-xl text-slate-600 dark:text-slate-300 mb-8">
              Administra citas, muestras, análisis y resultados de forma segura y eficiente.
            </p>
            <div className="flex gap-4">
              <Link to="/login">
                <Button size="lg">Para Pacientes</Button>
              </Link>
              <Link to="/registro-laboratorio">
                <Button size="lg" variant="outline">
                  Para Laboratorios
                </Button>
              </Link>
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            {[
              { title: 'Citas', description: 'Agenda tus citas de forma fácil' },
              { title: 'Muestras', description: 'Seguimiento de tus muestras' },
              { title: 'Resultados', description: 'Consulta tus resultados' },
              { title: 'Reportes', description: 'Reportes e historial' },
            ].map((item) => (
              <div
                key={item.title}
                className="bg-white dark:bg-slate-800 p-6 rounded-lg shadow-md border border-blue-100 dark:border-slate-700"
              >
                <h3 className="font-semibold text-slate-900 dark:text-white mb-2">
                  {item.title}
                </h3>
                <p className="text-sm text-slate-600 dark:text-slate-300">
                  {item.description}
                </p>
              </div>
            ))}
          </div>
        </div>
      </main>
    </div>
  )
}
