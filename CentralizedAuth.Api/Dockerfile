FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CentralizedAuth.Api/CentralizedAuth.Api.csproj", "CentralizedAuth.Api/"]
RUN dotnet restore "CentralizedAuth.Api/CentralizedAuth.Api.csproj"
COPY . .
WORKDIR "/src/CentralizedAuth.Api"
RUN dotnet build "CentralizedAuth.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CentralizedAuth.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CentralizedAuth.Api.dll"]