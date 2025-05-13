# Plan de acción - Issue 68

## Objetivo
Permitir un escape anticipado en el método `Coalesce` mediante una excepción personalizada (`FallbackStrategyException`), de modo que se pueda abortar la evaluación de funciones cuando sea necesario.

## Tareas

1. **Crear la excepción personalizada**
   - Implementar la clase `FallbackStrategyException` en el namespace adecuado.

2. **Modificar el método `Coalesce`**
   - Actualizar el bloque `catch` para relanzar la excepción `FallbackStrategyException` y mantener el comportamiento actual para el resto.
   - Realizar el mismo ajuste en la versión asíncrona de `Coalesce`.

3. **Actualizar o agregar pruebas unitarias**
   - Verificar que el nuevo comportamiento funcione correctamente y que no se rompa la compatibilidad existente.

4. **Actualizar la documentación si corresponde**
   - Documentar el nuevo comportamiento en los comentarios del método y/o documentación del proyecto.

## Criterios de aceptación
- Si se lanza una `FallbackStrategyException` dentro de cualquier función pasada a `Coalesce`, la excepción debe propagarse y no ser capturada.
- El resto de las excepciones deben seguir siendo ignoradas como hasta ahora.
- Las pruebas unitarias deben pasar y reflejar el nuevo comportamiento.
- La documentación debe estar actualizada.
