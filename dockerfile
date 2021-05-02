# syntax=docker/dockerfile:1
# Installing images

FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0

# Copying application content 
COPY LibraryManagementBackend/bin/Release/net5.0/publish/ app/
COPY LibraryManagementBackend/database.db app/

# Switching directory to app/
WORKDIR /app

# Exposing ports
EXPOSE 5000
EXPOSE 5001
EXPOSE 10000

# Setting entrypoint. This is the command that will be executed when the container starts up
CMD ["dotnet", "LibraryManagementBackend.dll", "--launch-profile", "Production"]
