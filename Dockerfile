# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files for dependencies
COPY ["src/API/API.csproj", "API/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["src/API/Endpoints/gRPC/Protos/OrdersGrpc.proto", "API/Endpoints/gRPC/Protos/"]

COPY . .

# Restore dependencies
RUN dotnet restore "API/API.csproj"

COPY . .

WORKDIR "src/API"
RUN dotnet publish "API.csproj" -c Release -o /app

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "API.dll"]
