# Project in progress

prr

## Dumy Data

This project uses SQLServer as DB manager. To create an fill the tables needed by this proyect, run the sql queries in the file dumyData.sql.

dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update