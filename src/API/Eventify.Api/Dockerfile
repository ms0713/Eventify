#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/Eventify.Api/Eventify.Api.csproj", "API/Eventify.Api/"]
COPY ["Modules/Events/Eventify.Modules.Events.Infrastructure/Eventify.Modules.Events.Infrastructure.csproj", "Modules/Events/Eventify.Modules.Events.Infrastructure/"]
COPY ["Modules/Events/Eventify.Modules.Events.Application/Eventify.Modules.Events.Application.csproj", "Modules/Events/Eventify.Modules.Events.Application/"]
COPY ["Modules/Events/Eventify.Modules.Events.Domain/Eventify.Modules.Events.Domain.csproj", "Modules/Events/Eventify.Modules.Events.Domain/"]
COPY ["Modules/Events/Eventify.Modules.Events.Presentation/Eventify.Modules.Events.Presentation.csproj", "Modules/Events/Eventify.Modules.Events.Presentation/"]
RUN dotnet restore "./API/Eventify.Api/Eventify.Api.csproj"
COPY . .
WORKDIR "/src/API/Eventify.Api"
RUN dotnet build "./Eventify.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eventify.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eventify.Api.dll"]