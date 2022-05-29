dotnet tool update --global dotnet-ef
dotnet build
dotnet ef --startup-project ../blockcore.status/ database update --context SQLiteDbContext
pause