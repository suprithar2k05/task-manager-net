# Use .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Use ASP.NET runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "server.dll"]
