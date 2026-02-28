Lab System - Prototype

Scaffold de ejemplo: backend en C# (.NET) usando DDD/Hexagonal + CQRS (sin MediatR) y frontend en React 19.

Instrucciones rápidas:

- Backend (.NET):

  cd backend
  dotnet restore
  dotnet run

- Frontend (Vite + React):

  cd frontend
  npm install
  npm run dev

Nota: Ajusta la cadena de conexión en `backend/appsettings.Development.json` antes de ejecutar.

Frontend - variables y proxy

- Para desarrollar contra un backend local, puedes usar la variable de entorno `VITE_API_URL`.
  Por ejemplo, desde la raíz del proyecto:

  ```bash
  cd frontend
  VITE_API_URL=http://localhost:5000 npm run dev
  ```

- Si prefieres usar el proxy integrado de Vite, añade `VITE_API_URL` vacío y configura el proxy en `vite.config.js` (opcional).

Si el backend corre en HTTPS (por ejemplo .NET con Kestrel), ajusta el `VITE_API_URL` a la URL HTTPS del backend.
