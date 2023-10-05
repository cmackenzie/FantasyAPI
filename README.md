# FantasyAPI

This project is using the following technologies and frameworks

- Code
  - C#
  - .NET MVC
  - Entity Framework
- Data
  - Npgsql
  - postgresql

This project was built on a mac. If this is going to cause an issue, let me know.

## Getting started

### System/Software requirments

- A working postgres instance
- .NET
- Visual Studio

### Quick Start

- Open `FantasyAPI.sln up` with Visual Studio
- Update your postgres connection string [here](https://github.com/cmackenzie/FantasyAPI/blob/main/FantasyCore/DbContext.cs#L16)
- Open the terminal in Visual Studio and navigate to the `FantasyCore` project folder
- Run the schema migration `dotnet ef database update`
- Run the FantasyProcessor in Debug Mode and allow each sport processor to run at least once (can follow allong in the logs)
- Run the FantasyAPI in Debug Mode. You may use the generated Swagger docs to test the API endspoints
- (Optional) You may set the solution up to run both projects at once if you'd like.

## Project Overview

### FantasyCore

This project houses all of the shared code across the api and the data processor. It is primarily used to share the dbcontext and models across both projects, but it also holds some other utility classes as well.
FantasyAPI runs on Postgres via the Npgsql package via the Entity Framework. You may set you connection string [here](https://github.com/cmackenzie/FantasyAPI/blob/main/FantasyCore/DbContext.cs#L16) and that will work across projects.

### FantasyAPI

This project houses the REST api to get players by our internal id as well as search for lists of players. There are 2 endpoints and the details of those endpoints (requests and response) are available on the SwaggerUI when you run the API in development mode.

### FantasyProcessor

This project houses the data processor that is responsible for reaching out to the CBS fantasy player API. It is roughly setup as a series of sports specifc polling and persisting services using a sport specific channel to pass messages between each other. This is obviously overkill for a project of this size, but I wanted to illustrate how I might approach this in a production setting for a system that may need many of these tasks and the consistency/latency issues you might run into.

The bulk of the importing/persisting logic can be found in the `Engines` folder.

Note: The CBS Apis are queried every 30 seconds, if this is too frequent, feel free to adjust the frequency [here](https://github.com/cmackenzie/FantasyAPI/blob/main/FantasyProcessor/Services/Polling/ScheduledPollingService.cs#L9)

## Complaints and Feature Requests

If you have any questions or any issues, or need any clarification on anything, don't hesitate to reach out cameron.r.mackenzie@gmail.com
