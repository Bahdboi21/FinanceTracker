# Step 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Step 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all the project files
COPY ["FinanceTracker/FinanceTracker.csproj", "./FinanceTracker/"]
COPY ["FinanceTracker.Application/FinanceTracker.Application.csproj", "./FinanceTracker.Application/"]
COPY ["FinanceTracker.Infrastructure/FinanceTracker.Infrastructure.csproj", "./FinanceTracker.Infrastructure/"]
COPY ["FinanceTracker.Persistence/FinanceTracker.Persistence.csproj", "./FinanceTracker.Persistence/"]

# Restore dependencies
RUN dotnet restore "./FinanceTracker/FinanceTracker.csproj"

# Copy the full source
COPY . .

# Set the working directory to the main project
WORKDIR "/src/FinanceTracker"
RUN dotnet build "FinanceTracker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Step 3: Publish Image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinanceTracker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Step 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceTracker.dll"]
