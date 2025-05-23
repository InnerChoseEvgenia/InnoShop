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
COPY ["Services/ProductManagement/Product.API/Product.API.csproj", "Services/ProductManagement/Product.API/"]
COPY ["Services/ProductManagement/Product.Application/Product.Application.csproj", "Services/ProductManagement/Product.Application/"]
COPY ["Services/ProductManagement/Product.Core/Product.Core.csproj", "Services/ProductManagement/Product.Core/"]
COPY ["Services/ProductManagement/Product.Infrastructure/Product.Infrastructure.csproj", "Services/ProductManagement/Product.Infrastructure/"]
RUN dotnet restore "./Services/ProductManagement/Product.API/Product.API.csproj"
COPY . .
WORKDIR "/src/Services/ProductManagement/Product.API"
RUN dotnet build "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.API.dll"]