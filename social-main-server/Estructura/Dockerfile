# Etapa 1: Compilación de la API
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copiar los archivos de la solución y restaurar dependencias
COPY Estructura.sln ./
COPY Estructura/*.csproj ./Estructura/
WORKDIR /app/Estructura
RUN dotnet restore

# Copiar el resto del código fuente y compilar la aplicación
COPY Estructura/. ./Estructura/
RUN dotnet publish -c Release -o /publish

# Etapa 2: Configurar SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest AS sqlserver

# Variables de entorno para SQL Server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong(!)Password
ENV MSSQL_PID=Express

# Etapa 3: Configurar NGINX y la API
FROM nginx:alpine AS nginx

# Instalar dependencias adicionales en Alpine (opcional)
RUN apk update && apk add --no-cache bash nano curl

# Copiar los artefactos compilados de la API
COPY --from=build /publish /app

# Copiar el archivo de configuración de NGINX
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf

# Copiar el script de inicio
COPY ./nginx/start.sh /start.sh
RUN chmod +x /start.sh

# Exponer los puertos
EXPOSE 1433 5001 8080

# Comando para iniciar SQL Server, la API y NGINX con el script de inicio
CMD ["/start.sh"]
