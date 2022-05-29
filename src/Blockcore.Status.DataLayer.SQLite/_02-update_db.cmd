dotnet tool update --global dotnet-ef
dotnet build
dotnet ef --startup-project ../Blockcore.Status/ database update --context SQLiteDbContext
pause