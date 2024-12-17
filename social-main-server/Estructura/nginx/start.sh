#!/bin/sh

# Iniciar SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Iniciar la API en segundo plano
dotnet Estructura.dll &  # Asegúrate de que Estructura.dll es el archivo correcto, ya que la aplicación es Estructura, no Estructura.API

# Iniciar NGINX en primer plano
nginx -g 'daemon off;'
