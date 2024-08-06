# Instalamos docfx
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
RUN dotnet tool install -g docfx
ENV PATH="$PATH:/root/.dotnet/tools"

# Preparamos archivos y generamos documentacion
WORKDIR /app
COPY . .
RUN docfx ./docs/docfx.json

# Copiamos el resultado de la documentacion generada
FROM nginx:alpine
COPY --from=build-env /app/docs/_site/ /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
