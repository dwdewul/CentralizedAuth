FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CentralizedAuth.Authorization/CentralizedAuth.Authorization.csproj", "CentralizedAuth.Authorization/"]
RUN dotnet restore "CentralizedAuth.Authorization/CentralizedAuth.Authorization.csproj"
COPY . .
WORKDIR "/src/CentralizedAuth.Authorization"
RUN dotnet build "CentralizedAuth.Authorization.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CentralizedAuth.Authorization.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CentralizedAuth.Authorization.dll"]