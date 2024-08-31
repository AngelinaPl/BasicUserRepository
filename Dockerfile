FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BasicUserRepository.Api/BasicUserRepository.Api.csproj", "BasicUserRepository.Api/"]
RUN dotnet restore "./BasicUserRepository.Api/BasicUserRepository.Api.csproj"
COPY . .
WORKDIR "/src/BasicUserRepository.Api"
RUN dotnet build "./BasicUserRepository.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BasicUserRepository.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasicUserRepository.Api.dll"]