# LabSystem Frontend

Sistema integral de gestión de laboratorios - Frontend moderno con React 18, TypeScript y Tailwind CSS.

## 🚀 Características

- **React 18.2.0** con TypeScript 5.3.3
- **Vite 5.0.8** para bundling ultrarrápido
- **React Router v6.20.1** para enrutamiento
- **TanStack React Query v5.28.0** para gestión de estado del servidor
- **Tailwind CSS 3.4.1** con dark mode
- **shadcn/ui** - 30+ componentes reutilizables
- **Sistema de autenticación** con JWT
- **Tema oscuro/claro** personalizable
- **Notificaciones** con sonner

## 📋 Requisitos previos

- Node.js 18+
- npm o yarn

## 🔧 Instalación

1. Clonar el repositorio:
```bash
git clone https://github.com/tu-usuario/laboratorio-clinico.git
cd LaboratorioClinicoAzure/frontend
```

2. Instalar dependencias:
```bash
npm install
```

3. Crear archivo .env:
```bash
cp .env.example .env
```

4. Configurar variables de entorno:
```env
VITE_API_BASE_URL=http://localhost:5000/api
```

## 🏃 Ejecución

### Desarrollo

```bash
npm run dev
```

La aplicación se abrirá en `http://localhost:5173`

### Build para producción

```bash
npm run build
```

### Preview de producción

```bash
npm run preview
```

## 📁 Estructura del proyecto

```
src/
├── components/
│   ├── ui/              # Componentes shadcn/ui
│   ├── DashboardLayout.tsx
│   ├── LabSidebar.tsx
│   ├── StatCard.tsx
│   ├── DataTable.tsx
│   └── FormField.tsx
├── hooks/
│   ├── use-theme.tsx    # Tema oscuro/claro
│   ├── use-toast.ts      # Notificaciones
│   ├── use-lab-auth.tsx   # Autenticación
│   ├── use-mobile.tsx   # Detección de móvil
│   └── index.ts
├── lib/
│   ├── api.ts           # Cliente HTTP
│   └── utils.ts         # Utilidades
├── pages/
│   ├── Landing.tsx      # Página de inicio
│   ├── Login.tsx        # Inicio de sesión
│   ├── Registration.tsx  # Registro de laboratorio
│   ├── Dashboard.tsx    # Dashboard de pacientes
│   ├── LabPortal.tsx    # Portal de laboratorio
│   ├── LabAppointments.tsx
│   ├── LabRegistration.tsx
│   └── NotFound.tsx
├── App.tsx              # Configuración de rutas
├── main.tsx             # Entry point
└── index.css            # Estilos globales
```

## 📖 Guía de uso

### Autenticación

```typescript
import { useLabAuth } from '@/hooks'

function MyComponent() {
  const { user, login, logout, isAuthenticated } = useLabAuth()
  
  const handleLogin = async () => {
    try {
      await login('user@example.com', 'password')
    } catch (error) {
      console.error('Login failed:', error)
    }
  }
  
  return (
    <div>
      {isAuthenticated ? (
        <p>Welcome, {user?.email}</p>
      ) : (
        <button onClick={handleLogin}>Login</button>
      )}
    </div>
  )
}
```

### Notificaciones

```typescript
import { useToast } from '@/hooks'

function MyComponent() {
  const { toast } = useToast()
  
  return (
    <button onClick={() => toast('Éxito!', 'success')}>
      Show Toast
    </button>
  )
}
```

### Tema oscuro/claro

```typescript
import { useTheme } from '@/hooks'

function MyComponent() {
  const { theme, setTheme, isDark } = useTheme()
  
  return (
    <button onClick={() => setTheme(isDark ? 'light' : 'dark')}>
      Toggle Theme
    </button>
  )
}
```

### Llamadas a la API

```typescript
import { apiGet, apiPost } from '@/lib/api'
import { useLabAuth } from '@/hooks'

function MyComponent() {
  const { user } = useLabAuth()
  
  const handleFetch = async () => {
    try {
      const data = await apiGet('/citas', user?.token)
      console.log(data)
    } catch (error) {
      console.error('Error:', error)
    }
  }
  
  return <button onClick={handleFetch}>Fetch Data</button>
}
```

### Componentes shadcn/ui

```typescript
import {
  Button,
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Input,
  Label,
  Dialog,
  DialogContent,
  DialogTitle,
} from '@/components/ui'

export function MyComponent() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Título</CardTitle>
      </CardHeader>
      <CardContent>
        <Label>Campo</Label>
        <Input placeholder="Ingresa algo..." />
        <Button>Enviar</Button>
      </CardContent>
    </Card>
  )
}
```

## 🎨 Temas y personalización

El proyecto usa Tailwind CSS con variables CSS para fácil personalización:

```css
/* src/index.css */
:root {
  --primary: 221.2 83.2% 53.3%;
  --destructive: 0 84.2% 60.2%;
  /* más variables... */
}

.dark {
  --primary: 217.2 91.2% 59.8%;
  /* más variables... */
}
```

## 🌙 Dark Mode

El dark mode se activa automáticamente basado en las preferencias del sistema, pero se puede personalizar:

```typescript
// El usuario prefiere dark mode
setTheme('dark')

// El usuario prefiere light mode
setTheme('light')

// Usar preferencias del sistema
setTheme('system')
```

## 🚀 Despliegue

### Con Vercel

```bash
npx vercel
```

### Con Netlify

```bash
npm run build
# Subir la carpeta dist a Netlify
```

### Con Docker

```dockerfile
FROM node:18-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo LICENSE para más detalles.

## 📧 Contacto

- Proyecto: [GitHub Repository]
- Issues: [GitHub Issues]

## 🔗 Enlaces útiles

- [React Documentation](https://react.dev)
- [Vite Documentation](https://vitejs.dev)
- [Tailwind CSS](https://tailwindcss.com)
- [shadcn/ui](https://ui.shadcn.com)
- [React Router](https://reactrouter.com)
- [TanStack Query](https://tanstack.com/query/latest)

---

Desenvolvido con ❤️ para la gestión integral de laboratorios clínicos
