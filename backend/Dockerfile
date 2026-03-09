# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
# Port 80 is standard for Nginx to talk to
EXPOSE 80
ENTRYPOINT ["dotnet", "backend.dll"]
