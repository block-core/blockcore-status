#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Blockcore.Status/NuGet.config", "src/Blockcore.Status/"]
COPY ["src/Blockcore.Status/blockcore.status.csproj", "src/Blockcore.Status/"]
COPY ["src/Blockcore.Status.Common/blockcore.status.Common.csproj", "src/Blockcore.Status.Common/"]
COPY ["src/Blockcore.Status.Entities/blockcore.status.Entities.csproj", "src/Blockcore.Status.Entities/"]
COPY ["src/Blockcore.Status.ViewModels/blockcore.status.ViewModels.csproj", "src/Blockcore.Status.ViewModels/"]
COPY ["src/Blockcore.Status.Services/blockcore.status.Services.csproj", "src/Blockcore.Status.Services/"]
COPY ["src/Blockcore.Status.DataLayer/blockcore.status.DataLayer.csproj", "src/Blockcore.Status.DataLayer/"]
COPY ["src/Blockcore.Status.IocConfig/blockcore.status.IocConfig.csproj", "src/Blockcore.Status.IocConfig/"]
COPY ["src/Blockcore.Status.DataLayer.InMemoryDatabase/blockcore.status.DataLayer.InMemoryDatabase.csproj", "src/Blockcore.Status.DataLayer.InMemoryDatabase/"]
COPY ["src/Blockcore.Status.DataLayer.SQLite/blockcore.status.DataLayer.SQLite.csproj", "src/Blockcore.Status.DataLayer.SQLite/"]
COPY ["src/Blockcore.Status.DataLayer.MSSQL/blockcore.status.DataLayer.MSSQL.csproj", "src/Blockcore.Status.DataLayer.MSSQL/"]
RUN dotnet restore "src/Blockcore.Status/Blockcore.Status.csproj"
COPY . .
WORKDIR "/src/src/Blockcore.Status"
RUN dotnet build "Blockcore.Status.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blockcore.Status.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blockcore.Status.dll"]