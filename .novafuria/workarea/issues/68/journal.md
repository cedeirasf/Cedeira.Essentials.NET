## $(date '+%Y-%m-%d %H:%M')

Inicio del trabajo en la issue 68. Se analizó el código actual de `Coalesce` y se confirmó que no existe un mecanismo de escape anticipado ni la excepción `FallbackStrategyException`. Se creó la estructura de workarea y se redactó el plan de acción inicial.

Se implementó la excepción personalizada `FallbackStrategyException` y se modificaron ambos métodos `Coalesce` (sincrónico y asíncrono) para relanzar esta excepción, manteniendo el comportamiento actual para el resto. Listo para avanzar con las pruebas unitarias.

Se agregaron y ejecutaron los tests unitarios para verificar la propagación de FallbackStrategyException en ambos métodos Coalesce. Todos los tests pasaron correctamente, validando el nuevo comportamiento.
