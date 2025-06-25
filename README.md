# Rover Mission Planner API

Microservicio para planificar y validar tareas de rovers exploradores en Marte.

## Requisitos
- .NET 9 SDK
- (Opcional) DB Browser for SQLite para ver la base de datos

## Instalación y uso

1. **Clonar el repositorio:**
   ```sh
   git clone <URL_DEL_REPO>
   cd app-psinet-back
   ```

2. **Restaurar dependencias y compilar:**
   ```sh
   dotnet build
   ```

3. **Ejecutar la API:**
   ```sh
   dotnet run --project RoverMissionPlanner.API
   ```
   La API estará disponible en `http://localhost:5027/swagger`.

4. **Ejecutar pruebas unitarias:**
   ```sh
   dotnet test RoverMissionPlanner.Tests
   ```

## Endpoints principales
- `POST   /rovers/{roverName}/tasks` — Crea una tarea para un rover
- `GET    /rovers/{roverName}/tasks?date=YYYY-MM-DD` — Lista tareas del día
- `GET    /rovers/{roverName}/utilization?date=YYYY-MM-DD` — Porcentaje de utilización diaria

## Notas
- Persistencia: SQLite (archivo `rovermissionplanner.db` en la carpeta de la API)
- Validaciones: FluentValidation
- Manejo global de errores: Middleware personalizado

## Estructura de carpetas
- `RoverMissionPlanner.Domain` — Entidades y contratos
- `RoverMissionPlanner.Application` — Lógica de negocio
- `RoverMissionPlanner.Infrastructure` — Persistencia de datos
- `RoverMissionPlanner.API` — API REST
- `RoverMissionPlanner.Tests` — Pruebas unitarias

---

¡Listo para usar! Para dudas, revisa el código o contacta al autor. 