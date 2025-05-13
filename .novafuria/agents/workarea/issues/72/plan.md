# Plan de acción para la issue 72

## Análisis inicial

(Pendiente de completar tras el análisis de la issue y el contexto)

## Estrategia y firmas propuestas

### Estrategia

- Implementar los métodos de extensión en la clase `ExceptionExtension` dentro del namespace `Cedeira.Essentials.NET.Extensions.Exceptions`, siguiendo el patrón de la extensión `FullMessage`.
- Los métodos serán recursivos, explorando la cadena de `InnerException` para detectar o recuperar excepciones de un tipo específico.
- Se implementarán variantes para búsqueda por tipo (`Type`), por nombre (`string`) y genérica (`T` donde T : Exception).
- Se priorizará la eficiencia y la claridad del código, asegurando que la búsqueda se detenga al encontrar la primera coincidencia.
- Se agregarán pruebas unitarias para cubrir los casos de uso principales y algunos bordes.

### Firmas propuestas

```csharp
// Devuelve true si la excepción o alguna anidada es del tipo especificado
public static bool ContainsException(this Exception ex, Type exceptionType)
public static bool ContainsException(this Exception ex, string exceptionTypeName)
public static bool ContainsException<T>(this Exception ex) where T : Exception

// Devuelve la primera excepción anidada que coincide con el tipo especificado
public static Exception? FindException(this Exception ex, Type exceptionType)
public static Exception? FindException(this Exception ex, string exceptionTypeName)
public static T? FindException<T>(this Exception ex) where T : Exception
```

- Todas las variantes devuelven `null` si no se encuentra coincidencia.
- Se documentarán los métodos siguiendo el estándar del repositorio.

## Tareas

- [ ] Analizar en detalle la descripción y contexto de la issue
- [ ] Investigar la implementación previa en Cedeira.CedMT.WorkspaceContext
- [ ] Definir la ubicación y firma de los métodos de extensión
- [ ] Implementar métodos ContainsException y FindException (todas variantes)
- [ ] Crear pruebas unitarias
- [ ] Documentar el código y actualizar el README si corresponde
- [ ] Registrar avances en journal.md 

## Plan de pruebas unitarias

Se diseñarán pruebas para cubrir los siguientes escenarios en cada variante de los métodos ContainsException y FindException:

### Escenarios generales
- Excepción simple del tipo buscado (debe encontrarla)
- Excepción anidada (InnerException) del tipo buscado (debe encontrarla)
- Excepción anidada en varios niveles (debe encontrar la primera coincidencia)
- Ninguna excepción del tipo buscado (debe devolver false/null)
- Excepción nula (debe manejarse sin lanzar excepción)
- Tipo/nombre nulo o vacío (debe manejarse sin lanzar excepción)

### Casos específicos
- Buscar por tipo (Type)
- Buscar por nombre (string)
- Buscar por tipo genérico (T)
- Buscar tipos base y derivados (herencia)
- Buscar tipos con el mismo nombre pero diferente namespace (si aplica)

Cada método será testeado con asserts positivos y negativos, y se validará que la recursividad no cause stack overflow en cadenas largas de InnerException. 