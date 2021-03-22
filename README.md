# FullStackWebApp
> Proof of concept .NET Core 5 and Angular 11 application


## Table of contents
* [Description](#description)
* [Screenshot](#screenshot)
* [Requirements](#requirements)
* [Installation](#installation)
* [Project structure](#project-structure)
* [Database Model](#database-model)

## Description
This is full stack project for fetching data that populates database on schedule. Thad data is displayed on Client app so user can see two scoreboards. One is showing top 10 brokers who scored the most offers. The other is showing the same but for offers that had a gardent also.

## Screenshot
![image](https://user-images.githubusercontent.com/16215654/111913586-20ba1100-8a6f-11eb-8c8c-28ae2a141743.png)

## Requirements
- Visual Studio 2019
- MSSQL Server
    - To download MSSQL Server:
        - Go to [SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) page
        - Download "Developer" edition
        - Install with "Basic" profile
        - Optionally install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?redirectedfrom=MSDN&view=sql-server-ver15)
- [.NET Core 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
    - To confirm, check `dotnet --list-sdks` and `dotnet --list-runtimes`


## Installation
- Clone project from Github or download ZIP
- Open project in Visual Studio
- Via NuGet console, run migrations: `Update-Database`
- Run default configuration (`IIS Express`)


## Project structure
The project is divided in two applications `FullStackWebApp` (backend) and `Client` (frontend).

- `FullStackWebApp` notable folders
    - `AutoMapperProfiles` - Backend is using [AutoMapper](https://www.nuget.org/packages/automapper/) package to populate model information from DTOs.
    - `DTOs` - [DTO pattern](https://en.wikipedia.org/wiki/Data_transfer_object) files, structuring data for all APIs (internal and Funda's)
    - `Schedulers` - [Quartz](https://www.nuget.org/packages/Quartz/) is used for scheduling and handling background job.
        - Data from Funda's API is stored in local database as part of the Job.
    - `Services` - data fetching logic is in Service ([Separation of Concerns](https://en.wikipedia.org/wiki/Separation_of_concerns) pattern)
        - Job shouldn't care how data is fetched, and Service doesn't care who's fetching the data.
    - `AanbodControler` with 3 endpoints:
        - `TestFetchingData` - test service that fetches data and populates database.
        - `TestJob` - to run job manually (automaticaly job is scheduled for execution every 12 hours).
        - `LeaderBoard/{tuin}` - that gets hit by the client application

## Database Model
- `Aanbod` table to store all offers
    - `Id` `(int)`
    - `GUID` `(string)`
    - `Type` `(string)`
    - `City` `(string)`
    - `Tuin` `(bool)`
    - `MakelaarId` `(int)`
    - `MakelaarNaam` `(stirng)`
