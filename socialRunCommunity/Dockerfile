# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

# Add SSL certificates
# COPY /path/to/your/surfonlife.pfx /etc/ssl/surfonlife.pfx

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "socialRunCommunity.dll"]
