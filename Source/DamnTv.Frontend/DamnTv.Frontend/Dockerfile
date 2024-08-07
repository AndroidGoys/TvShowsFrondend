# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/DamnTv.Frontend/DamnTv.Frontend/DamnTv.Frontend.csproj", "Source/DamnTv.Frontend/DamnTv.Frontend/"]
COPY ["Source/DamnTv.Api.Client/DamnTv.Api.Client.csproj", "Source/DamnTv.Api.Client/"]
COPY ["Source/DamnTv.Frontend.PreviewDesign/DamnTv.Frontend.PreviewDesign.csproj", "Source/DamnTv.Frontend.PreviewDesign/"]
COPY ["Source/DamnTv.Frontend/DamnTv.Frontend.Client/DamnTv.Frontend.Client.csproj", "Source/DamnTv.Frontend/DamnTv.Frontend.Client/"]
RUN dotnet restore "./Source/DamnTv.Frontend/DamnTv.Frontend/DamnTv.Frontend.csproj"
COPY . .
WORKDIR "/src/Source/DamnTv.Frontend/DamnTv.Frontend"
RUN dotnet build "./DamnTv.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build/

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DamnTv.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish/ /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DamnTv.Frontend.dll"]