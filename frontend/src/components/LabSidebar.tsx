import { Link, useLocation } from 'react-router-dom'
import { cn } from '@/lib/utils'
import {
  Calendar,
  Vials,
  FileText,
  Settings,
  Home,
  BarChart3,
} from 'lucide-react'

interface SidebarItem {
  label: string
  href: string
  icon: React.ReactNode
  badge?: number
}

interface LabSidebarProps {
  items?: SidebarItem[]
}

const defaultItems: SidebarItem[] = [
  {
    label: 'Dashboard',
    href: '/laboratorio',
    icon: <Home className="h-5 w-5" />,
  },
  {
    label: 'Citas',
    href: '/laboratorio/citas',
    icon: <Calendar className="h-5 w-5" />,
    badge: 3,
  },
  {
    label: 'Muestras',
    href: '/laboratorio/muestras',
    icon: <Vials className="h-5 w-5" />,
  },
  {
    label: 'Reportes',
    href: '/laboratorio/reportes',
    icon: <FileText className="h-5 w-5" />,
  },
  {
    label: 'Análisis',
    href: '/laboratorio/analisis',
    icon: <BarChart3 className="h-5 w-5" />,
  },
  {
    label: 'Configuración',
    href: '/laboratorio/configuracion',
    icon: <Settings className="h-5 w-5" />,
  },
]

export function LabSidebar({ items = defaultItems }: LabSidebarProps) {
  const location = useLocation()

  return (
    <nav className="space-y-2 p-4">
      {items.map((item) => (
        <Link
          key={item.href}
          to={item.href}
          className={cn(
            'flex items-center gap-3 px-4 py-2 rounded-lg transition-colors',
            location.pathname === item.href
              ? 'bg-blue-100 dark:bg-blue-950 text-blue-600 dark:text-blue-400'
              : 'text-slate-700 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-800'
          )}
        >
          {item.icon}
          <span className="flex-1">{item.label}</span>
          {item.badge && (
            <span className="bg-red-500 text-white text-xs rounded-full px-2 py-1">
              {item.badge}
            </span>
          )}
        </Link>
      ))}
    </nav>
  )
}
