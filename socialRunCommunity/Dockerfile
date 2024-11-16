# Используем SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем всё остальное и компилируем приложение
COPY . ./
RUN dotnet publish -c Release -o out

# Базовый runtime-образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Запускаем Kestrel на HTTP
ENTRYPOINT ["dotnet", "socialRunCommunity.dll"]
