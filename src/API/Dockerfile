# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrderService-cqrs-ddd.API/OrderService-cqrs-ddd.API.csproj", "OrderService-cqrs-ddd.API/"]
RUN dotnet restore "OrderService-cqrs-ddd.API/OrderService-cqrs-ddd.API.csproj"
COPY . .
WORKDIR "/src/OrderService-cqrs-ddd.API"
RUN dotnet build "OrderService-cqrs-ddd.API.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "OrderService-cqrs-ddd.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService-cqrs-ddd.API.dll"]
