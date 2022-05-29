#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/blockcore.status/NuGet.config", "src/blockcore.status/"]
COPY ["src/blockcore.status/blockcore.status.csproj", "src/blockcore.status/"]
COPY ["src/blockcore.status.Common/blockcore.status.Common.csproj", "src/blockcore.status.Common/"]
COPY ["src/blockcore.status.Entities/blockcore.status.Entities.csproj", "src/blockcore.status.Entities/"]
COPY ["src/blockcore.status.ViewModels/blockcore.status.ViewModels.csproj", "src/blockcore.status.ViewModels/"]
COPY ["src/blockcore.status.Services/blockcore.status.Services.csproj", "src/blockcore.status.Services/"]
COPY ["src/blockcore.status.DataLayer/blockcore.status.DataLayer.csproj", "src/blockcore.status.DataLayer/"]
COPY ["src/blockcore.status.IocConfig/blockcore.status.IocConfig.csproj", "src/blockcore.status.IocConfig/"]
COPY ["src/blockcore.status.DataLayer.InMemoryDatabase/blockcore.status.DataLayer.InMemoryDatabase.csproj", "src/blockcore.status.DataLayer.InMemoryDatabase/"]
COPY ["src/blockcore.status.DataLayer.SQLite/blockcore.status.DataLayer.SQLite.csproj", "src/blockcore.status.DataLayer.SQLite/"]
COPY ["src/blockcore.status.DataLayer.MSSQL/blockcore.status.DataLayer.MSSQL.csproj", "src/blockcore.status.DataLayer.MSSQL/"]
RUN dotnet restore "src/blockcore.status/blockcore.status.csproj"
COPY . .
WORKDIR "/src/src/blockcore.status"
RUN dotnet build "blockcore.status.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "blockcore.status.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blockcore.status.dll"]