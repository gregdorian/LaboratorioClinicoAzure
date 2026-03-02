# API Documentation - LabSystem Backend

## Overview

LabSystem es un sistema integral de gestión de laboratorios clínicos construido con .NET 8 y Entity Framework Core. Esta documentación describe todos los endpoints disponibles.

## Base URL

```
http://localhost:5000/api
```

## Authentication

Todos los endpoints protegidos requieren un token JWT en el header:

```
Authorization: Bearer {token}
```

## Endpoints

### Authentication

#### Login
- **Endpoint**: `POST /auth/login`
- **Description**: Autentica un usuario
- **Request Body**:
  ```json
  {
    "email": "user@example.com",
    "password": "password123"
  }
  ```
- **Response**:
  ```json
  {
    "token": "jwt_token_here",
    "userId": "user-id",
    "email": "user@example.com",
    "laboratorioId": "lab-id",
    "laboratorioName": "Lab Name",
    "role": "admin"
  }
  ```

#### Register Laboratory
- **Endpoint**: `POST /auth/register-laboratory`
- **Description**: Registra un nuevo laboratorio
- **Request Body**:
  ```json
  {
    "laboratoryName": "Lab Name",
    "email": "contact@lab.com",
    "password": "password123",
    "nit": "123456789",
    "city": "Bogotá",
    "address": "Calle 1 # 2-3"
  }
  ```

### Citas (Appointments)

#### Get All Appointments
- **Endpoint**: `GET /citas`
- **Auth**: Required
- **Query Parameters**:
  - `laboratorioId` (optional): Filter by laboratory
  - `pacienteId` (optional): Filter by patient
  - `estado` (optional): Filter by status
- **Response**:
  ```json
  [
    {
      "citaId": "cita-id",
      "fechaHora": "2024-01-15T10:30:00",
      "pacienteId": "patient-id",
      "paciente": {
        "pacienteId": "patient-id",
        "persona": {
          "nombreCompleto": "John Doe"
        }
      },
      "disponibilidadHorariaId": "availability-id",
      "disponibilidadHoraria": {
        "horaInicio": "08:00",
        "horaFin": "17:00"
      },
      "estado": "Confirmada"
    }
  ]
  ```

#### Get Appointment by ID
- **Endpoint**: `GET /citas/{id}`
- **Auth**: Required
- **Response**: Single appointment with full details

#### Get Patient Appointments
- **Endpoint**: `GET /citas/paciente/{pacienteId}`
- **Auth**: Required
- **Response**: List of appointments for a specific patient

#### Create Appointment
- **Endpoint**: `POST /citas`
- **Auth**: Required
- **Request Body**:
  ```json
  {
    "fechaHora": "2024-01-15T10:30:00",
    "pacienteId": "patient-id",
    "disponibilidadHorariaId": "availability-id"
  }
  ```

#### Update Appointment
- **Endpoint**: `PUT /citas/{id}`
- **Auth**: Required
- **Request Body**: Same as create

#### Delete Appointment
- **Endpoint**: `DELETE /citas/{id}`
- **Auth**: Required

---

### Solicitudes (Requests)

#### Get All Requests
- **Endpoint**: `GET /solicitudes`
- **Auth**: Required
- **Query Parameters**:
  - `laboratorioId` (optional)
  - `pacienteId` (optional)
  - `estado` (optional)
- **Response**: List of requests with exam counts

#### Get Request by ID
- **Endpoint**: `GET /solicitudes/{id}`
- **Auth**: Required

#### Get Patient Requests
- **Endpoint**: `GET /solicitudes/paciente/{pacienteId}`
- **Auth**: Required

#### Create Request
- **Endpoint**: `POST /solicitudes`
- **Auth**: Required
- **Request Body**:
  ```json
  {
    "pacienteId": "patient-id",
    "examenesIds": ["exam-id-1", "exam-id-2"],
    "observaciones": "Optional observations"
  }
  ```

#### Update Request
- **Endpoint**: `PUT /solicitudes/{id}`
- **Auth**: Required

#### Delete Request
- **Endpoint**: `DELETE /solicitudes/{id}`
- **Auth**: Required

---

### Muestras (Samples)

#### Get All Samples
- **Endpoint**: `GET /muestras`
- **Auth**: Required
- **Response**: List of samples with reception status

#### Get Sample by ID
- **Endpoint**: `GET /muestras/{id}`
- **Auth**: Required

#### Get Request Samples
- **Endpoint**: `GET /muestras/solicitud/{solicitudId}`
- **Auth**: Required

#### Register Sample Reception
- **Endpoint**: `POST /muestras/recepcion`
- **Auth**: Required
- **Request Body**:
  ```json
  {
    "solicitudExamenId": "exam-id",
    "fechaRecepcion": "2024-01-15T10:30:00",
    "usuarioId": "user-id"
  }
  ```

#### Register Sample Analysis
- **Endpoint**: `POST /muestras/analisis`
- **Auth**: Required
- **Request Body**:
  ```json
  {
    "muestraId": "sample-id",
    "fechaAnalisis": "2024-01-15T14:00:00",
    "resultadoInicial": true,
    "usuarioAnalista": "user-id"
  }
  ```

---

### Resultados (Results)

#### Get All Results
- **Endpoint**: `GET /resultados`
- **Auth**: Required
- **Query Parameters**:
  - `laboratorioId` (optional)
  - `pacienteId` (optional)
  - `estado` (optional)

#### Get Result by ID
- **Endpoint**: `GET /resultados/{id}`
- **Auth**: Required

#### Get Patient Results
- **Endpoint**: `GET /resultados/paciente/{pacienteId}`
- **Auth**: Required

#### Search Results
- **Endpoint**: `GET /resultados/buscar`
- **Auth**: Required
- **Query Parameters**:
  - `numeroDocumento`: Patient document number
  - `apellido`: Patient last name
  
- **Response**:
  ```json
  [
    {
      "resultadoId": "result-id",
      "nroDocumento": "123456789",
      "apellido": "Doe",
      "nombre": "John",
      "examen": "Blood Test",
      "resultado": "Normal",
      "fecha": "2024-01-15"
    }
  ]
  ```

#### Get Result Details
- **Endpoint**: `GET /resultados/{id}/detalles`
- **Auth**: Required

---

### Laboratorios (Laboratories)

#### Get All Laboratories
- **Endpoint**: `GET /laboratorios`
- **Auth**: Required (optional for public list)
- **Response**: List of laboratories

#### Get Laboratory by ID
- **Endpoint**: `GET /laboratorios/{id}`
- **Auth**: Optional

#### Get Laboratory Exams
- **Endpoint**: `GET /laboratorios/{id}/examenes`
- **Auth**: Optional
- **Response**:
  ```json
  {
    "laboratorioId": "lab-id",
    "nombre": "Lab Name",
    "examenes": [
      {
        "examenId": "exam-id",
        "nombre": "Blood Test",
        "codigo": "BLC001"
      }
    ]
  }
  ```

---

## Data Transfer Objects (DTOs)

### CitaDetailDto
```typescript
{
  citaId: string
  fechaHora: Date
  pacienteId: string
  paciente: { pacienteId: string, persona: { nombreCompleto: string } }
  disponibilidadHorariaId: string
  disponibilidadHoraria: { horaInicio: string, horaFin: string }
  estado: string
}
```

### SolicitudDto
```typescript
{
  solicitudId: string
  pacienteId: string
  laboratorioId: string
  fechaRegistro: Date
  examenesCount: number
  estado: string
}
```

### MuestraDto & RecepcionMuestraDto
```typescript
{
  muestraId: string
  solicitudExamenId: string
  estado: string
  fechaRecepcion: Date
}
```

### ResultadoSearchDto
```typescript
{
  nroDocumento: string
  apellido: string
  nombre: string
  examen: string
  resultado: string
  fecha: Date
}
```

### ResultadoDetailDto
```typescript
{
  resultadoId: string
  solicitudExamenId: string
  laboratorioId: string
  resultado: string
  interpretacion: string
  valoresReferencia: string
  fechaResultado: Date
}
```

### LaboratorioDto
```typescript
{
  laboratorioId: string
  nombre: string
  nit: string
  ciudad: string
  direccion: string
  examenes: Array<{ examenId: string, nombre: string }>
}
```

---

## Error Responses

All errors follow this format:

```json
{
  "message": "Error description",
  "code": "ERROR_CODE",
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### Common Error Codes

- `UNAUTHORIZED`: Missing or invalid token
- `FORBIDDEN`: User doesn't have permission
- `NOT_FOUND`: Resource not found
- `BAD_REQUEST`: Invalid request data
- `CONFLICT`: Resource already exists
- `INTERNAL_SERVER_ERROR`: Server error

---

## Rate Limiting

- **Limit**: 100 requests per minute per IP
- **Header**: `X-RateLimit-Remaining`

---

## Pagination

Optional pagination support:

```
GET /endpoint?page=1&pageSize=10
```

Response includes:
```json
{
  "data": [...],
  "pagination": {
    "page": 1,
    "pageSize": 10,
    "total": 100,
    "pages": 10
  }
}
```

---

## Examples

### Example: Get Patient Results
```bash
curl -X GET "http://localhost:5000/api/resultados/paciente/patient-123" \
  -H "Authorization: Bearer your_token_here" \
  -H "Content-Type: application/json"
```

### Example: Create Appointment
```bash
curl -X POST "http://localhost:5000/api/citas" \
  -H "Authorization: Bearer your_token_here" \
  -H "Content-Type: application/json" \
  -d '{
    "fechaHora": "2024-01-20T10:30:00",
    "pacienteId": "patient-123",
    "disponibilidadHorariaId": "availability-456"
  }'
```

### Example: Search Results
```bash
curl -X GET "http://localhost:5000/api/resultados/buscar?numeroDocumento=123456789&apellido=Doe" \
  -H "Authorization: Bearer your_token_here"
```

---

## Changelog

- **v1.0.0** (2024-01-15)
  - Initial API release
  - Added 16 endpoints for appointments, requests, samples, results, and laboratories
  - JWT authentication
  - Response pagination support

---

## Support

Para reportar issues o solicitar features: [GitHub Issues](https://github.com/tu-usuario/laboratorio-clinico/issues)
