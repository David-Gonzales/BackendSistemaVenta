FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura dependencias
COPY *.sln ./
COPY WebAPI/*.csproj ./WebAPI/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Identity/*.csproj ./Identity/
COPY Persistence/*.csproj ./Persistence/
COPY Shared/*.csproj ./Shared/
RUN dotnet restore

# Copia el resto del código y compila
COPY . .
WORKDIR /src/WebAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebAPI.dll"]