# Используем официальный образ .NET 8.0 SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы решения и проекта
COPY ./SocialRunCommunity_back.sln ./
COPY ./socialRunCommunity/*.csproj ./socialRunCommunity/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем остальные файлы и собираем приложение
COPY ./socialRunCommunity/. ./socialRunCommunity/
WORKDIR /app/socialRunCommunity
RUN dotnet publish -c Release -o out

# Используем официальный образ .NET 8.0 ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Установка PostgreSQL клиента для миграций (если требуется)
RUN apt-get update && apt-get install -y postgresql-client

# Копируем опубликованное приложение
COPY --from=build /app/socialRunCommunity/out ./

# Настройка сертификатов HTTPS
# Копируем сертификаты из локального каталога проекта в контейнер
COPY ./certs/*.crt /etc/ssl/certs/
COPY ./certs/*.key /etc/ssl/private/

# Устанавливаем переменные окружения для сертификатов
ENV ASPNETCORE_Kestrel__Endpoints__Https__Url=https://+:443
ENV ASPNETCORE_Kestrel__Endpoints__Https__Certificate__Path=/etc/ssl/certs/your_certificate.crt
ENV ASPNETCORE_Kestrel__Endpoints__Https__Certificate__KeyPath=/etc/ssl/private/your_private.key

# Открываем порты
EXPOSE 80
EXPOSE 443

# Запускаем приложение
ENTRYPOINT ["dotnet", "socialRunCommunity.dll"]
