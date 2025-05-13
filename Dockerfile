# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy .csproj files
COPY ["FinanceTracker/FinanceTracker.csproj", "FinanceTracker/"]
COPY ["FinanceTracker.Application/FinanceTracker.Application.csproj", "FinanceTracker.Application/"]
COPY ["FinanceTracker.Infrastructure/FinanceTracker.Infrastructure.csproj", "FinanceTracker.Infrastructure/"]
COPY ["FinanceTracker.Persistence/FinanceTracker.Persistence.csproj", "FinanceTracker.Persistence/"]

# Restore dependencies
RUN dotnet restore "FinanceTracker/FinanceTracker.csproj"

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR "/src/FinanceTracker"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceTracker.dll"]
