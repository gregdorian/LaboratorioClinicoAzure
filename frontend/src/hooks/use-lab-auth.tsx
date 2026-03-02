import { createContext, useContext, useState, useCallback, useEffect } from 'react'

interface LabUser {
  id: string
  email: string
  laboratorioId: string
  laboratorioName: string
  role: 'admin' | 'technician' | 'director'
  token: string
}

interface LabAuthContextType {
  user: LabUser | null
  isAuthenticated: boolean
  isLoading: boolean
  login: (email: string, password: string) => Promise<void>
  logout: () => void
  register: (laboratoryData: any) => Promise<void>
}

const LabAuthContext = createContext<LabAuthContextType | undefined>(undefined)

export function LabAuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<LabUser | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  // Initialize from localStorage
  useEffect(() => {
    const storedUser = localStorage.getItem('labUser')
    if (storedUser) {
      try {
        setUser(JSON.parse(storedUser))
      } catch (error) {
        console.error('Failed to parse stored user:', error)
        localStorage.removeItem('labUser')
      }
    }
    setIsLoading(false)
  }, [])

  const login = useCallback(async (email: string, password: string) => {
    setIsLoading(true)
    try {
      const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
      })

      if (!response.ok) {
        throw new Error('Login failed')
      }

      const data = await response.json()
      const userData: LabUser = {
        id: data.userId,
        email: data.email,
        laboratorioId: data.laboratorioId,
        laboratorioName: data.laboratorioName,
        role: data.role,
        token: data.token,
      }

      setUser(userData)
      localStorage.setItem('labUser', JSON.stringify(userData))
    } finally {
      setIsLoading(false)
    }
  }, [])

  const logout = useCallback(() => {
    setUser(null)
    localStorage.removeItem('labUser')
  }, [])

  const register = useCallback(async (laboratoryData: any) => {
    setIsLoading(true)
    try {
      const response = await fetch('/api/auth/register-laboratory', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(laboratoryData),
      })

      if (!response.ok) {
        throw new Error('Registration failed')
      }

      const data = await response.json()
      const userData: LabUser = {
        id: data.userId,
        email: data.email,
        laboratorioId: data.laboratorioId,
        laboratorioName: data.laboratorioName,
        role: data.role,
        token: data.token,
      }

      setUser(userData)
      localStorage.setItem('labUser', JSON.stringify(userData))
    } finally {
      setIsLoading(false)
    }
  }, [])

  return (
    <LabAuthContext.Provider
      value={{
        user,
        isAuthenticated: !!user,
        isLoading,
        login,
        logout,
        register,
      }}
    >
      {children}
    </LabAuthContext.Provider>
  )
}

export function useLabAuth() {
  const context = useContext(LabAuthContext)
  if (context === undefined) {
    throw new Error('useLabAuth must be used within a LabAuthProvider')
  }
  return context
}
