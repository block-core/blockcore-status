#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Blockcore.Status/NuGet.config", "src/Blockcore.Status/"]
COPY ["src/Blockcore.Status/Blockcore.Status.csproj", "src/Blockcore.Status/"]
COPY ["src/Blockcore.Status.Common/Blockcore.Status.Common.csproj", "src/Blockcore.Status.Common/"]
COPY ["src/Blockcore.Status.Entities/Blockcore.Status.Entities.csproj", "src/Blockcore.Status.Entities/"]
COPY ["src/Blockcore.Status.ViewModels/Blockcore.Status.ViewModels.csproj", "src/Blockcore.Status.ViewModels/"]
COPY ["src/Blockcore.Status.Services/Blockcore.Status.Services.csproj", "src/Blockcore.Status.Services/"]
COPY ["src/Blockcore.Status.DataLayer/Blockcore.Status.DataLayer.csproj", "src/Blockcore.Status.DataLayer/"]
COPY ["src/Blockcore.Status.IocConfig/Blockcore.Status.IocConfig.csproj", "src/Blockcore.Status.IocConfig/"]
COPY ["src/Blockcore.Status.DataLayer.InMemoryDatabase/Blockcore.Status.DataLayer.InMemoryDatabase.csproj", "src/Blockcore.Status.DataLayer.InMemoryDatabase/"]
COPY ["src/Blockcore.Status.DataLayer.SQLite/Blockcore.Status.DataLayer.SQLite.csproj", "src/Blockcore.Status.DataLayer.SQLite/"]
COPY ["src/Blockcore.Status.DataLayer.MSSQL/Blockcore.Status.DataLayer.MSSQL.csproj", "src/Blockcore.Status.DataLayer.MSSQL/"]
COPY . .
RUN dotnet restore "src/Blockcore.Status/Blockcore.Status.csproj"

WORKDIR "/src/src/Blockcore.Status"
RUN dotnet build "Blockcore.Status.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blockcore.Status.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blockcore.Status.dll"]