# Multi-stage Dockerfile for Manufacturing Monitoring API (.NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ManufacturingMonitoring.API/ManufacturingMonitoring.API.csproj", "ManufacturingMonitoring.API/"]
RUN dotnet restore "ManufacturingMonitoring.API/ManufacturingMonitoring.API.csproj"

COPY . .
WORKDIR "/src/ManufacturingMonitoring.API"
RUN dotnet build "ManufacturingMonitoring.API.csproj" -c Release -o /app/build
RUN dotnet publish "ManufacturingMonitoring.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ManufacturingMonitoring.API.dll"]
