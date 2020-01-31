FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY Gestamp.sln .
COPY docker-compose.dcproj /src/
COPY src/SGDE.API/SGDE.API.csproj src/SGDE.API/
COPY src/SGDE.Domain/SGDE.Domain.csproj src/SGDE.Domain/
COPY src/SGDE.DataEFCoreMySQL/SGDE.DataEFCoreMySQL.csproj src/SGDE.DataEFCoreMySQL/
COPY src/SGDE.DataEFCoreSQL/SGDE.DataEFCoreSQL.csproj src/SGDE.DataEFCoreSQL/
COPY src/SGDE.SeedData/SGDE.SeedData.csproj src/SGDE.SeedData/
COPY src/SGDE.Web/SGDE.Web.csproj src/SGDE.Web/

RUN dotnet restore Arconsa.sln

COPY . .
WORKDIR /src/src/SGDE.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SGDE.API.dll"]