# Prueba Técnica Fullstack – Task Manager

## Introducción

Este proyecto corresponde a una **prueba técnica Fullstack**.

La solucion propuesta permite la **gestión de tareas**, asignadas a usuarios, con control de estados y soporte para información adicional almacenada en formato JSON dentro de SQL Server.

---

## Pasos para ejecutar el proyecto

### Requisitos previos
- .NET SDK 8.0
- SQL Server (LocalDB o instancia local)
- Node.js (v24)
- Angular (v21)
- Visual Studio 2022

---

### Base de datos
1. Crear una base de datos en SQL Server (ej: `TaskManagerDb`).
2. Ejecutar el script SQL incluido en el repositorio:
   - `SQL_CREATE.sql`
   - Este script crea las tablas `Users` y `Tasks`, índices, constraints y validaciones JSON.
3. Verificar que la columna `AdditionalData` exista como `NVARCHAR(MAX)` con validación `ISJSON`.

---

### Backend (.NET 8 Web API)
1. Ir a la carpeta `TaskManager.Api` y abrir la solución en **Visual Studio 2022**.
2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```
3. Ejecutar el proyecto (`https`).
4. Verificar que la API levante correctamente:
   - https://localhost:7197/api/tasks
   - https://localhost:7197/api/users
   - https://localhost:7197/swagger/

---

### Frontend (Angular 21 + Material)
1. Ir a la carpeta `task-manager-ui`.
2. Instalar dependencias:
   ```bash
   npm install
   ```
3. Ejecutar el proyecto:
   ```bash
   ng serve
   ```
4. Acceder a:
   - http://localhost:4200

---

## Funcionalidades implementadas

### Usuarios
- Crear usuarios
- Listar usuarios

> La gestión de usuarios se expone vía API. No se implementó una interfaz gráfica para usuarios, ya que el enunciado del requerimiento no lo solicita explícitamente.

### Tareas
- Crear tareas
- Listar tareas
- Filtrar tareas por estado
- Cambiar estado de una tarea (Pending, InProgress, Done)
- Asignar tareas a usuarios
- Manejo de información adicional mediante JSON

---

## Decisiones técnicas

- Se priorizó cumplir estrictamente el alcance solicitado
- Se evitó agregar funcionalidades no requeridas
- El manejo de JSON se demuestra directamente en SQL Server, como se solicita en el requerimiento
- El frontend se mantiene simple, claro y usable



### Backend
- **.NET 8 Web API**: versión estable.
- ** ASP.NET Core Web API**
- **Arquitectura en capas**:
  - **Controllers**: Exponen los endpoints REST
  - **Services**: Contienen la lógica de negocio
  - **Repositories**: Encapsulan el acceso a datos
  - **DTOs**: Separan el modelo de dominio de la API
- **Repositorios específicos** (`TaskRepository`, `UserRepository`) para separar responsabilidades y mayor claridad.
- **Entity Framework Core (Database First)**: 
  - Se optó por dapper pero se descartó para no complicar el desarrollo dado el alcance del proyecto.
  - EFC es simple y potente, cumple a cabalidad con los requerimientos.
- **Enums** para estados de tareas (`Pending`, `InProgress`, `Done`).
- **Reglas de negocio en backend**:
  - No se permite cambiar una tarea de `Pending` a `Done` directamente.
- **Manejo global de errores** mediante middleware.

---

### Base de Datos
- SQL Server como motor relacional.
- Uso de **JSON nativo en SQL Server**:
  - Columna `AdditionalData` (`NVARCHAR(MAX)`).
  - Validación con `ISJSON`.
  - Consultas con `JSON_VALUE`, `JSON_QUERY` y `OPENJSON`.
- Índices para optimizar consultas por usuario, estado y fecha.


La base de datos se define mediante scripts SQL y utiliza el enfoque **Database First**.

#### Estructura relevante

La tabla `Tasks` incluye el campo:

```sql
AdditionalData NVARCHAR(MAX)
```

Este campo permite almacenar información adicional en formato JSON.

#### Validación de JSON

Se asegura que el contenido sea JSON válido mediante la siguiente restricción:

```sql
CHECK (AdditionalData IS NULL OR ISJSON(AdditionalData) = 1)
```

#### Estados de las tareas

Los estados de las tareas se manejan mediante un **Enum**, almacenado como `INT` en la base de datos:

- `0` → Pending
- `1` → InProgress
- `2` → Done

Esto mejora la consistencia, performance y facilidad de mantenimiento.

---


### Frontend
- **Angular 21** con **Standalone Components**.
- **Angular Material** para UI.
- Reactive Forms
- Tabla con listado de tareas y filtro por estado.
- Modal para creación de tareas.
- Mapeo visual de enums (mostrar texto en lugar de número).
- Deshabilitación de acciones inválidas según estado.

---

## Funcionalidades pendientes / no implementadas

Las siguientes funcionalidades **no fueron implementadas** por no ser requeridas explícitamente o por alcance de la prueba:

### 🔹 Usuarios
- Gestión completa de usuarios (crear, editar, eliminar).
- Autenticación y login.
- Roles y permisos.

### 🔹 Tareas
- Eliminación de tareas.
- Edición completa de tareas (título, descripción, usuario).
- Historial de cambios de estado.

### 🔹 Seguridad
- Autenticación JWT.
- Autorización por roles.
- Protección de endpoints.

### 🔹 Calidad
- Tests unitarios y de integración.
- Manejo avanzado de logging.

---

## Manejo de JSON en SQL Server (Requerimiento adicional)

Como parte del manejo avanzado de base de datos, se utilizan funciones nativas de SQL Server para trabajar con datos en formato JSON.

### Ejemplo de JSON almacenado

```json
{
  "priority": "High",
  "estimatedEndDate": "2026-02-15",
  "tags": ["backend", "urgent"],
  "metadata": {
    "source": "manual",
    "slaHours": 48
  }
}
```

### Validar contenido JSON

```sql
SELECT Id, Title
FROM Tasks
WHERE ISJSON(AdditionalData) = 1;
```

### Consultar un campo específico (JSON_VALUE)

```sql
SELECT
  Id,
  Title,
  JSON_VALUE(AdditionalData, '$.priority') AS Priority
FROM Tasks;
```

### Filtrar tareas por un valor dentro del JSON

```sql
SELECT Id, Title
FROM Tasks
WHERE JSON_VALUE(AdditionalData, '$.priority') = 'High';
```

### Consultar arrays u objetos (JSON_QUERY)

```sql
SELECT
  Id,
  Title,
  JSON_QUERY(AdditionalData, '$.tags') AS Tags
FROM Tasks;
```

### Explorar arrays con OPENJSON

```sql
SELECT
  t.Id,
  t.Title,
  tag.value AS Tag
FROM Tasks t
CROSS APPLY OPENJSON(t.AdditionalData, '$.tags') AS tag;
```

### Actualizar un campo dentro del JSON

```sql
UPDATE Tasks
SET AdditionalData = JSON_MODIFY(AdditionalData, '$.priority', 'Medium')
WHERE Id = 1;
```

---


## Nota Final

La solución propuesta cumple con todos los requerimientos funcionales y técnicos solicitados en la prueba, incluyendo el manejo avanzado de JSON en SQL Server.

La solución prioriza:
- Claridad del código
- Correcta separación de responsabilidades
- Cumplimiento estricto de los requerimientos del documento

El diseño permite extender fácilmente las funcionalidades pendientes sin necesidad de refactorización mayor.

---

## Autor

Prueba técnica desarrollada por **[José Alberto Perez Narvaez]**
- <prespik@gmail.com>

