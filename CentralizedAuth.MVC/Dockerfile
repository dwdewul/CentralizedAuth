FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CentralizedAuth.MVC/CentralizedAuth.MVC.csproj", "CentralizedAuth.MVC/"]
RUN dotnet restore "CentralizedAuth.MVC/CentralizedAuth.MVC.csproj"
COPY . .
WORKDIR "/src/CentralizedAuth.MVC"
RUN dotnet build "CentralizedAuth.MVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CentralizedAuth.MVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CentralizedAuth.MVC.dll"]