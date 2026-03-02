import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'
import { Toaster } from 'sonner'
import { ThemeProvider } from '@/hooks/use-theme'
import { LabAuthProvider, useLabAuth } from '@/hooks/use-lab-auth'

// Pages
import Landing from '@/pages/Landing'
import Login from '@/pages/Login'
import Registration from '@/pages/Registration'
import Dashboard from '@/pages/Dashboard'
import LabPortal from '@/pages/LabPortal'
import LabAppointments from '@/pages/LabAppointments'
import LabRegistration from '@/pages/LabRegistration'
import NotFound from '@/pages/NotFound'

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5,
      gcTime: 1000 * 60 * 10,
    },
  },
})

// Protected route wrapper
function ProtectedRoute({ children }: { children: React.ReactNode }) {
  const { isAuthenticated, isLoading } = useLabAuth()

  if (isLoading) {
    return <div className="flex items-center justify-center h-screen">Loading...</div>
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />
  }

  return <>{children}</>
}

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Landing />} />
      <Route path="/login" element={<Login />} />
      <Route path="/registro-laboratorio" element={<Registration />} />
      <Route
        path="/dashboard"
        element={
          <ProtectedRoute>
            <Dashboard />
          </ProtectedRoute>
        }
      />
      <Route
        path="/laboratorio"
        element={
          <ProtectedRoute>
            <LabPortal />
          </ProtectedRoute>
        }
      />
      <Route
        path="/laboratorio/citas"
        element={
          <ProtectedRoute>
            <LabAppointments />
          </ProtectedRoute>
        }
      />
      <Route
        path="/laboratorio/muestras"
        element={
          <ProtectedRoute>
            <LabRegistration />
          </ProtectedRoute>
        }
      />
      <Route path="*" element={<NotFound />} />
    </Routes>
  )
}

export default function App() {
  return (
    <ThemeProvider>
      <QueryClientProvider client={queryClient}>
        <LabAuthProvider>
          <Router>
            <AppRoutes />
            <Toaster position="bottom-right" />
          </Router>
        </LabAuthProvider>
      </QueryClientProvider>
    </ThemeProvider>
  )
}
