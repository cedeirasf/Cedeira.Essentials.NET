FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet tool install -g docfx
ENV PATH="$PATH:/root/.dotnet/tools"
RUN docfx build


FROM nginx:alpine
COPY --from=build-env /app/_site/ /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

