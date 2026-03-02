import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'
import { useLabAuth, useToast } from '@/hooks'

export default function Registration() {
  const navigate = useNavigate()
  const { register, isLoading } = useLabAuth()
  const { toast } = useToast()
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [formData, setFormData] = useState({
    laboratoryName: '',
    email: '',
    password: '',
    confirmPassword: '',
    nit: '',
    city: '',
    address: '',
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (formData.password !== formData.confirmPassword) {
      toast('Las contraseñas no coinciden', 'error')
      return
    }

    setIsSubmitting(true)

    try {
      await register(formData)
      toast('Laboratorio registrado exitosamente', 'success')
      navigate('/laboratorio')
    } catch (error) {
      toast('Error al registrar el laboratorio', 'error')
    } finally {
      setIsSubmitting(false)
    }
  }

  if (isLoading) {
    return <div className="flex items-center justify-center h-screen">Loading...</div>
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-slate-900 dark:to-slate-800 flex items-center justify-center p-4">
      <Card className="w-full max-w-lg">
        <CardHeader className="space-y-2 text-center">
          <CardTitle>Registrar Laboratorio</CardTitle>
          <CardDescription>Crea una cuenta para tu laboratorio</CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  Nombre del Laboratorio
                </label>
                <Input
                  type="text"
                  name="laboratoryName"
                  placeholder="MiLaboratorio"
                  value={formData.laboratoryName}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  NIT
                </label>
                <Input
                  type="text"
                  name="nit"
                  placeholder="123456789"
                  value={formData.nit}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
            </div>

            <div>
              <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                Email
              </label>
              <Input
                type="email"
                name="email"
                placeholder="contacto@laboratorio.com"
                value={formData.email}
                onChange={handleChange}
                required
                disabled={isSubmitting}
              />
            </div>

            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  Ciudad
                </label>
                <Input
                  type="text"
                  name="city"
                  placeholder="Bogotá"
                  value={formData.city}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  Dirección
                </label>
                <Input
                  type="text"
                  name="address"
                  placeholder="Calle 1 # 2-3"
                  value={formData.address}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
            </div>

            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  Contraseña
                </label>
                <Input
                  type="password"
                  name="password"
                  placeholder="••••••••"
                  value={formData.password}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
              <div>
                <label className="text-sm font-medium text-slate-700 dark:text-slate-300">
                  Confirmar Contraseña
                </label>
                <Input
                  type="password"
                  name="confirmPassword"
                  placeholder="••••••••"
                  value={formData.confirmPassword}
                  onChange={handleChange}
                  required
                  disabled={isSubmitting}
                />
              </div>
            </div>

            <Button type="submit" className="w-full" disabled={isSubmitting}>
              {isSubmitting ? 'Registrando...' : 'Registrar Laboratorio'}
            </Button>
          </form>
        </CardContent>
      </Card>
    </div>
  )
}
