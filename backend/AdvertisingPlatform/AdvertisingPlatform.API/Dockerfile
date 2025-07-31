# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AdvertisingPlatform/AdvertisingPlatform.API/AdvertisingPlatform.API.csproj", "AdvertisingPlatform/AdvertisingPlatform.API/"]
COPY ["Shared/Shared/Shared.csproj", "Shared/Shared/"]
COPY ["AdvertisingPlatform/AdvertisingPlatform.Application/AdvertisingPlatform.Application.csproj", "AdvertisingPlatform/AdvertisingPlatform.Application/"]
COPY ["AdvertisingPlatform/AdvertisingPlatform.Infrastructure/AdvertisingPlatform.Infrastructure.csproj", "AdvertisingPlatform/AdvertisingPlatform.Infrastructure/"]
RUN dotnet restore "./AdvertisingPlatform/AdvertisingPlatform.API/AdvertisingPlatform.API.csproj"
COPY . .
WORKDIR "/src/AdvertisingPlatform/AdvertisingPlatform.API"
RUN dotnet build "./AdvertisingPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AdvertisingPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdvertisingPlatform.API.dll"]