# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY . .
RUN dotnet restore

# Build the project
RUN dotnet build -c Release -o /app/build

# Publish the project
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published application from the build image
COPY --from=build /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "WalletFramework.dll"]