dotnet tool update --global dotnet-ef --version 6.0.1
dotnet build
dotnet ef --startup-project ../Blockcore.Status/ database update --context MsSqlDbContext
pause