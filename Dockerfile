# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura dependencias
COPY *.sln ./
COPY WebAPI/*.csproj ./WebAPI/
RUN dotnet restore

# Copia el resto del código y compila
COPY . .
WORKDIR /src/WebAPI
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebAPI.dll"]