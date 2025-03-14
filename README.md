# SQL Server Aspire Samples

A set of samples that show how to integrate SQL Server and Azure SQL with .NET Aspire.

## Base

- Folder: `./base`

The basic .Net Aspire application with a simple WebAPI project (WebApplication1) that you can use as a starting point to add support for Azure SQL or SQL Server

## Bring-Your-Own-SQL-Server (BYOSS)

- Folder: `./byoss`

The simplest integration of SQL Server in .NET Aspire. It takes the "Base" example and updates the WebAPI so that now it is calling an existing SQL Server, using a provided connectiong string.

## Aspire-Hosted SQL Server

- Folder: `./hostedss`

Change the BYOSS sample so the SQL Server is deployed and managed by Aspire orchestration, providing a basic integration with the Aspire environment. SQL Server is deployed in a OCI container by Aspire. No changes to WebAPI project.

The integration with Aspire is quite basic in this sample as it is done using only on the server side, introducting usage the `Aspire.Hosting.SqlServer` library in AppHost project. 

The client WebAPI application (Webapplication1) is still getting the connection string from the Aspire-provided environment variable.

## Aspire-Hosted SQL Server + DBUp

- Folder: `./hostedss - dbup`

In this sample, which is based on the previous one, deployment of database schema is also added to the solution. 

In addition there is now full integration of SQL Server with the Aspire environment. The client WebAPI application (WebApplication1) is now getting the connection object via Dependency Injection, thanks to the usage of the `Aspire.Microsoft.Data.SqlClient` library. 

The database is deployed using an imperative approach, via the `DbUp` library, that is also orchestrated by Aspire, by a custom application (DatabaseDeploy) that is added to the AppHost project to allow Aspire to orchestrate it.

## Aspire-Hosted SQL Server + Database Project

- Folder: `./hostedss - dbprj`

Similar to the previous sample, but the database is deployed using a declarative approach, using a [Database Project](https://learn.microsoft.com/sql/tools/sql-database-projects/sql-database-projects?view=sql-server-ver16), that is also orchestrated by Aspire, via the community extension `CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects`.

## Aspire-Hosted SQL Server + EF Core

- Folder: `./hostedss - ef`

In this sample, the client application (Webapplication1) is now using Entity Framework Core to interact with the SQL Server. The EF Core database context is provided via Dependency Injection, thanks to the usage of the library: `Aspire.Microsoft.EntityFrameworkCore.SqlServer`.

The database is deployed using a EF Core database migrations, that are deployed with the support of a Worker Service, as explained in the Aspire documentation ["Apply Entity Framework Core migrations in .NET Aspire"](https://learn.microsoft.com/en-us/dotnet/aspire/database/ef-core-migrations).

Please note that to keep the sample as simple as possible the EF Core entities and migrations have been defined directly in the WebApplication1 solution, which is not a best practice for a real-world application. A dedicated project should be created for the EF Core entities and migrations.

## Aspire-Hosted SQL Server + DbUp + DAB

- Folder: `./hostedss - dbup - dab`

In this sample, taken from the `./hostedss - dbup` sample, and add Data API Builder (DAB) to the solution, so that the database can be exposed as a REST and GraphQL API, so there is no need anymore to manuallly created a CRUD API and therefore the WebAPI project is removed.

DAB is orchestrated by Aspire, via the community extension `CommunityToolkit.Aspire.Hosting.Azure.DataApiBuilder`, that has been added to the AppHost project.

## End-To-End Jamstack "ToDo List" Application 

- Folder: `./todo_app`

Full end-to-end example of a Jamstack application, with a Vue front-end, a Data API Builder back-end, and SQL Server database, deployed using a Database Project. Everything is orchestrated by Aspire, including the Node application (TodoApp.Frontend), thanks to the `ommunityToolkit.Aspire.Hosting.NodeJS.Extensions` library.



