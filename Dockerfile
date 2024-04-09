FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY AutoDrivingCarSimulation ./AutoDrivingCarSimulation

# Restore dependencies
WORKDIR /app/AutoDrivingCarSimulation/CarSimulation
RUN dotnet restore

WORKDIR /app/AutoDrivingCarSimulation/CarSimulation.UnitTests
RUN dotnet restore

# Build the console app
WORKDIR /app/AutoDrivingCarSimulation/CarSimulation
RUN dotnet build -c Release -o /app/build

# Build the test app
WORKDIR /app/AutoDrivingCarSimulation/CarSimulation.UnitTests
RUN dotnet build -c Release -o /app/build

FROM build AS runtime
WORKDIR /app/build