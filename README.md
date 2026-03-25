# Weather Tracker

This project is a simple weather tracking system built with C#, ASP.NET Core, Entity Framework Core, SQLite, HTML, and JavaScript.

The system fetches weather data from an external API, stores weather snapshots in a local database, and displays the history in a web interface.

## Technologies
- C#
- ASP.NET Core Minimal API
- Entity Framework Core
- SQLite
- HTML
- JavaScript

## How to run
1. Open the project in VS Code
2. Run:
   dotnet restore
3. Run:
   dotnet run
4. Open the browser at:
   http://localhost:5013

## Features
- Fetch current weather data from an external API
- Save weather snapshots in a local SQLite database
- Show full weather history
- Show the last 5 weather measurements
- Use Dependency Injection
- Use Observer Pattern

## Known issues
- Weather data is updated manually by button click
- The UI is intentionally simple