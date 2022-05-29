dotnet tool update --global dotnet-ef --version 6.0.1
dotnet build
dotnet ef --startup-project ../blockcore.status/ database update --context MsSqlDbContext
pause