# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiere nur die csproj-Datei zuerst
COPY KundenUmfrageTool.Api.csproj ./

# Restore Abhängigkeiten
RUN dotnet restore "KundenUmfrageTool.Api.csproj"

# Dann Rest vom Code
COPY . .

# Projekt builden
RUN dotnet publish "KundenUmfrageTool.Api.csproj" -c Release -o /app

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "KundenUmfrageTool.Api.dll"]
