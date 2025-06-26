# Instrucciones para el Frontend (Angular) – Consumo de la API Rover Mission Planner

## 1. Base URL
La API corre en:
```
http://localhost:5027
```
Swagger: [http://localhost:5027/swagger](http://localhost:5027/swagger)

---

## 2. Endpoints principales

### a) Crear tarea
- **POST** `/rovers/{roverName}/tasks`
- **Body (JSON):**
  ```json
  {
    "id": "GUID",                // Puede ser generado en el frontend o dejarlo vacío si el backend lo autogenera
    "RoverName": "string",       // Debe coincidir con {roverName} en la URL
    "TaskType": 0,                // 0: Drill, 1: Sample, 2: Photo, 3: Charge
    "Latitude": 0,
    "Longitude": 0,
    "StartsAt": "2025-06-25T19:55:30.954Z", // Formato ISO 8601 (UTC)
    "DurationMinutes": 60,
    "Status": 0                   // 0: Planned, 1: InProgress, 2: Completed, 3: Aborted
  }
  ```
- **Respuestas:**
  - `201 Created` si se crea correctamente.
  - `409 Conflict` si hay solapamiento de tareas.
  - `400 Bad Request` si hay errores de validación.

### b) Listar tareas del día
- **GET** `/rovers/{roverName}/tasks?date=YYYY-MM-DD`
- **Respuesta:**
  ```json
  [
    {
      "id": "GUID",
      "RoverName": "string",
      "TaskType": 0,
      "Latitude": 0,
      "Longitude": 0,
      "StartsAt": "2025-06-25T19:55:30.954Z",
      "DurationMinutes": 60,
      "Status": 0
    }
  ]
  ```

### c) Utilización diaria
- **GET** `/rovers/{roverName}/utilization?date=YYYY-MM-DD`
- **Respuesta:**
  ```json
  {
    "utilization": 25.0
  }
  ```

---

## 3. Modelos y enums

- **TaskType:**
  - 0: Drill
  - 1: Sample
  - 2: Photo
  - 3: Charge

- **Status:**
  - 0: Planned
  - 1: InProgress
  - 2: Completed
  - 3: Aborted

---

## 4. Validaciones
- No se permiten tareas solapadas para el mismo rover.
- Todos los campos son obligatorios.
- Latitud: -90 a 90, Longitud: -180 a 180.
- Duración debe ser mayor a 0.

---

## 5. Notas técnicas
- La API responde en JSON.
- Los errores de validación vienen en formato estándar de ASP.NET.
- El backend está preparado para ser consumido por cualquier SPA moderna (Angular, React, etc.).
- Puedes ver y probar todos los endpoints en `/swagger`.

---

## 6. Ejemplo de flujo para el frontend
1. El usuario selecciona un rover y una fecha.
2. El frontend hace un GET a `/rovers/{roverName}/tasks?date=YYYY-MM-DD` para mostrar la agenda.
3. El usuario puede crear una nueva tarea con el formulario, que hace un POST a `/rovers/{roverName}/tasks`.
4. El frontend puede mostrar el porcentaje de utilización llamando a `/rovers/{roverName}/utilization?date=YYYY-MM-DD`.

---

**Cualquier duda, consulta el README del backend o contacta al desarrollador.** 