# Cedeira.Essentials.NET
Libreria para .NET con funcionalidades comunes y reusbles para los proyectos de Cedeira.

# Documentación

Para generar la documentación estática usando `docfx` de manera local ejecute los siguientes pasos:

```bash
cd "ruta al repositorio"

cd ./docs
docfx docfx.json
```

Si quiere ejecutar un servidor que exponga la documentación, use el flag `--serve` del comando docfx:

```bash
cd "ruta al repositorio"

cd ./docs
docfx docfx.json --serve
```

La documentación se generará como un sitio web en el directorio `docs/_site`. El proceso esta configurado para copiar dentro del sitio la carpeta `docs/api` que genera `docfx` al mapear los proyectos y los artefactos dentro de la carpeta `docs/` que permiten incorporar un índice y una página de bienvenida.

> ![NOTE]
> Ejecutar `docfx ./docs/docfx.json --serve` desde la raíz del repositorio genera el mismo resultado de manera local

Para generar la imagen:

```bash
docker build -t <nombre de imagen>:latest -f ./docs/Dockerfile .
```
