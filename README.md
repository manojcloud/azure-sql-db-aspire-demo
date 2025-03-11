# Project Name

(short, 1-3 sentenced, description of the project)

## Base

The basic .Net Aspire application with a simple WebAPI that you can use as a starting point to add support for Azure SQL or SQL Server

## Bring-Your-Own-SQL-Server (BYOSS)

The simplest integration of SQL Server in .NET Aspire. It takes the "Base" example and updates the WebAPI so that now it is calling an existing SQL Server, using a provided connectiong string.

## Aspire-Hosted SQL Server

Change the BYOSS sample so the SQL Server is deployed and managed by Aspire orchestration, providing a thight integration with the Aspire environment. SQL Server is deployed in a OCI container by Aspire. No changes to WebAPI project.


