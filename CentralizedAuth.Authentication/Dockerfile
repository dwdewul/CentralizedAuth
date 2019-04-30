FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CentralizedAuth.Authentication/CentralizedAuth.Authentication.csproj", "CentralizedAuth.Authentication/"]
RUN dotnet restore "CentralizedAuth.Authentication/CentralizedAuth.Authentication.csproj"
COPY . .
WORKDIR "/src/CentralizedAuth.Authentication"
RUN dotnet build "CentralizedAuth.Authentication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CentralizedAuth.Authentication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CentralizedAuth.Authentication.dll"]